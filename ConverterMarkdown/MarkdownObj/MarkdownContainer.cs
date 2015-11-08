using System.Collections.Generic;

namespace ConverterMarkdown.MarkdownObj
{
    public class MarkdownContainer : IMarkdownObject
    {
        public IEnumerable<IMarkdownObject> Child { get; set; } = new List<IMarkdownObject>();

        public MarkdownContainer() { }

        public MarkdownContainer(IEnumerable<IMarkdownObject> child)
        {
            Child = child;
        }
    }
}
