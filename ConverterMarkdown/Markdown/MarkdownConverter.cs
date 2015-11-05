using ConverterMarkdown.MarkdownObj;
using ConverterMarkdown.MarkdownObjectToTypeParser;

namespace ConverterMarkdown.Markdown
{
    public class MarkdownConverter
    {
        private IMarkdownObjectParser mToTypeParser;

        public MarkdownConverter(IMarkdownObjectParser toTypeParser)
        {
            mToTypeParser = toTypeParser;
        }

        public string Convert(string rawFileStr)
        {
            RawToMarkdownObjectParser toMarkdownObjectParser = new RawToMarkdownObjectParser();
            DocumentMarkdown documentMarkdown = toMarkdownObjectParser.Parse(rawFileStr);
            string html = mToTypeParser.Parse(documentMarkdown);
            return html;
        }
    }
}