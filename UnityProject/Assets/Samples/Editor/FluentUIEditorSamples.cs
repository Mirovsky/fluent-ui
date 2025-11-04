using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;

namespace FluentUI.Samples.Editor
{
    public class FluentUIEditorSamples : EditorWindow
    {
        [MenuItem("Assets/Fluent UI/Samples Window")]
        public static void ShowSamples()
        {
            GetWindow<FluentUIEditorSamples>().Show();
        }

        private void CreateGUI()
        {
            rootVisualElement
                .Children(
                    new Label("Fluent UI Samples")
                        .Style(static style => style
                            .FontSize(24)
                            .UnityFontStyleAndWeight(FontStyle.Bold)
                            .Width(Length.Percent(100))
                            .UnityTextAlign(TextAnchor.MiddleCenter)),
                    new TabView()
                        .Children(
                            new Tab("Simple Component")
                                .Children(new SimpleComponentExample()),
                            new Tab("List View")
                                .Children(new ListViewExample()),
                            new Tab("Data Bindings")
                                .Children(new DataBindingsExample())
                        )
                );
        }
    }
}
