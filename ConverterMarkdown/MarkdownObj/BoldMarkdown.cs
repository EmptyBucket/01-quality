using System.Collections.Generic;

namespace ConverterMarkdown.MarkdownObj
{
    public class BoldMarkdown : MarkdownContainer
    {
        public BoldMarkdown() : base() { }

        public BoldMarkdown(IEnumerable<IMarkdownObject> child) : base(child) { }
    }
}
