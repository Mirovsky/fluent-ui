namespace FluentUISourceGenerator.Tests.BuilderUtils;

using System.IO;
using System.Linq;
using Xunit;
using Utils;

public class EventsTests
{
    [Fact]
    public void GetAllEvents_Basic()
    {
        string sources = File.ReadAllText("Mocks/Basic.cs");

        var comp = TestUtils.Create([sources]);
        var a = comp.GetTypeByMetadataName("FluentUISourceGenerator.Tests.Mocks.A");
        var expected = new[] {"Event"};
        var actual = BuilderUtils.GetAllEvents(a).Select(e => e.Name);

        Assert.Equal(expected, actual);
    }
}
