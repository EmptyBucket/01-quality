using ConverterMarkdown.Markdown;
using ConverterMarkdown.MarkdownObj;

namespace ConverterMarkdown.MarkdownObjectToTypeParser
{
    public interface IMarkdownObjectParser
    {
        string Parse(DocumentMarkdown treeMarkdownObject);
    }
}
