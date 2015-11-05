using System.Collections.Generic;
using System.Linq;

namespace ConverterMarkdown.MarkdownObj
{
    public class MarkdownObject : IMarkdownObject
    {
        private IEnumerable<IMarkdownObject> mChild = new List<IMarkdownObject>();
        public IEnumerable<IMarkdownObject> Child
        {
            get
            {
                return mChild;
            }
            set
            {
                mChild = new List<IMarkdownObject>(value);
            }
        }

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
