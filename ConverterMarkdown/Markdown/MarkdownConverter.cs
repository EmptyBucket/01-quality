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
            var rawToMarkdownObjectParser = new RawToMarkdownObjectParser();
            DocumentMarkdown documentMarkdown = rawToMarkdownObjectParser.Parse(rawFileStr);
            string html = mToTypeParser.Parse(documentMarkdown);
            return html;
        }
    }
}