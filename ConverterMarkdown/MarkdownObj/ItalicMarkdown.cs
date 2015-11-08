using System.Collections.Generic;

namespace ConverterMarkdown.MarkdownObj
{
    public class ItalicMarkdown : MarkdownContainer
    {
        public ItalicMarkdown() : base() { }

        public ItalicMarkdown(IEnumerable<IMarkdownObject> child) : base(child) { }
    }
}
