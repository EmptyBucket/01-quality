using System.Collections.Generic;

namespace ConverterMarkdown.MarkdownObj
{
    public class CodeMarkdown : MarkdownContainer
    {
        public CodeMarkdown() : base() { }

        public CodeMarkdown(IEnumerable<IMarkdownObject> child) : base(child) { }
    }
}
