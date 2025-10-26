#if LOCALIZATION

using UnityEngine;
using UnityEngine.UIElements;

namespace FluentUI.Samples.Localization
{
    public class LocalizationSample : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;

        private void Start()
        {
            uiDocument.rootVisualElement.Add(new LocalizationSampleView());
        }
    }
}

#endif
