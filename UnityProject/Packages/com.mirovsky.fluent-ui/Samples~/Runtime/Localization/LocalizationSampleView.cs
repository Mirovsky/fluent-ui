#if LOCALIZATION

using UnityEngine.UIElements;
using UnityEngine.Localization;

namespace FluentUI.Samples.Localization
{
    public class LocalizationSampleView : VisualElement
    {
        public LocalizationSampleView()
        {
            this.Name("LocalizationSampleView")
                .Children(
                    new Label()
                        .BindText(new LocalizedString("LocalizationKeys", "HELLO_WORLD"))
                );
        }
    }
}

#endif
