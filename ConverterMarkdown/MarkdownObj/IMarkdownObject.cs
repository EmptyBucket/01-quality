using System.Collections.Generic;

namespace ConverterMarkdown.MarkdownObj
{
    public interface IMarkdownObject
    {
        string Content { get; }

        IEnumerable<IMarkdownObject> Child { get; set; }
    }
}
