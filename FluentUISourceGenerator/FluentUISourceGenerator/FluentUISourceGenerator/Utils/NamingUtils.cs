namespace FluentUISourceGenerator.Utils;

using System;
using Microsoft.CodeAnalysis;

public static class NamingUtils
{
    public static string CreateTName(ISymbol symbol)
    {
        var containingType = symbol.ContainingType;
        return containingType != null ? $"T{containingType.Name}" : $"T{symbol.Name}";
    }

    public static string CreateFluentName(string name)
    {
        return FirstCharToUpper(name.Replace("Set", ""));
    }

    public static string CreateStyleName(string name)
    {
        return "style" + FirstCharToUpper(name.Replace("Property", ""));
    }

    public static string CreateSimpleName(IEventSymbol symbol)
    {
        return symbol.ExplicitInterfaceImplementations.Length > 0
            ? symbol.ExplicitInterfaceImplementations[0].Name
            : symbol.Name;
    }

    private static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => input[0].ToString().ToUpper() + input.Substring(1)
        };
}
