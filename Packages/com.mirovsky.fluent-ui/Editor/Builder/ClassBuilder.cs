using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluentUI.Generator
{
    public class ClassBuilder
    {
        public string Namespace { get; private set; }
        public HashSet<string> Usings { get; private set; }
        public Type Type { get; private set; }
        public string InnerObjectName { get; private set; }
        public Type[] GenericTypeParameters { get; private set; } =  Array.Empty<Type>();

        public List<MethodInfo> Methods { get; } = new();
        public List<PropertyInfo> Properties { get; }= new();
        public List<EventInfo> Events { get; }= new();

        public void SetNamespace(string namespaceName)
        {
            Namespace = namespaceName;
        }

        public void SetUsings(HashSet<string> usings)
        {
            Usings = usings;
        }

        public void SetType(Type type)
        {
            Type = type;
        }

        public void SetInnerObjectName(string innerObjectName)
        {
            InnerObjectName = innerObjectName;
        }

        public void SetGenericTypeParameters(Type[] genericTypeParameters)
        {
            GenericTypeParameters = genericTypeParameters;
        }

        public void AddMethodForMethod(MethodInfo methodInfo)
        {
            Methods.Add(methodInfo);
        }

        public void AddMethodForProperty(PropertyInfo propertyInfo)
        {
            Properties.Add(propertyInfo);
        }

        public void AddMethodForEvent(EventInfo eventInfo)
        {
            Events.Add(eventInfo);
        }

        public bool NeedsWriting()
        {
            return Methods.Count > 0 || Properties.Count > 0 || Events.Count > 0;
        }
    }
}
