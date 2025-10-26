namespace FluentUISourceGenerator.Builder;

using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Utils;

public interface ITemplateData
{
    Dictionary<string, string> ToDictionary();
}

public readonly struct EmptyData : ITemplateData
{
    public Dictionary<string, string> ToDictionary() => new();
}

public readonly struct ClassData(
    string @namespace,
    string classPrefix,
    string className,
    string classPostfix,
    string content,
    string customContent = ""
) : ITemplateData
{
    public Dictionary<string, string> ToDictionary() =>
        new()
        {
            ["NAMESPACE"] = @namespace,
            ["CLASS_PREFIX"] = classPrefix,
            ["CLASS_NAME"] = className,
            ["CLASS_POSTFIX"] = classPostfix,
            ["CONTENT"] = content,
            ["CUSTOM_CONTENT"] = customContent,
        };
}

public readonly struct MethodData(
    string tName,
    string methodName,
    string generics,
    string constraints,
    string parameters,
    string calledMethodName,
    string calledArguments
) : ITemplateData
{

    public Dictionary<string, string> ToDictionary() =>
        new()
        {
            ["T_NAME"] = tName,
            ["METHOD_NAME"] = methodName,
            ["GENERIC_ARGUMENTS"] = generics,
            ["METHOD_PARAMETERS"] = parameters,
            ["METHOD_CONSTRAINTS"] = constraints,
            ["CALLED_METHOD_NAME"] = calledMethodName,
            ["CALLED_METHOD_ARGUMENTS"] = calledArguments,
        };

    public static MethodData FromSymbol(INamedTypeSymbol type, IMethodSymbol s) => new(
        tName: NamingUtils.CreateTName(type),
        methodName: NamingUtils.CreateFluentName(s.Name),
        generics: TemplateUtils.CreateGenericsForMethod(s),
        parameters: TemplateUtils.CreateParametersForMethod(s),
        constraints: TemplateUtils.CreateConstraintsForSymbol(type),
        calledMethodName: s.Name,
        calledArguments: TemplateUtils.CreateArgumentsForMethod(s)
    );
}

public readonly struct PropertyData(
    string tName,
    string methodName,
    string generics,
    string constraints,
    string propertyType,
    string propertyName
) : ITemplateData
{
    public Dictionary<string, string> ToDictionary() =>
        new()
        {
            ["T_NAME"] = tName,
            ["METHOD_NAME"] = methodName,
            ["GENERIC_ARGUMENTS"] = generics,
            ["METHOD_CONSTRAINTS"] = constraints,
            ["PROPERTY_TYPE"] = propertyType,
            ["PROPERTY_NAME"] = propertyName
        };

    public static PropertyData FromSymbol(INamedTypeSymbol type, IPropertySymbol s) => new(
        tName: NamingUtils.CreateTName(type),
        methodName: NamingUtils.CreateFluentName(s.Name),
        generics: TemplateUtils.CreateGenericsForSymbol(type, s),
        constraints: TemplateUtils.CreateConstraintsForSymbol(type),
        propertyType: s.Type.ToDisplayString(),
        propertyName: s.Name
    );

    public static PropertyData FromStyleSymbol(IPropertySymbol p) => new(
        tName: "TVisualElement",
        methodName: NamingUtils.CreateFluentName(p.Name),
        generics: "TVisualElement",
        constraints: "where TVisualElement : UnityEngine.UIElements.VisualElement",
        propertyType: p.Type.ToDisplayString(),
        propertyName: p.Name
    );

    public static PropertyData FromBindStyleSymbol(IPropertySymbol p) => new(
        tName: "TVisualElement",
        methodName: "Style" + NamingUtils.CreateFluentName(p.Name),
        generics: "TVisualElement",
        constraints: "where TVisualElement : UnityEngine.UIElements.VisualElement",
        propertyType: "",
        propertyName: NamingUtils.CreateStyleName(p.Name)
    );
}

public readonly struct EventData(
    string tName,
    string methodName,
    string generics,
    string constraints,
    string eventType,
    string eventName
) : ITemplateData
{
    public Dictionary<string, string> ToDictionary() =>
        new()
        {
            ["T_NAME"] = tName,
            ["METHOD_NAME"] = methodName,
            ["GENERIC_ARGUMENTS"] = generics,
            ["METHOD_CONSTRAINTS"] = constraints,
            ["EVENT_TYPE"] = eventType,
            ["EVENT_NAME"] = eventName
        };

    public static EventData FromSymbol(INamedTypeSymbol type, IEventSymbol s) => new(
        tName: NamingUtils.CreateTName(type),
        methodName: NamingUtils.CreateFluentName(s.Name),
        generics: TemplateUtils.CreateGenericsForSymbol(type, s),
        constraints: TemplateUtils.CreateConstraintsForSymbol(type),
        eventType: s.Type.ToDisplayString(),
        eventName: s.Name
    );
}

public readonly struct BindingData(
    string fieldName,
    string bindingId
) : ITemplateData
{
    public Dictionary<string, string> ToDictionary() =>
        new()
        {
            ["FIELD_NAME"] = fieldName,
            ["BINDING_ID"] = bindingId
        };

    public static BindingData FromSymbol(IPropertySymbol s) => new(
        fieldName: s.Name.Replace("Property", ""),
        bindingId: s.Name.Replace("Property", "")
    );

    public static BindingData FromStyleSymbol(IPropertySymbol s) => new(
        fieldName: NamingUtils.CreateStyleName(s.Name),
        bindingId: "style." + s.Name.Replace("Property", "")
    );
}
