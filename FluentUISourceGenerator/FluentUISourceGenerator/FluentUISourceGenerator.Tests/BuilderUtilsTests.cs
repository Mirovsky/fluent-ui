namespace FluentUISourceGenerator.Tests;

using System.IO;
using System.Linq;
using Utils;
using Xunit;

public class BuilderUtilsTests
{
    [Fact]
    public void GetAllProperties_Basic()
    {
        string sources = File.ReadAllText("Mocks/Basic.cs");

        var comp = TestUtils.Create([sources]);
        var a = comp.GetTypeByMetadataName("FluentUISourceGenerator.Tests.Mocks.A");

        Assert.NotNull(a);

        var expected = new[] { "label" };
        var actual = BuilderUtils.GetAllProperties(a).Select(p => p.Name);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetAllProperties_BasicGenerics()
    {
        string sources = File.ReadAllText("Mocks/BasicGenerics.cs");

        var comp = TestUtils.Create([sources]);

        var a = comp.GetTypeByMetadataName("FluentUISourceGenerator.Tests.Mocks.GenericA`1");
        Assert.NotNull(a);
        var expectedA = new[] { "label" };
        var actualA = BuilderUtils.GetAllProperties(a).Select(p => p.Name);
        Assert.Equivalent(expectedA, actualA);

        var b = comp.GetTypeByMetadataName("FluentUISourceGenerator.Tests.Mocks.GenericB");
        Assert.NotNull(b);
        var expectedB = new[] {"genericValue", "value", "genericValue2"};
        var actualB = BuilderUtils.GetAllProperties(b).Select(p => p.Name);
        Assert.Equivalent(expectedB, actualB);
    }

    [Fact]
    public void GetAllProperties_Button()
    {
        string sources = File.ReadAllText("Mocks/Button.cs");

        var comp = TestUtils.Create([sources]);

        var textElement = comp.GetTypeByMetadataName("FluentUISourceGenerator.Tests.Mocks.TextElement");
        Assert.NotNull(textElement);
        string[] expectedTextElement = ["text"];
        var actualTextElement = BuilderUtils.GetAllProperties(textElement).Select(p => p.Name);
        Assert.Equal(expectedTextElement, actualTextElement);

        var button = comp.GetTypeByMetadataName("FluentUISourceGenerator.Tests.Mocks.Button");
        Assert.NotNull(button);
        string[] expectedButton = [];
        var actualButton = BuilderUtils.GetAllProperties(button).Select(p => p.Name);
        Assert.Equal(expectedButton, actualButton);
    }

    [Fact]
    public void GetAllProperties_TextField()
    {
        string sources = File.ReadAllText("Mocks/TextField.cs");

        var comp = TestUtils.Create([sources]);

        var baseField = comp.GetTypeByMetadataName("FluentUISourceGenerator.Tests.Mocks.BaseField`1");
        Assert.NotNull(baseField);
        string[] expectedBaseField = [];
        var actualBaseField = BuilderUtils.GetAllProperties(baseField).Select(p => p.Name);
        Assert.Equal(expectedBaseField, actualBaseField);

        var textField = comp.GetTypeByMetadataName("FluentUISourceGenerator.Tests.Mocks.TextField");
        Assert.NotNull(textField);
        string[] expectedTextField = ["value"];
        var actualTextField = BuilderUtils.GetAllProperties(textField).Select(p => p.Name);
        Assert.Equal(expectedTextField, actualTextField);
    }
}
