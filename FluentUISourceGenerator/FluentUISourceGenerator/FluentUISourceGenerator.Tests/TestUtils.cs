namespace FluentUISourceGenerator.Tests;

using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public static class TestUtils
{
    public static CSharpCompilation Create(IEnumerable<string> sources, params MetadataReference[]? refs)
    {
        var tree = sources
            .Select(s => CSharpSyntaxTree.ParseText(s, new CSharpParseOptions(LanguageVersion.CSharp8)));
        var metaReference = new List<MetadataReference> {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };
        if (refs != null)
        {
            metaReference.AddRange(refs);
        }

        return CSharpCompilation.Create(
            "TestAsm",
            tree,
            metaReference,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );
    }
}
