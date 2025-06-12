namespace FluentUI.Generator
{
    using System.Collections.Generic;
    using UnityEngine;

    public class WriterBlacklist
    {
        private readonly HashSet<string> _classBlacklist = new()
        {
            "DataBinding",
            "IStyle",
            "Manipulator",
            "Bindable"
        };

        private readonly HashSet<string> _propertiesBlacklist = new()
        {
            "clickable"
        };

        public bool IsClassBlacklisted(string className) => _classBlacklist.Contains(className);

        public bool IsPropertyBlacklisted(System.Reflection.PropertyInfo propertyInfo)
        {
            string propertyType = GeneratorTypeUtils.GetFullTypeNameWithNesting(propertyInfo.PropertyType);

            return _propertiesBlacklist.Contains(propertyType);
        }
    }
}
