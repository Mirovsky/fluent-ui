namespace FluentUISourceGenerator.Builder;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Utils;

public static class StyleSourceTextGenerator
{
    public static SourceText CreateSourceTextFromStyles(INamedTypeSymbol styleProperties)
    {
        return SourceText.From(BuilderUtils.Format(CreateStringFromProperties(styleProperties)), Encoding.UTF8);
    }

    private static string CreateStringFromProperties(INamedTypeSymbol style)
    {
        var properties = BuilderUtils.GetAllStyleProperties(style).ToArray();

        var content = new List<string>()
            .Concat(properties
                .Select(p => PropertyData.FromSymbol(style, p))
                .Select(p => Templates.MethodForPropertyTemplate.Render(p)))
            .Concat(properties
                .Select(PropertyData.FromBindStyleSymbol)
                .Select(p => Templates.GenericBindMethodForPropertyTemplate.Render(p)))
            .Concat(properties
                .Select(PropertyData.FromBindStyleSymbol)
                .Select(p => Templates.BindMethodForPropertyTemplate.Render(p)))
            .Concat([Templates.MethodForStylePropertyTemplate]);

        var classData = new ClassData(
            "FluentUI",
            "FluentUI",
            "Style",
            "Extensions",
            string.Join("\n", content)
        );

        return Templates.ClassTemplate.Render(classData);
    }
}
