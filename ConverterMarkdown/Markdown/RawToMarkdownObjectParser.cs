using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ConverterMarkdown.MarkdownObj;

namespace ConverterMarkdown.Markdown
{
    public class RawToMarkdownObjectParser
    {
        private readonly string PatternItalic = @"(?:_(?!\d))";
        private readonly string PatterBold = "(?:__)";
        private readonly string PatternCode = "(?:`)";
        private readonly string PatterParagraph = $@"(?:{Environment.NewLine})\s*{Environment.NewLine}";

        private readonly string Italic = "_";
        private readonly string Bold = "__";
        private readonly string Code = "`";
        private readonly string Paragraph = Environment.NewLine;

        private readonly Regex mRegMarkdown;

        public RawToMarkdownObjectParser()
        {
            string[] markdownObjectPatterns = new string[]
                {
                    PatterBold,
                    PatternItalic,
                    PatternCode,
                    PatterParagraph
                };
            string joinMarkdownObjectPatterns = string.Join("|", markdownObjectPatterns);
            string rootPattern = @"(?!\\)({0})(.*?)(?!\\)\1";
            string pattern = string.Format(rootPattern, joinMarkdownObjectPatterns);
            mRegMarkdown = new Regex(pattern);
        }

        public DocumentMarkdown Parse(string rawStr)
        {
            DocumentMarkdown documentMarkdown = new DocumentMarkdown();
            documentMarkdown.Child = RawParse(rawStr);
            return documentMarkdown;
        }

        public IEnumerable<IMarkdownObject> RawParse(string rawStr)
        {
            List<IMarkdownObject> listMarkdownObjectOutLayer = new List<IMarkdownObject>();
            Match match = mRegMarkdown.Match(rawStr);
            if (!match.Success)
                listMarkdownObjectOutLayer.Add(new TextMarkdown(rawStr));
            else
            {
                int indexStartText = 0;
                while (match.Success)
                {
                    if (match.Index > indexStartText)
                    {
                        int indexEndText = match.Index;
                        string str = new string(rawStr.Take(indexEndText).Skip(indexStartText).ToArray());
                        TextMarkdown textMarkdown = new TextMarkdown(str);
                        listMarkdownObjectOutLayer.Add(textMarkdown);
                    }
                    indexStartText = match.Index + match.Length;

                    string contents = match.Groups[2].ToString();
                    IEnumerable<IMarkdownObject> child = RawParse(contents);

                    string type = match.Groups[1].ToString();
                    MarkdownContainer markdownObject;
                    if (type == Italic)
                        markdownObject = new ItalicMarkdown(child);
                    else if (type == Bold)
                        markdownObject = new BoldMarkdown(child);
                    else if (type == Code)
                        markdownObject = new CodeMarkdown(child);
                    else if (type == Paragraph)
                        markdownObject = new ParagraphMarkdown(child);
                    else
                        markdownObject = new MarkdownContainer(child);
                    listMarkdownObjectOutLayer.Add(markdownObject);
                    match = match.NextMatch();
                }
            }
            return listMarkdownObjectOutLayer;
        }
    }
}
