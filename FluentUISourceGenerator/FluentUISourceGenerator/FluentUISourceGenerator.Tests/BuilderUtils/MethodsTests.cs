namespace FluentUISourceGenerator.Tests.BuilderUtils;

using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Xunit;
using Utils;

public class MethodsTests
{
    [Fact]
    public void GetAllMethods_Basic()
    {
        string sources = File.ReadAllText("Mocks/Basic.cs");

        var comp = TestUtils.Create([sources]);
        var a = comp.GetTypeByMetadataName("FluentUISourceGenerator.Tests.Mocks.A");

        Assert.NotNull(a);

        var expected = new[] {"Method"};
        var actual = BuilderUtils.GetAllMethods(a).Select(m => m.Name);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetAllMethods_BasicGenerics()
    {
        string sources = File.ReadAllText("Mocks/BasicGenerics.cs");

        var comp = TestUtils.Create([sources]);

        var a = comp.GetTypeByMetadataName("FluentUISourceGenerator.Tests.Mocks.GenericA`1");
        string[] expectedA = [];
        var actualA = BuilderUtils.GetAllMethods(a).Select(m => m.Name);
        Assert.Equal(expectedA, actualA);

        var b = comp.GetTypeByMetadataName("FluentUISourceGenerator.Tests.Mocks.GenericB");
        var expectedB = new[] {"Method"};
        var actualB = BuilderUtils.GetAllMethods(b).Select(m => m.Name);
        Assert.Equal(expectedB, actualB);
    }
}
