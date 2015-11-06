using ConverterMarkdown.MarkdownObj;

namespace ConverterMarkdown.MarkdownObjectToTypeParser
{
    public interface IMarkdownObjectParser
    {
        MarkdownObjectInterpretator InterpretatorItalic { get; }
        MarkdownObjectInterpretator InterpretatorBold { get; }
        MarkdownObjectInterpretator InterpretatorCode { get; }
        MarkdownObjectInterpretator InterpretatorParagraph { get; }
        MarkdownObjectInterpretator InterpretatorDocument { get; }
        MarkdownObjectInterpretator InterpretatorText { get; }

        string Parse(DocumentMarkdown documentMarkdown);
    }
}
