using System.Collections.Generic;

namespace ConverterMarkdown.MarkdownObject
{
    public interface IMarkdownObject
    {
        List<IMarkdownObject> Child { get; }
        string Content { get; }
    }
}
