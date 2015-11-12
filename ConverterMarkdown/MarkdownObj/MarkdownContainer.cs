using System.Collections.Generic;

namespace ConverterMarkdown.MarkdownObj
{
    public class MarkdownContainer : IMarkdownObject
    {
        public IEnumerable<IMarkdownObject> Child { get; }

        public MarkdownContainer()
        {
            Child = new List<IMarkdownObject>();
        }

        public MarkdownContainer(IEnumerable<IMarkdownObject> child)
        {
            Child = child;
        }
    }
}
