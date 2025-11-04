using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace FluentUI.Samples.Editor
{
    public class ListViewExample : VisualElement
    {
        private readonly List<string> _items = Enumerable.Range(0, 1000).Select(i => $"Item {i}").ToList();

        public ListViewExample()
        {
            this.Name("ListView")
                .Children(
                    new ListView(_items)
                        .MakeItem(() => new Label())
                        .BindItem((element, i) => ((Label)element).Text(_items[i]))
                        .Style(static style => style.Height(400))
                );
        }
    }
}
