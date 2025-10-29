namespace FluentUISourceGenerator.Tests;

using System.Linq;
using Utils;
using Xunit;

public class NamingTests
{
    [Fact]
    public void CreateName_Symbol()
    {
        var comp = TestUtils.Create([
            """
            namespace N {
                class A {
                }
            }
            """
        ]);
        var a = comp.GetTypeByMetadataName("N.A");
        Assert.Equal("TA", NamingUtils.CreateTName(a));
    }

    [Fact]
    public void CreateName_Property()
    {
        var comp = TestUtils.Create([
            """
            namespace N {
                class A {
                    public string label { get; set; }
                }
            }
            """
        ]);
        var a = comp.GetTypeByMetadataName("N.A");
        var property = Utils.BuilderUtils.GetAllProperties(a).First();
        Assert.Equal("TA", NamingUtils.CreateTName(property));
    }

    [Fact]
    public void CreateFluentName_Method()
    {
        Assert.Equal("Value", NamingUtils.CreateFluentName("SetValue"));
    }

    [Fact]
    public void CreateFluentName_Property()
    {
        Assert.Equal("Label", NamingUtils.CreateFluentName("label"));
    }

    [Fact]
    public void CreateStyleName()
    {
        Assert.Equal("styleWidth", NamingUtils.CreateStyleName("WidthProperty"));
    }
}
