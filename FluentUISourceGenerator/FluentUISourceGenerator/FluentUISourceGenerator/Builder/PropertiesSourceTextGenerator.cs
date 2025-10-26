namespace FluentUISourceGenerator.Builder;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Utils;

public static class PropertiesSourceTextGenerator
{
    public static SourceText CreateSourceTextFromProperties(
        IEnumerable<IPropertySymbol> properties,
        IEnumerable<IPropertySymbol> styleProperties
    ) {
        return SourceText.From(BuilderUtils.Format(CreateStringFromProperties(properties, styleProperties)), Encoding.UTF8);
    }

    private static string CreateStringFromProperties(
        IEnumerable<IPropertySymbol> properties,
        IEnumerable<IPropertySymbol> styleProperties
    )
    {
        var content = new List<string>()
            .Concat(properties
                .Select(BindingData.FromSymbol)
                .Select(d => Templates.BindingTemplate.Render(d)))
            .Concat(styleProperties
                .Select(BindingData.FromStyleSymbol)
                .Select(d => Templates.BindingTemplate.Render(d))
            );


        var classData = new ClassData(
            "FluentUI",
            "",
            "Properties",
            "",
            string.Join("\n", content)
        );

        return Templates.ClassTemplate.Render(classData);
    }
}
