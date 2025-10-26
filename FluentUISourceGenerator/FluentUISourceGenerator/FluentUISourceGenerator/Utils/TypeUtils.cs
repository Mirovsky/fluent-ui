namespace FluentUISourceGenerator.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

public static class TypeUtils
{
    public static bool InheritsFrom(INamedTypeSymbol type, INamedTypeSymbol baseType)
    {
        for (var t = type.BaseType; t != null; t = t.BaseType)
        {
            if (SymbolEqualityComparer.Default.Equals(t, baseType))
            {
                return true;
            }
        }

        return SymbolEqualityComparer.Default.Equals(type, baseType);
    }

    public static List<INamedTypeSymbol> CollectTypes(INamespaceSymbol ns)
    {
        var list = ns.GetTypeMembers().ToList();

        foreach (var child in ns.GetNamespaceMembers())
        {
            list.AddRange(CollectTypes(child));
        }

        return list;
    }

    public static bool HasObsoleteAttribute(ISymbol symbol)
    {
        return symbol.GetAttributes()
            .Any(a => a.AttributeClass != null && a.AttributeClass.Name == "ObsoleteAttribute");
    }

    public static bool HasCreatePropertyAttribute(ISymbol symbol)
    {
        return symbol.GetAttributes()
            .Any(a => a.AttributeClass != null && a.AttributeClass.Name == "CreatePropertyAttribute");
    }

    public static bool IsPropertyDeclaredOrClosedByThisType(IPropertySymbol property, INamedTypeSymbol namedType)
    {
        bool declaredInThisType = SymbolEqualityComparer.Default.Equals(property.ContainingType, namedType);
        bool wasGeneric = IsGenericType(property.OriginalDefinition.Type);

        // Declared here, and it's not generic, it should be included
        if (declaredInThisType && !wasGeneric && !property.IsOverride)
        {
            return true;
        }

        bool isGeneric = IsGenericType(property.Type);
        // It's declared elsewhere, but it was generic, and now it's not, check if this type is closing this generic constraint
        if (wasGeneric && !isGeneric)
        {
            // Shortcut, if it was generic, and it's not, I assume it was closed by this type
            return namedType.BaseType?.IsGenericType == true && property.IsOverride == false;
        }

        return declaredInThisType && !isGeneric && property.IsOverride == false;
    }

    public static bool IsMethodDeclaredInThisType(IMethodSymbol method, INamedTypeSymbol namedType)
    {
        return SymbolEqualityComparer.Default.Equals(method.ContainingType, namedType);
    }

    public static IEnumerable<string> GetMethodGenericParameters(IMethodSymbol method)
    {
        return method.TypeParameters
            .Select(p => p.Name);
    }

    public static IEnumerable<string> GetContainingTypeGenericParameters(ISymbol symbol)
    {
        var containingType = symbol.ContainingType;
        if (containingType == null || containingType.IsGenericType == false)
        {
            return [];
        }

        return containingType.TypeArguments
            .Where(t => t.TypeKind == TypeKind.TypeParameter)
            .Select(p => p.Name);
    }

    public static string[] GetContainingTypeConstraints(INamedTypeSymbol symbol)
    {
        string tName = NamingUtils.CreateTName(symbol);
        if (symbol.IsGenericType == false)
        {
            return [$"where {tName} : {symbol.ToDisplayString()}"];
        }

        return symbol.TypeParameters
            .Select(CreateConstraintString)
            .Where(s => !string.IsNullOrEmpty(s))
            .Prepend($"where {tName} : {symbol.ToDisplayString()}")
            .ToArray();
    }

    public static string[] GetParametersFromMethod(IMethodSymbol method)
    {
        return method.Parameters
            .Select(p => $"{p.Type.ToDisplayString()} {p.Name}")
            .ToArray();
    }

    public static IEnumerable<string> GetArgumentsFromMethod(IMethodSymbol method)
    {
        return method.Parameters
            .Select(p => p.Name)
            .ToArray();
    }

    private static string CreateConstraintString(ITypeParameterSymbol typeParameter)
    {
        var constraints = new List<string>();

        if (typeParameter.HasReferenceTypeConstraint)
        {
            string suffix = typeParameter.ReferenceTypeConstraintNullableAnnotation switch
            {
                NullableAnnotation.Annotated     => "?",
                _                                => ""
            };

            constraints.Add("class" + suffix);
        }

        if (typeParameter.HasValueTypeConstraint)
        {
            constraints.Add("struct");
        }

        if (typeParameter.HasUnmanagedTypeConstraint)
        {
            constraints.Add("unmanaged");
        }

        // C# 8+
        if (typeParameter.HasNotNullConstraint)
        {
            constraints.Add("notnull");
        }

        for (int i = 0; i < typeParameter.ConstraintTypes.Length; i++)
        {
            var t = typeParameter.ConstraintTypes[i];
            var ann = i < typeParameter.ConstraintNullableAnnotations.Length
                ? typeParameter.ConstraintNullableAnnotations[i]
                : NullableAnnotation.None;

            string typeText = t.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            if (ann == NullableAnnotation.Annotated && t.NullableAnnotation != NullableAnnotation.Annotated)
            {
                typeText += "?";
            }

            constraints.Add(typeText);
        }

        if (typeParameter.HasConstructorConstraint)
        {
            constraints.Add("new()");
        }

        return constraints.Count > 0
            ? $"where {typeParameter.Name}: {string.Join(", ", constraints)}"
            : string.Empty;
    }

    private static bool IsGenericType(ITypeSymbol? symbol)
    {
        if (symbol == null)
        {
            return false;
        }

        return symbol switch
        {
            ITypeParameterSymbol => true,
            INamedTypeSymbol namedSymbol => namedSymbol.TypeArguments.Any(IsGenericType) ||
                                            namedSymbol.ContainingType != null &&
                                            IsGenericType(namedSymbol.ContainingType),
            IArrayTypeSymbol arraySymbol => IsGenericType(arraySymbol.ElementType),
            IPointerTypeSymbol pointerSymbol => IsGenericType(pointerSymbol.PointedAtType),
            _ => false
        };
    }
}
