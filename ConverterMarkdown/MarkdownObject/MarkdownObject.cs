using System.Collections.Generic;

namespace ConverterMarkdown.MarkdownObject
{
    public class MarkdownObject : IMarkdownObject
    {
        public List<IMarkdownObject> Child { get; private set; } = new List<IMarkdownObject>();

        public string Content { get; private set; }

        public MarkdownObject()
        {
            Content = string.Empty;
        }

        public MarkdownObject(string content)
        {
            Content = content;
        }

        public MarkdownObject(List<IMarkdownObject> child) : this()
        {
            Child = child;
        }

        public MarkdownObject(string content, List<IMarkdownObject> child) : this(content)
        {
            Child = child;
        }
    }
}
