namespace FluentUISourceGenerator.Utils;

using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public static class BuilderUtils
{
    public static string Format(string code)
    {
        var tree = CSharpSyntaxTree.ParseText(code);
        var root = tree.GetRoot().NormalizeWhitespace(indentation: "    ", elasticTrivia: true);
        return root.ToFullString();
    }

    public static IEnumerable<IMethodSymbol> GetAllMethods(INamedTypeSymbol type)
    {
        return GetMethods(type)
            .Where(m => m.IsStatic == false &&
                        m.DeclaredAccessibility == Accessibility.Public &&
                        m.IsAbstract == false &&
                        m.IsVirtual == false &&
                        TypeUtils.IsMethodDeclaredInThisType(m, type) &&
                        m.ReturnsVoid &&
                        m.MethodKind == MethodKind.Ordinary &&
                        m.MethodKind != MethodKind.Constructor &&
                        TypeUtils.HasObsoleteAttribute(m) == false
            );
    }

    public static IEnumerable<IPropertySymbol> GetAllProperties(INamedTypeSymbol type)
    {
        return GetProperties(type)
            .Where(p => p.IsStatic == false &&
                        p.DeclaredAccessibility == Accessibility.Public &&
                        p.IsIndexer == false &&
                        p.SetMethod != null &&
                        p.SetMethod.DeclaredAccessibility == Accessibility.Public &&
                        TypeUtils.HasObsoleteAttribute(p) == false &&
                        TypeUtils.IsPropertyDeclaredOrClosedByThisType(p, type)
            );
    }

    public static IEnumerable<IEventSymbol> GetAllEvents(INamedTypeSymbol type)
    {
        return GetEvents(type)
            .Where(e => e.IsStatic == false &&
                        e.AddMethod != null &&
                        TypeUtils.HasObsoleteAttribute(e) == false
            );
    }

    public static IEnumerable<IPropertySymbol> GetAllStyleProperties(INamedTypeSymbol type)
    {
        return GetDeclaredProperties(type)
            .Where(p => TypeUtils.HasObsoleteAttribute(p) == false);
    }

    private static IEnumerable<IMethodSymbol> GetMethods(INamedTypeSymbol type)
    {
        var current = type;
        while (current != null)
        {
            foreach (var m in GetDeclaredMethods(current))
            {
                yield return m;
            }

            current = current.BaseType;
        }
    }

    private static IEnumerable<IMethodSymbol> GetDeclaredMethods(INamedTypeSymbol current)
    {
        return current.GetMembers().OfType<IMethodSymbol>();
    }

    private static IEnumerable<IEventSymbol> GetEvents(INamedTypeSymbol type)
    {
        return type.GetMembers().OfType<IEventSymbol>();
    }

    private static IEnumerable<IPropertySymbol> GetProperties(INamedTypeSymbol type)
    {
        var dictionary = new Dictionary<string, IPropertySymbol>();

        var current = type;
        while (current != null)
        {
            foreach (var p in GetDeclaredProperties(current))
            {
                if (dictionary.TryGetValue(p.Name, out var prevProperty) == false)
                {
                    dictionary.Add(p.Name, p);
                    continue;
                }

                if (p.IsVirtual)
                {
                    dictionary[p.Name] = p;
                    continue;
                }
            }

            current = current.BaseType;
        }

        return dictionary.Values;
    }

    private static IEnumerable<IPropertySymbol> GetDeclaredProperties(INamedTypeSymbol type)
    {
        return type.GetMembers().OfType<IPropertySymbol>();
    }
}
