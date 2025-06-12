using System.Collections.Generic;
using System.Reflection;

namespace FluentUI.Generator
{
    using System;

    public class PropertiesBuilder
    {
        public string Namespace { get; private set; }

        public readonly List<string> FieldNames = new();

        private readonly HashSet<string> _existingFields = new();

        public void SetNamespace(string namespaceName)
        {
            Namespace = namespaceName;
        }

        public void AddField(FieldInfo fieldInfo, Type parentType)
        {
            string fieldName = parentType.Name.Contains("Style")
                ? GeneratorNamingUtils.GetPropertyName(fieldInfo.Name, "Style")
                : fieldInfo.Name;

            if (_existingFields.Add(fieldName))
            {
                FieldNames.Add(fieldName);
            }
        }

        public bool HasProperty(PropertyInfo propertyInfo, string innerObjectName)
        {
            return _existingFields.Contains(GeneratorNamingUtils.GetPropertyName(propertyInfo.Name, innerObjectName) + "Property");
        }
    }
}
