/*
using System;
using Unity.Properties;
using UnityEngine.UIElements;

namespace FluentUI
{
    public class DataBindingFluentWrapper
    {
        private readonly VisualElement _target;

        public DataBindingFluentWrapper(VisualElement target)
        {
            _target = target;
        }

        public DataBindingFluentWrapper LocalSource(object source)
        {
            _target.DataSource(source);

            return this;
        }

        public DataBindingFluentWrapper ToTarget(string property, string propertyPathString)
        {
            _target.Binding(
                property,
                new DataBinding()
                    .BindingMode(BindingMode.ToTarget)
                    .DataSourcePath(new PropertyPath(propertyPathString))
            );

            return this;
        }

        public DataBindingFluentWrapper ToTargetWithSource(string bindingId, object source, string propertyPathString)
        {
            _target.SetBinding(
                bindingId,
                new DataBinding()
                    .BindingMode(BindingMode.ToTarget)
                    .DataSource(source)
                    .DataSourcePath(new PropertyPath(propertyPathString))
            );

            return this;
        }
    }
}
*/
