using ConverterMarkdown.MarkdownObjectToTypeParser;

namespace ConverterMarkdown.Markdown
{
    public class MarkdownToTypeConverter
    {
        private IMarkdownObjectToTypeParser mToTypeParser;

        public MarkdownToTypeConverter(IMarkdownObjectToTypeParser toTypeParser)
        {
            mToTypeParser = toTypeParser;
        }

        public string Convert(string rawFileStr)
        {
            MarkdownStrToMarkdownObjectParser toMarkdownObjectParser = new MarkdownStrToMarkdownObjectParser();
            TreeMarkdownObject treeMarkdownObject = toMarkdownObjectParser.Parse(rawFileStr);
            string html = mToTypeParser.Parse(treeMarkdownObject);
            return html;
        }
    }
}