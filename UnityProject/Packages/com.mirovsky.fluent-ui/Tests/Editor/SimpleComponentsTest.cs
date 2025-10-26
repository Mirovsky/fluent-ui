using NUnit.Framework;
using UnityEngine.UIElements;

namespace FluentUI.Editor.Tests
{
    public class SimpleComponentsTest
    {
        [Test]
        public void SimpleComponent()
        {
            var box = new Box()
                .Children(
                    new Label(),
                    new TextField()
                );
        }
    }
}
