namespace FluentUISourceGenerator.Utils;

using System.Linq;
using Microsoft.CodeAnalysis;

public static class TemplateUtils
{
    public static string CreateGenericsForMethod(IMethodSymbol method) =>
        string.Join(
            ", ",
            TypeUtils.GetContainingTypeGenericParameters(method)
                .Concat(TypeUtils.GetMethodGenericParameters(method))
                .Prepend(NamingUtils.CreateTName(method))
        );

    public static string CreateGenericsForSymbol(INamedTypeSymbol type, ISymbol symbol) =>
        string.Join(", ", TypeUtils.GetContainingTypeGenericParameters(symbol).Prepend(NamingUtils.CreateTName(type)));

    public static string CreateConstraintsForSymbol(INamedTypeSymbol symbol) =>
        string.Join(" ", TypeUtils.GetContainingTypeConstraints(symbol));

    public static string CreateParametersForMethod(IMethodSymbol method)
    {
        string[] parameters = TypeUtils.GetParametersFromMethod(method);

        return parameters.Length > 0 ? "," + string.Join(", ", parameters) : "";
    }

    public static string CreateArgumentsForMethod(IMethodSymbol method)
    {
        return string.Join(", ", TypeUtils.GetArgumentsFromMethod(method));
    }
}
