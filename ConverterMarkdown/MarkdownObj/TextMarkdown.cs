namespace ConverterMarkdown.MarkdownObj
{
    public class TextMarkdown : IMarkdownObject
    {
        public string Content { get; set; }

        public TextMarkdown()
        {
            Content = string.Empty;
        }

        public TextMarkdown(string content)
        {
            Content = content;
        }
    }
}
