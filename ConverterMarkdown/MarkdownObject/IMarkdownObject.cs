using System.Collections.Generic;

namespace ConverterMarkdown.MarkdownObject
{
    interface IMarkdownObject
    {
        List<IMarkdownObject> Child { get; }
        string Content { get; }
    }
}
