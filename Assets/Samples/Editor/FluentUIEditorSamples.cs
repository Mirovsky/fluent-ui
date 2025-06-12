using UnityEditor;
using UnityEngine.UIElements;
using FluentUI;
using Samples.Editor;
using UnityEngine;

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
                    .StyleFontSize(24)
                    .StyleUnityFontStyleAndWeight(FontStyle.Bold)
                    .StyleWidth(Length.Percent(100))
                    .StyleUnityTextAlign(TextAnchor.MiddleCenter),
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
