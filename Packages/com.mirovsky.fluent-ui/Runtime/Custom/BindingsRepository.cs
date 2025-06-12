namespace FluentUI
{
    using System;
    using System.Collections.Generic;
    using Unity.Properties;
    using UnityEngine.UIElements;

    public static class BindingsRepository
    {
        private static readonly Dictionary<int, DataBinding> BindingsCache = new();

        public static DataBinding GetCachedOrCreateBinding(
            String property,
            object? localDataSource,
            BindingMode bindingMode)
        {
            int hash = HashCode.Combine(property, bindingMode);

            if (localDataSource != null)
            {
                hash ^= localDataSource.GetHashCode();
            }

            if (BindingsCache.TryGetValue(hash, out var binding))
            {
                return binding;
            }

            binding = new DataBinding()
                .DataSourcePath(new PropertyPath(property))
                .BindingMode(bindingMode);

            if (localDataSource != null)
            {
                binding.DataSource(localDataSource);
            }
            BindingsCache.Add(hash, binding);

            return binding;
        }
    }
}
