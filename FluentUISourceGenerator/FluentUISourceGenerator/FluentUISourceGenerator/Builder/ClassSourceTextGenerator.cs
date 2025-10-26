using Microsoft.CodeAnalysis;

namespace FluentUISourceGenerator.Builder;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.Text;
using Utils;

public static class ClassSourceTextGenerator
{
    public static SourceText? CreateSourceTextFromBuilder(INamedTypeSymbol type, bool createBindings)
    {
        string content = BuilderUtils.Format(CreateStringFromBuilder(type, createBindings));
        if (string.IsNullOrEmpty(content))
        {
            return null;
        }

        return SourceText.From(content, Encoding.UTF8);
    }

    public static SourceText CreateSourceTextForCustomClass(string templateName)
    {
        var classData = new ClassData(
            "FluentUI",
            "FluentUI",
            templateName,
            "Extensions",
            "",
            CustomTemplates.GetCustomData(templateName)
        );

        return SourceText.From(
            BuilderUtils.Format(Templates.ClassTemplate.Render(classData)),
            Encoding.UTF8
        );
    }

    private static string CreateStringFromBuilder(INamedTypeSymbol type, bool createBindings)
    {
        var methods = BuilderUtils.GetAllMethods(type).ToArray();
        var properties = BuilderUtils.GetAllProperties(type).ToArray();
        var events = BuilderUtils.GetAllEvents(type).ToArray();

        if (methods.Length == 0 && properties.Length == 0 && events.Length == 0)
        {
            return string.Empty;
        }

        var renderedMethods = new List<string>()
            .Concat(methods
                .Select(p => MethodData.FromSymbol(type, p))
                .Select(m => Templates.MethodForMethodTemplate.Render(m)))
            .Concat(properties
                .Select(p => PropertyData.FromSymbol(type, p))
                .Select(p => Templates.MethodForPropertyTemplate.Render(p)))
            .Concat(events
                .Select(p => EventData.FromSymbol(type, p))
                .Select(e => Templates.MethodForEventTemplate.Render(e)));

        if (createBindings)
        {
            renderedMethods = renderedMethods.Concat(properties
                    .Where(TypeUtils.HasCreatePropertyAttribute)
                    .Select(p => PropertyData.FromSymbol(type, p))
                    .Select(p => Templates.GenericBindMethodForPropertyTemplate.Render(p)))
                .Concat(properties
                    .Where(TypeUtils.HasCreatePropertyAttribute)
                    .Select(p => PropertyData.FromSymbol(type, p))
                    .Select(p => Templates.BindMethodForPropertyTemplate.Render(p)));
        }

        var classData = new ClassData(
            "FluentUI",
            "FluentUI",
            type.Name,
            "Extensions",
            string.Join("\n", renderedMethods),
            CustomTemplates.GetCustomData(type.Name)
        );

        return Templates.ClassTemplate.Render(classData);
    }
}
