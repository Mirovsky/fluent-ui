namespace FluentUISourceGenerator.Builder;

public static partial class Templates
{
    public const string BindingsCacheTemplate =
        """
        #nullable enable
        
        internal static class BindingsRepository
        {
            private static readonly System.Collections.Generic.Dictionary<int, UnityEngine.UIElements.DataBinding> s_bindingsCache = new();
        
            public static UnityEngine.UIElements.DataBinding GetCachedOrCreateBinding(
                string property,
                object? localDataSource,
                UnityEngine.UIElements.BindingMode bindingMode)
            {
                int hash = System.HashCode.Combine(property, bindingMode);
        
                if (localDataSource != null)
                {
                    hash ^= localDataSource.GetHashCode();
                }
        
                if (s_bindingsCache.TryGetValue(hash, out var binding))
                {
                    return binding;
                }
        
                binding = new UnityEngine.UIElements.DataBinding()
                {
                    dataSourcePath = new Unity.Properties.PropertyPath(property),
                    bindingMode = bindingMode
                };
        
                if (localDataSource != null)
                {
                    binding.dataSource = localDataSource;
                }
                s_bindingsCache.Add(hash, binding);
        
                return binding;
            }
        }
        """;
}
