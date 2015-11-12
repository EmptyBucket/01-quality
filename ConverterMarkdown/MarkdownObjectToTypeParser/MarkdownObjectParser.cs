﻿using System;
using System.Collections.Generic;
using System.Linq;
using ConverterMarkdown.MarkdownObj;

namespace ConverterMarkdown.MarkdownObjectToTypeParser
{
    public abstract class MarkdownObjectParser : IMarkdownObjectParser
    {
        public abstract MarkdownTagInterpretator InterpretatorItalic { get; }
        public abstract MarkdownTagInterpretator InterpretatorBold { get; }
        public abstract MarkdownTagInterpretator InterpretatorCode { get; }
        public abstract MarkdownTagInterpretator InterpretatorParagraph { get; }
        public abstract MarkdownTagInterpretator InterpretatorDocument { get; }
        public abstract MarkdownTagInterpretator InterpretatorText { get; }

        private IEnumerable<MarkdownTagInterpretator> mEnumerableMarkdownInterpretator;

        public MarkdownObjectParser()
        {
            mEnumerableMarkdownInterpretator = new List<MarkdownTagInterpretator>()
            {
                InterpretatorItalic,
                InterpretatorBold,
                InterpretatorCode,
                InterpretatorParagraph,
                InterpretatorDocument,
                InterpretatorText
            };
        }

        public virtual string Parse(DocumentMarkdown documentMarkdown)
        {
            string documentStr =  ParseMarkdownObject(documentMarkdown);

            return documentStr;
        }

        private string ParseMarkdownObject(IMarkdownObject markdownObject)
        {
            var contents = string.Empty;
            if (markdownObject is TextMarkdown)
                contents = ((TextMarkdown)markdownObject).Content;
            else if(markdownObject is MarkdownContainer)
            {
                var markdownContainer = (MarkdownContainer)markdownObject;
                IEnumerable<string> contentsEnumerable = markdownContainer.Child.Select(item => ParseMarkdownObject(item));
                contents = string.Join("", contentsEnumerable);
                MarkdownTagInterpretator markdownInterpretator = MarkdownTagInterpretator.GetMarkdownInterpretator(markdownObject.GetType(), mEnumerableMarkdownInterpretator);
                contents = $"{markdownInterpretator.OpenTag}{Environment.NewLine}{contents}{Environment.NewLine}{markdownInterpretator.CloseTag}{Environment.NewLine}";
            }

            return contents;
        }
    }
}
