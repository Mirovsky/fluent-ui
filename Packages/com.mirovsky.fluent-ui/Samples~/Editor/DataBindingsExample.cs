using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace FluentUI.Samples.Editor
{
    public class DataBindingsExample : VisualElement
    {
        [CreateProperty]
        private string _textValue = "Hello, World!";

        [CreateProperty]
        private int _fontSize = 12;

        [CreateProperty]
        private Color _color = Color.white;

        public DataBindingsExample()
        {
            this.Name("DataBindings")
                .DataSource(this)
                .Children(
                    new TextField("Input")
                        .BindValue(nameof(_textValue), bindingMode: BindingMode.TwoWay),
                    new Label()
                        .BindText(nameof(_textValue))
                        .BindStyleFontSize(nameof(_fontSize))
                        .BindStyleColor(nameof(_color)),
                    new VisualElement()
                        .StyleFlexDirection(FlexDirection.Row)
                        .StyleWidth(Length.Percent(100))
                        .StyleJustifyContent(Justify.SpaceBetween)
                        .Children(
                            new Button(() => _fontSize++)
                                .Text("Increase Size")
                                .StyleFlexGrow(1),
                            new Button(() => _fontSize--)
                                .Text("Decrease Size")
                                .StyleFlexGrow(1),
                            new Button(() => _color = new Color(Random.value, Random.value, Random.value))
                                .Text("Randomize Color")
                                .StyleFlexGrow(1)
                        )
                );
        }
    }
}
