using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace FluentUI.Samples.Editor
{
    public class SimpleComponentExample : VisualElement
    {
        public SimpleComponentExample()
        {
            this.Name("SimpleComponent")
                .Children(
                    new Label("Hello, World!"),
                    new Button()
                        .Text("Click me! (and nothing happens)"),
                    new Toggle("Just a toggle"),
                    new RadioButtonGroup("Radio Group")
                        .Choices(new [] { "Option 1", "Option 2", "Option 3" }),
                    new Slider("Yeet", 0.0f, 1.0f, SliderDirection.Horizontal, 0.1f),
                    new Vector4Field("Vector4"),
                    new TextField("Text")
                        .Value("Let's add some default value here"),
                    new TagField("Tag Me")
                );
        }
    }
}
