using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FluentUI.Generator
{
    public static class GeneratorTypeUtils
    {
        public static IEnumerable<PropertyInfo> GetProperties(
            Type type,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public
        ) {
            var included = new List<PropertyInfo>();

            var ownProps = type.GetProperties(flags | BindingFlags.DeclaredOnly);
            foreach (var p in ownProps)
            {
                if (p.PropertyType.ContainsGenericParameters ||
                    !p.CanWrite ||
                    p.SetMethod == null ||
                    !p.SetMethod.IsPublic)
                {
                    continue;
                }

                if (p.GetCustomAttribute<ObsoleteAttribute>() != null)
                {
                    continue;
                }

                if (IsOverrideMethod(p.SetMethod))
                {
                    var baseProp = FindOverriddenBaseProperty(p);
                    if (baseProp == null || !WasDeclaredWithDeclaringTypeGenerics(baseProp))
                    {
                        continue;
                    }
                }

                included.Add(p);
            }

            var overriddenBaseAccessors = new HashSet<MethodInfo>();
            foreach (var p in included)
            {
                var set = p.SetMethod;
                if (set == null)
                {
                    continue;
                }

                var baseDef = set.GetBaseDefinition();
                if (baseDef != set)
                {
                    overriddenBaseAccessors.Add(baseDef);
                }
            }

            foreach (var t in GetBaseTypes(type))
            {
                var baseDeclared = t.GetProperties(flags | BindingFlags.DeclaredOnly);
                foreach (var bp in baseDeclared)
                {
                    if (!DirectlyTargetsOpenGenericBase(type, bp.DeclaringType))
                    {
                        continue;
                    }

                    if (!WasDeclaredWithDeclaringTypeGenerics(bp))
                    {
                        continue;
                    }

                    if (bp.PropertyType.ContainsGenericParameters ||
                        !bp.CanWrite ||
                        bp.SetMethod == null ||
                        !bp.SetMethod.IsPublic)
                    {
                        continue;
                    }

                    var set = bp.SetMethod;
                    if (set != null && (overriddenBaseAccessors.Contains(set) || IsOverrideMethod(set) || !set.IsPublic))
                    {
                        continue;
                    }

                    included.Add(bp);
                }
            }

            return DistinctBySignature(included);
        }

        public static IEnumerable<EventInfo> GetEvents(Type type)
        {
            var baseEvents = new List<EventInfo>( GetEventsForTypeInternal(type));
            foreach (var t in GetBaseTypes(type))
            {
                baseEvents.AddRange(GetEventsForTypeInternal(t));
            }

            return baseEvents;

            static IEnumerable<EventInfo> GetEventsForTypeInternal(Type type)
            {
                return type
                    .GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .Where(e => e.GetCustomAttribute<ObsoleteAttribute>() == null);
            }
        }

        public static IEnumerable<MethodInfo> GetMethods(Type type)
        {
            var baseMethods = GetMethodsForType(type).ToList();

            if (!IsGenericComplexType(type))
            {
                return baseMethods;
            }

            var comparer = new MethodInfoComparer();
            var parentMethods = GetMethodsForType(type.BaseType);

            foreach (var m in parentMethods)
            {
                if (baseMethods.All(bm => !comparer.Equals(bm, m)))
                {
                    baseMethods.Add(m);
                }
            }

            return baseMethods;
        }

        public static string GetFullTypeNameWithNesting(Type type)
        {
            if (type == null)
                return string.Empty;

            if (type.IsGenericTypeParameter)
            {
                return type.Name;
            }

            // For non-nested types, use the regular type name handling
            if (!type.IsNested)
            {
                return $"{GetFullTypeName(type)}";
            }

            // For nested types, we need to include the declaring type
            StringBuilder nameBuilder = new();

            // Build the nesting hierarchy
            List<Type> typeHierarchy = new();
            Type currentType = type;

            // Traverse up the declaring type hierarchy
            while (currentType != null)
            {
                typeHierarchy.Add(currentType);
                currentType = currentType.DeclaringType;
            }

            // Build the name in reverse order (outermost to innermost)
            for (int i = typeHierarchy.Count - 1; i >= 0; i--)
            {
                nameBuilder.Append(GetFullTypeName(typeHierarchy[i]));

                if (i > 0)
                    nameBuilder.Append('.');
            }

            return $"{nameBuilder}";
        }

        public static string GetSafeNonGenericName(Type type, string innerObjectName = "")
        {
            string safeName = type.IsGenericType ? RemoveGenericParameters(type.Name) : type.Name;
            if (!string.IsNullOrEmpty(innerObjectName))
            {
                safeName = innerObjectName + safeName;
            }

            return safeName;
        }

        public static string GetFullTypeName(Type type)
        {
            if (!type.IsGenericType)
            {
                if (type.IsAssignableFrom(typeof(object)))
                {
                    return "System.Object";
                }
                if (type.IsAssignableFrom(typeof(UnityEngine.Object)))
                {
                    return "UnityEngine.Object";
                }

                return type.Name;
            }

            string baseName = RemoveGenericParameters(type.Name);

            var genericArgs = type.GetGenericArguments();
            string genericArgsStr = string.Join(", ", genericArgs.Select(GetFullTypeName));

            return $"{baseName}<{genericArgsStr}>";
        }

        public static HashSet<string> GetTypeNamespaces(Type type)
        {
            if (type == null || string.IsNullOrEmpty(type.Namespace))
            {
                return new HashSet<string>();
            }

            var namespaces = new HashSet<string> { type.Namespace };

            if (type.IsGenericType)
            {
                namespaces.UnionWith(type.GetGenericArguments().SelectMany(GetTypeNamespaces));
            }

            if (type.IsArray)
            {
                namespaces.UnionWith(GetTypeNamespaces(type.GetElementType()));
            }

            return namespaces;
        }

        private static bool IsGenericComplexType(Type type)
        {
            if (type == null)
            {
                return true;
            }

            if (type.ContainsGenericParameters)
            {
                return !type.IsGenericType || type.GetGenericArguments().Length != 1 || HasNestedGenerics(type);
            }

            if (type.IsGenericType && type.Name.Contains("AnonymousType"))
            {
                return true;
            }

            if (type == typeof(object) || type == typeof(System.Dynamic.ExpandoObject))
            {
                return true;
            }

            if (type.IsGenericType)
            {
                var genericArgs = type.GetGenericArguments();
                if (genericArgs.Length > 2)
                {
                    return true;
                }

                foreach (var arg in genericArgs)
                {
                    if (IsGenericComplexType(arg))
                    {
                        return true;
                    }
                }
            }

            return type.IsArray && IsGenericComplexType(type.GetElementType());
        }

        private static bool IsOverrideMethod(MethodInfo method)
        {
            if (method == null || !method.IsVirtual || method.IsFinal)
            {
                return false;
            }

            return method.GetBaseDefinition().DeclaringType != method.DeclaringType;
        }

        private static bool DirectlyTargetsOpenGenericBase(Type type, Type openGenericBase)
        {
            var openGeneric = openGenericBase.IsGenericType ? openGenericBase.GetGenericTypeDefinition() : openGenericBase;
            if (!openGeneric.IsGenericTypeDefinition)
            {
                return false;
            }

            var b = type.BaseType;
            return b is not null
                   && b.IsGenericType
                   && b.GetGenericTypeDefinition() == openGeneric;
        }

        private static bool HasNestedGenerics(Type type)
        {
            return type.GetGenericArguments().Any(arg => arg.IsGenericType || arg.GetGenericArguments().Length > 0);
        }

        private static string RemoveGenericParameters(string name)
        {
            int backtickIndex = name.IndexOf('`');
            if (backtickIndex > 0)
            {
                name = name[..backtickIndex];
            }

            return name;
        }

        private static IEnumerable<Type> GetBaseTypes(Type type)
        {
            for (var current = type.BaseType; current != null; current = current.BaseType)
            {
                yield return current;
            }
        }

        private static PropertyInfo FindOverriddenBaseProperty(PropertyInfo overrideProp)
        {
            var acc = overrideProp.GetMethod ?? overrideProp.SetMethod;
            if (acc == null)
            {
                return null;
            }

            var baseAcc = acc.GetBaseDefinition();
            if (baseAcc == acc)
            {
                return null;
            }

            return baseAcc.DeclaringType?.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .FirstOrDefault(p =>
                    (p.GetMethod != null && p.GetMethod == baseAcc) ||
                    (p.SetMethod != null && p.SetMethod == baseAcc));
        }

        private static bool WasDeclaredWithDeclaringTypeGenerics(PropertyInfo prop)
        {
            var declaring = prop.DeclaringType;
            if (declaring == null)
            {
                return false;
            }

            if (!declaring.IsGenericType && !declaring.IsGenericTypeDefinition)
            {
                return false;
            }

            var openDeclaring = declaring.IsGenericTypeDefinition ? declaring : declaring.GetGenericTypeDefinition();
            var openProp = FindMatchingPropertyOnOpenType(openDeclaring, prop);
            if (openProp == null)
            {
                return false;
            }

            var classParams = openDeclaring.GetGenericArguments();
            return UsesAnyOf(openProp.PropertyType, classParams);
        }

        private static PropertyInfo FindMatchingPropertyOnOpenType(Type openDeclaring, PropertyInfo closedProp)
        {
            string name = closedProp.Name;
            int arity = closedProp.GetIndexParameters().Length;
            return openDeclaring
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(p => p.Name == name && p.GetIndexParameters().Length == arity);
        }

        private static bool UsesAnyOf(Type type, Type[] targets)
        {
            if (type.IsGenericParameter)
            {
                return targets.Contains(type);
            }

            if (type.HasElementType)
            {
                return UsesAnyOf(type.GetElementType()!, targets);
            }

            if (!type.IsGenericType && !type.ContainsGenericParameters)
            {
                return false;
            }

            return type.GetGenericArguments().Any(arg => UsesAnyOf(arg, targets));
        }

        private static IEnumerable<PropertyInfo> DistinctBySignature(IEnumerable<PropertyInfo> props)
            => props.GroupBy(p => (p.DeclaringType, p.Name, Arity: p.GetIndexParameters().Length))
                .Select(g => g.First());

        private static IEnumerable<MethodInfo> GetMethodsForType(Type type)
        {
            return type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(m => m.ReturnType == typeof(void))
                .Where(m => !m.IsAbstract)
                .Where(m => !m.IsVirtual || m.GetBaseDefinition().DeclaringType != m.DeclaringType)
                .Where(m => !(m.Name.StartsWith("set_") ||
                              m.Name.StartsWith("get_") ||
                              m.Name.StartsWith("add_") ||
                              m.Name.StartsWith("remove_")))
                .Where(m => m.GetCustomAttribute<ObsoleteAttribute>() == null);
        }
    }
}
