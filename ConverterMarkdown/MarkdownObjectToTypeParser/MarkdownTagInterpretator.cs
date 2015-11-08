using System;
using System.Collections.Generic;
using System.Linq;

namespace ConverterMarkdown.MarkdownObjectToTypeParser
{
    public class MarkdownTagInterpretator
    {
        public string OpenTag { get; }
        public string CloseTag { get; }
        private Type mType;

        public MarkdownTagInterpretator(Type type, string openTag = "", string closeTag = "")
        {
            mType = type;
            OpenTag = openTag;
            CloseTag = closeTag;
        }

        public static MarkdownTagInterpretator GetMarkdownInterpretator(Type type, IEnumerable<MarkdownTagInterpretator> enumerableInterpretation)
        {
            return enumerableInterpretation.First(item => item.mType == type);
        }
    }
}
