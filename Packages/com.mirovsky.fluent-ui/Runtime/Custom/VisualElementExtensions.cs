using UnityEngine.UIElements;

namespace FluentUI
{
    public static class VisualElementExtensions
    {
        public static TVisualElement Children<TVisualElement>(this TVisualElement t, params VisualElement[] children) where TVisualElement : VisualElement
        {
            foreach (var child in children)
            {
                t.Add(child);
            }

            return t;
        }

        public static TVisualElement Classes<TVisualElement>(this TVisualElement t, params string[] classes) where TVisualElement : VisualElement
        {
            foreach (string c in classes)
            {
                t.AddToClassList(c);
            }

            return t;
        }

        public static TVisualElement RegisterValueCallback<TVisualElement, TValue>(this TVisualElement t, EventCallback<ChangeEvent<TValue>> callback)
            where TVisualElement : VisualElement, INotifyValueChanged<TValue>
        {
            t.RegisterValueChangedCallback(callback);

            return t;
        }
    }
}
