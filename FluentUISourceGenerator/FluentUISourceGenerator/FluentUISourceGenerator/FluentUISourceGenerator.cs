namespace FluentUISourceGenerator;

using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Builder;
using Utils;

[Generator]
public class FluentUISourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext ctx)
    {
        InitializeExtensionsGeneration(ctx);
        InitializePropertiesGeneration(ctx);
        InitializeDataBindingGeneration(ctx);
        InitializeStyleGeneration(ctx);

        OutputBindingsCache(ctx);
    }

    private static void InitializeExtensionsGeneration(IncrementalGeneratorInitializationContext ctx)
    {
        var elements = ctx.CompilationProvider.Select(SelectVisualElementsSymbols);
        ctx.RegisterSourceOutput(elements, RegisterVisualElementsOutput);
    }

    private static void InitializePropertiesGeneration(IncrementalGeneratorInitializationContext ctx)
    {
        var properties = ctx.CompilationProvider.Select(SelectPropertiesSymbols);
        var styleProperties = ctx.CompilationProvider.Select(SelectStylePropertiesSymbol);

        ctx.RegisterSourceOutput(properties.Combine(styleProperties), RegisterPropertiesOutput);
    }

    private static void InitializeDataBindingGeneration(IncrementalGeneratorInitializationContext ctx)
    {
        var dataBinding = ctx.CompilationProvider.Select(SelectDataBindingSymbols);
        ctx.RegisterSourceOutput(dataBinding, RegisterDataBindingOutput);
    }

    private static void InitializeStyleGeneration(IncrementalGeneratorInitializationContext ctx)
    {
        var style = ctx.CompilationProvider.Select(SelectStyleSymbol);
        ctx.RegisterSourceOutput(style, RegisterStyleOutput);
    }

    private static void OutputBindingsCache(IncrementalGeneratorInitializationContext ctx)
    {
        ctx.RegisterPostInitializationOutput(pi =>
        {
            pi.AddSource("BindingsCache.custom.g.cs", Templates.BindingsCacheTemplate.Render(new EmptyData()));
        });
    }

    private static ImmutableArray<INamedTypeSymbol> SelectVisualElementsSymbols(Compilation compilation, CancellationToken _)
    {
        var visualElementReference = compilation.GetTypeByMetadataName("UnityEngine.UIElements.VisualElement");
        if (visualElementReference == null)
        {
            return ImmutableArray<INamedTypeSymbol>.Empty;
        }

        return TypeUtils.CollectTypes(visualElementReference.ContainingNamespace)
            .Where(t => t.TypeKind == TypeKind.Class &&
                        t.DeclaredAccessibility == Accessibility.Public &&
                        TypeUtils.InheritsFrom(t, visualElementReference))
            .ToImmutableArray();
    }

    private static ImmutableArray<IPropertySymbol> SelectPropertiesSymbols(Compilation compilation, CancellationToken cancellationToken)
    {
        var referenceType = compilation.GetTypeByMetadataName("UnityEngine.UIElements.VisualElement");
        if (referenceType == null)
        {
            return ImmutableArray<IPropertySymbol>.Empty;
        }

        return SelectVisualElementsSymbols(compilation, cancellationToken)
            .SelectMany(s => s.GetMembers().OfType<IPropertySymbol>()
                .Where(TypeUtils.HasCreatePropertyAttribute))
            .GroupBy(p => p.Name)
            .Select(g => g.First())
            .ToImmutableArray();
    }

    private static ImmutableArray<IPropertySymbol> SelectStylePropertiesSymbol(Compilation compilation, CancellationToken _)
    {
        var style = compilation.GetTypeByMetadataName("UnityEngine.UIElements.IStyle");
        if (style == null)
        {
            return ImmutableArray<IPropertySymbol>.Empty;
        }

        return BuilderUtils.GetAllStyleProperties(style).ToImmutableArray();
    }

    private static INamedTypeSymbol? SelectDataBindingSymbols(Compilation compilation, CancellationToken _)
    {
        return compilation.GetTypeByMetadataName("UnityEngine.UIElements.DataBinding");
    }

    private static INamedTypeSymbol? SelectStyleSymbol(Compilation compilation, CancellationToken _)
    {
        return compilation.GetTypeByMetadataName("UnityEngine.UIElements.IStyle");
    }

    private static void RegisterVisualElementsOutput(SourceProductionContext spc, ImmutableArray<INamedTypeSymbol> types)
    {
        foreach (var t in types)
        {
            var source = ClassSourceTextGenerator.CreateSourceTextFromBuilder(t, createBindings: true);
            if (source == null)
            {
                continue;
            }

            spc.AddSource($"FluentUI{t.Name}Extensions.g.cs", source);
        }

        spc.AddSource($"FluentUINotifyValueChangedExtensions.g.cs", ClassSourceTextGenerator.CreateSourceTextForCustomClass("INotifyValueChanged"));
    }

    private static void RegisterPropertiesOutput(SourceProductionContext spc, (ImmutableArray<IPropertySymbol>, ImmutableArray<IPropertySymbol>) properties)
    {
        var (props, styleProps) = properties;

        spc.AddSource("FluentUIProperties.g.cs", PropertiesSourceTextGenerator.CreateSourceTextFromProperties(props, styleProps));
    }

    private static void RegisterDataBindingOutput(SourceProductionContext spc, INamedTypeSymbol? dataBinding)
    {
        if (dataBinding == null)
        {
            return;
        }

        var source = ClassSourceTextGenerator.CreateSourceTextFromBuilder(dataBinding, createBindings: false);
        if (source == null)
        {
            return;
        }

        spc.AddSource("FluentUIDataBindingExtensions.g.cs", source);
    }

    private static void RegisterStyleOutput(SourceProductionContext spc, INamedTypeSymbol? styleSymbol)
    {
        if (styleSymbol == null)
        {
            return;
        }

        spc.AddSource(
            "FluentUIStyleExtensions.g.cs",
            StyleSourceTextGenerator.CreateSourceTextFromStyles(styleSymbol)
        );
    }
}
