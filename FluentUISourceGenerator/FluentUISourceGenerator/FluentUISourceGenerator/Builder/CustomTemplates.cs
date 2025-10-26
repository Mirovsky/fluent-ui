namespace FluentUISourceGenerator.Builder;

using Microsoft.CodeAnalysis;

public class CustomTemplates
{
    public static string GetCustomData(string symbol) => symbol switch {
        "VisualElement" => VisualElementCustomTemplate,
        "INotifyValueChanged" => INotificationExtensionsTemplate,
        _ => ""
    };

    private const string VisualElementCustomTemplate =
        """
        public static TVisualElement Children<TVisualElement>(this TVisualElement t, params UnityEngine.UIElements.VisualElement[] children) where TVisualElement : UnityEngine.UIElements.VisualElement
        {
            foreach (var child in children)
            {
                t.Add(child);
            }
        
            return t;
        }
        
        public static TVisualElement Classes<TVisualElement>(this TVisualElement t, params string[] classes) where TVisualElement : UnityEngine.UIElements.VisualElement
        {
            foreach (string c in classes)
            {
                t.AddToClassList(c);
            }
        
            return t;
        }
        """;

    private const string INotificationExtensionsTemplate =
        """
        public static TVisualElement RegisterValueCallback<TVisualElement, TValue>(this TVisualElement t, UnityEngine.UIElements.EventCallback<UnityEngine.UIElements.ChangeEvent<TValue>> callback)
            where TVisualElement : UnityEngine.UIElements.VisualElement, UnityEngine.UIElements.INotifyValueChanged<TValue>
        {
            UnityEngine.UIElements.INotifyValueChangedExtensions.RegisterValueChangedCallback<TValue>(t, callback);
        
            return t;
        }
        """;
}
