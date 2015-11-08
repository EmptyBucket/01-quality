using System.Collections.Generic;

namespace ConverterMarkdown.MarkdownObj
{
    public class ParagraphMarkdown : MarkdownContainer
    {
        public ParagraphMarkdown() : base() { }

        public ParagraphMarkdown(IEnumerable<IMarkdownObject> child) : base(child) { }
    }
}
