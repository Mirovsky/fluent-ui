namespace FluentUISourceGenerator.Tests;

using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

public class DevelopingTests
{
    // [Fact]
    public void Dev()
    {
        var refs = new List<MetadataReference>
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(
                @"G:\Program Files\Unity\6000.0.60f1\Editor\Data\Managed\UnityEngine\UnityEngine.UIElementsModule.dll"),
            MetadataReference.CreateFromFile(
                @"G:\Program Files\Unity\6000.0.60f1\Editor\Data\Managed\UnityEngine\UnityEngine.PropertiesModule.dll")
        };

        var compilation = CSharpCompilation.Create(
            "Dev",
            [],
            refs,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );

        GeneratorDriver driver = CSharpGeneratorDriver.Create(new FluentUISourceGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

        var run = driver.GetRunResult();

        const string output = "./Outputs";
        Directory.CreateDirectory(output);

        foreach (var tree in run.GeneratedTrees)
        {
            File.WriteAllText(Path.Join(output, Path.GetFileName(tree.FilePath)), tree.GetText().ToString());
        }
    }
}
