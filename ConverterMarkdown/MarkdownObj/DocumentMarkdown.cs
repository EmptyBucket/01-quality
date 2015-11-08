using System.Collections.Generic;

namespace ConverterMarkdown.MarkdownObj
{
    public class DocumentMarkdown : MarkdownContainer
    {
        public DocumentMarkdown() : base() { }

        public DocumentMarkdown(IEnumerable<IMarkdownObject> child) : base(child) { }
    }
}
