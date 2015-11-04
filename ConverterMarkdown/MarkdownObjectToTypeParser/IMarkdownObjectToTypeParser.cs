using ConverterMarkdown.Markdown;

namespace ConverterMarkdown.MarkdownObjectToTypeParser
{
    public interface IMarkdownObjectToTypeParser
    {
        string Parse(TreeMarkdownObject treeMarkdownObject);
    }
}
