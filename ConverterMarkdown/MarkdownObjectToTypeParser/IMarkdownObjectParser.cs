using ConverterMarkdown.MarkdownObj;

namespace ConverterMarkdown.MarkdownObjectToTypeParser
{
    public interface IMarkdownObjectParser
    {
        MarkdownTagInterpretator InterpretatorItalic { get; }
        MarkdownTagInterpretator InterpretatorBold { get; }
        MarkdownTagInterpretator InterpretatorCode { get; }
        MarkdownTagInterpretator InterpretatorParagraph { get; }
        MarkdownTagInterpretator InterpretatorDocument { get; }
        MarkdownTagInterpretator InterpretatorText { get; }

        string Parse(DocumentMarkdown documentMarkdown);
    }
}
