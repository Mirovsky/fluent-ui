using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace FluentUI.Generator
{
    public static class Generator
    {
        [MenuItem("Assets/Fluent UI/Generate Extension Methods")]
        public static void GenerateVisualElementExtensions()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var uiElementsAssembly = assemblies.FirstOrDefault(a => a.GetName().Name.Contains("UnityEngine.UIElementsModule"));
            Assert.IsNotNull(uiElementsAssembly, "No assemblies with 'UIElements' in name were found.");

            var propertiesBuilder = GetPropertiesBuilder(uiElementsAssembly);

            var visualElementType = typeof(VisualElement);

            var subclasses = uiElementsAssembly.GetTypes()
                .Where(t => (t == visualElementType || visualElementType.IsAssignableFrom(t)) &&
                            !t.IsInterface &&
                            (t.IsPublic || t.IsNestedPublic))
                .OrderBy(t => t.Name)
                .ToList();

            foreach (var s in subclasses)
            {
                GenerateExtensions(s, propertiesBuilder);
            }

            GenerateExtensions(typeof(DataBinding), propertiesBuilder);
            GenerateExtensions(typeof(Manipulator), propertiesBuilder);
            GenerateStyleExtensions(propertiesBuilder);
            GenerateProperties(propertiesBuilder);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void GenerateExtensions(Type type, PropertiesBuilder propertiesBuilder)
        {
            var methods = GeneratorTypeUtils.GetMethods(type).ToArray();
            var properties = GeneratorTypeUtils.GetProperties(type).ToArray();
            var events = GeneratorTypeUtils.GetEvents(type).ToArray();

            var allUsedTypes = new HashSet<Type> {type};
            allUsedTypes.UnionWith(methods.SelectMany(m => m.GetParameters().Select(p => p.ParameterType)));
            allUsedTypes.UnionWith(properties.Select(p => p.PropertyType));
            allUsedTypes.UnionWith(events.Select(e => e.EventHandlerType));

            var usings = new HashSet<string>(allUsedTypes.SelectMany(GeneratorTypeUtils.GetTypeNamespaces)) { "System" };

            var builder = new ClassBuilder();

            builder.SetNamespace("FluentUI");
            builder.SetUsings(usings);
            builder.SetType(type);

            if (type.IsGenericType)
            {
                builder.SetGenericTypeParameters(type.GetGenericArguments());
            }

            foreach (MethodInfo method in methods)
            {
                builder.AddMethodForMethod(method);
            }

            foreach (var property in properties)
            {
                builder.AddMethodForProperty(property);
            }

            foreach (var eventInfo in events)
            {
                builder.AddMethodForEvent(eventInfo);
            }

            if (!builder.NeedsWriting())
            {
                return;
            }

            var writer = new ClassWriter(builder, propertiesBuilder);

            string path = Path.GetFullPath("Packages/com.mirovsky.fluent-ui-toolkit/Runtime/Generated");
            File.WriteAllText(
                Path.Combine(path, $"{GeneratorTypeUtils.GetSafeNonGenericName(type)}Extensions.g.cs"),
                writer.Write()
            );
        }

        private static void GenerateStyleExtensions(PropertiesBuilder propertiesBuilder)
        {
            var type = typeof(IStyle);

            var properties = GeneratorTypeUtils.GetProperties(type).ToArray();

            var allUsedTypes = new HashSet<Type> {type};
            allUsedTypes.UnionWith(properties.Select(p => p.PropertyType));

            var usings = new HashSet<string>(allUsedTypes.SelectMany(GeneratorTypeUtils.GetTypeNamespaces)) { "System" };

            var builder = new ClassBuilder();

            builder.SetNamespace("FluentUI");
            builder.SetUsings(usings);
            builder.SetType(typeof(VisualElement));
            builder.SetInnerObjectName("Style");

            foreach (var property in properties)
            {
                builder.AddMethodForProperty(property);
            }

            if (!builder.NeedsWriting())
            {
                return;
            }

            var writer = new ClassWriter(builder, propertiesBuilder);

            string path = Path.GetFullPath("Packages/com.mirovsky.fluent-ui-toolkit/Runtime/Generated");
            File.WriteAllText(
                Path.Combine(path, "StyleVisualElementExtensions.g.cs"),
                writer.Write()
            );
        }

        private static void GenerateProperties(PropertiesBuilder propertiesBuilder)
        {
            var writer = new PropertiesWriter(propertiesBuilder);

            string path = Path.GetFullPath("Packages/com.mirovsky.fluent-ui-toolkit/Runtime/Generated");
            File.WriteAllText(
                Path.Combine(path, "Properties.g.cs"),
                writer.Write()
            );

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static PropertiesBuilder GetPropertiesBuilder(Assembly uiElementsAssembly)
        {
            var propertiesBuilder = new PropertiesBuilder();
            propertiesBuilder.SetNamespace("FluentUI");

            var types = uiElementsAssembly.GetTypes();
            foreach (var type in types)
            {
                var fields = type
                    .GetFields(BindingFlags.Static | BindingFlags.NonPublic)
                    .Where(f => f.FieldType == typeof(BindingId));
                foreach (var f in fields)
                {
                    propertiesBuilder.AddField(f, type);
                }
            }

            return propertiesBuilder;
        }
    }
}
