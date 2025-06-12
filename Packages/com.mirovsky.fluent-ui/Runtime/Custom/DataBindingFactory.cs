using Unity.Properties;
using UnityEngine.UIElements;

namespace FluentUI
{
    public static class DataBindingFactory
    {
        public static DataBinding ToTarget(object dataSource, string propertyPathString)
        {
            return new DataBinding()
                .DataSourcePath(new PropertyPath(propertyPathString))
                .DataSource(dataSource)
                .BindingMode(BindingMode.ToTarget);
        }

        public static DataBinding ToTarget(string propertyPathString)
        {
            return new DataBinding()
                .DataSourcePath(new PropertyPath(propertyPathString))
                .BindingMode(BindingMode.ToTarget);
        }
    }
}
