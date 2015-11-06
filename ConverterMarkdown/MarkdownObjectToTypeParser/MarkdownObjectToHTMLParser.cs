using ConverterMarkdown.MarkdownObj;

namespace ConverterMarkdown.MarkdownObjectToTypeParser
{
    public class MarkdownObjectToHTMLParser : MarkdownObjectParser
    {
        public override MarkdownObjectInterpretator InterpretatorBold { get; } = new MarkdownObjectInterpretator(typeof(BoldMarkdown), "<strong>", "</strong>");

        public override MarkdownObjectInterpretator InterpretatorCode { get; } = new MarkdownObjectInterpretator(typeof(CodeMarkdown), "<code>", "</code>");

        public override MarkdownObjectInterpretator InterpretatorDocument { get; } = new MarkdownObjectInterpretator(typeof(DocumentMarkdown), "<body>", "</body>");

        public override MarkdownObjectInterpretator InterpretatorItalic { get; } = new MarkdownObjectInterpretator(typeof(ItalicMarkdown), "<em>", "</em>");

        public override MarkdownObjectInterpretator InterpretatorParagraph { get; } = new MarkdownObjectInterpretator(typeof(ParagraphMarkdown), "<p>", "/<p>");

        public override MarkdownObjectInterpretator InterpretatorText { get; } = new MarkdownObjectInterpretator(typeof(TextMarkdown));

        public override string Parse(DocumentMarkdown documentMarkdown)
        {
            string htmlBody = base.Parse(documentMarkdown);
            string html = $"<!DOCTYPE html><html><head></head>{htmlBody}</html>";
            return html;
        }

        public MarkdownObjectToHTMLParser() :base() { }
    }
}