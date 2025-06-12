using Unity.Properties;
using UnityEngine.UIElements;

namespace FluentUI.Bindings
{
    public class ToggleClassBinding : CustomBinding
    {
        private readonly string[] _classNames;

        public ToggleClassBinding(params string[] classNames)
        {
            _classNames = classNames;
        }

        protected override BindingResult Update(in BindingContext context)
        {
            var path = (PropertyPath)context.bindingId;
            if (!PropertyContainer.TryGetValue(context.dataSource, in path, out bool value))
            {
                return new BindingResult(BindingStatus.Failure);
            }

            if (value)
            {
                foreach (string className in _classNames)
                {
                    context.targetElement.AddToClassList(className);
                }
            }
            else
            {
                foreach (string className in _classNames)
                {
                    context.targetElement.RemoveFromClassList(className);
                }
            }

            return new BindingResult(BindingStatus.Success);
        }
    }
}
