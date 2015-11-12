using System;
using ConverterMarkdown.MarkdownObj;

namespace ConverterMarkdown.MarkdownObjectToTypeParser
{
    public class MarkdownObjectToHTMLParser : MarkdownObjectParser
    {
        public override MarkdownTagInterpretator InterpretatorBold { get; } = new MarkdownTagInterpretator(typeof(BoldMarkdown), "<strong>", "</strong>");

        public override MarkdownTagInterpretator InterpretatorCode { get; } = new MarkdownTagInterpretator(typeof(CodeMarkdown), "<code>", "</code>");

        public override MarkdownTagInterpretator InterpretatorDocument { get; } = new MarkdownTagInterpretator(typeof(DocumentMarkdown), "<body>", "</body>");

        public override MarkdownTagInterpretator InterpretatorItalic { get; } = new MarkdownTagInterpretator(typeof(ItalicMarkdown), "<em>", "</em>");

        public override MarkdownTagInterpretator InterpretatorParagraph { get; } = new MarkdownTagInterpretator(typeof(ParagraphMarkdown), "<p>", "</p>");

        public override MarkdownTagInterpretator InterpretatorText { get; } = new MarkdownTagInterpretator(typeof(TextMarkdown));

        public override string Parse(DocumentMarkdown documentMarkdown)
        {
            string htmlBody = base.Parse(documentMarkdown);
            var html = $"<!DOCTYPE html>{Environment.NewLine}<html>{Environment.NewLine}{htmlBody}{Environment.NewLine}</html>";
            return html;
        }

        public MarkdownObjectToHTMLParser() : base() { }
    }
}