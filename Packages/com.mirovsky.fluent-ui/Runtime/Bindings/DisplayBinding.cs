using Unity.Properties;
using UnityEngine.UIElements;

namespace FluentUI.Bindings
{
    public class DisplayBinding : CustomBinding
    {
        private readonly bool _negate;

        public DisplayBinding(bool negate)
        {
            updateTrigger = BindingUpdateTrigger.OnSourceChanged;

            _negate = negate;
        }

        protected override BindingResult Update(in BindingContext context)
        {
            var path = new PropertyPath("AddingElement");
            if (!PropertyContainer.TryGetValue(context.dataSource, in path, out bool value))
            {
                return new BindingResult(BindingStatus.Failure);
            }

            value = _negate ? !value : value;

            context.targetElement.style.display = value ? DisplayStyle.Flex : DisplayStyle.None;

            return new BindingResult(BindingStatus.Success);
        }
    }
}
