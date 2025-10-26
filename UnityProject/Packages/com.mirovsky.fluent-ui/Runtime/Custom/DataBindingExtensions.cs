/*
using System;
using FluentUI.Bindings;
using Unity.Properties;
using UnityEngine.UIElements;

namespace FluentUI
{
    public static class DataBindingExtensions
    {
        public static T LocalDataBinding<T>(this T t, Action<DataBindingFluentWrapper> binding) where T : VisualElement
        {
            binding.Invoke(new DataBindingFluentWrapper(t));

            return t;
        }

        public static T BindingToTarget<T>(this T t, string property, string propertyPathString) where T : VisualElement
        {
            t.Binding(property, new DataBinding().DataSourcePath(new PropertyPath(propertyPathString)));

            return t;
        }

        public static T BindingToTarget<T>(this T t, string property, object dataSource, string propertyPathString) where T : VisualElement
        {
            t.Binding(property, DataBindingFactory.ToTarget(dataSource, propertyPathString));

            return t;
        }

        public static T BindingToTargetWithConverter<T, TSource, TDestination>(
            this T t,
            string property,
            string dataSourcePath,
            TypeConverter<TSource, TDestination> converter)
            where T : VisualElement
        {

            var binding = new DataBinding
            {
                dataSourcePath = new PropertyPath(dataSourcePath)
            };
            binding.sourceToUiConverters.AddConverter(converter);

            t.Binding(property, binding);

            return t;
        }

        public static T DisplayFlexIf<T>(this T t, string dataSourcePath) where T : VisualElement
        {
            t.BindingToTargetWithConverter(
                "style.display",
                dataSourcePath,
                (ref bool value) => new StyleEnum<DisplayStyle>(value ? DisplayStyle.Flex : DisplayStyle.None)
            );

            return t;
        }

        public static T ClassesIf<T>(this T t, BindingId bindingId, params string[] classes) where T : VisualElement
        {
            t.Binding(bindingId, new ToggleClassBinding(classes));

            return t;
        }
    }
}
*/
