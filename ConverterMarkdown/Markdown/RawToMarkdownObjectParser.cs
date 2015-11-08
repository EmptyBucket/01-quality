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

        private const string Italic = "_";
        private const string Bold = "__";
        private const string Code = "`";

        private readonly Regex mRegMarkdown;

        public RawToMarkdownObjectParser()
        {
            string[] markdownObjectPatterns = new string[]
                {
                    PatterBold,
                    PatternItalic,
                    PatternCode
                };
            string joinMarkdownObjectPatterns = string.Join("|", markdownObjectPatterns);
            string rootPattern = @"(?:[^\\]|^)({0})(.*?)(?!\\)\1";
            string pattern = string.Format(rootPattern, joinMarkdownObjectPatterns);
            mRegMarkdown = new Regex(pattern);
        }

        public DocumentMarkdown Parse(string rawStr)
        {
            IEnumerable<ParagraphMarkdown> paragraphs = ParagraphParse(rawStr);
            DocumentMarkdown documentMarkdown = new DocumentMarkdown(paragraphs);
            return documentMarkdown;
        }

        public IEnumerable<ParagraphMarkdown> ParagraphParse(string rawStr)
        {
            List<ParagraphMarkdown> markdownParagraphEnumerable = new List<ParagraphMarkdown>();
            string patternParagraph = $@"{Environment.NewLine}\s*{Environment.NewLine}";
            Regex regParagraph = new Regex(patternParagraph);
            string[] paragraphs = regParagraph.Split(rawStr);
            foreach (var item in paragraphs)
            {
                IEnumerable<IMarkdownObject> child = RawParse(item);
                ParagraphMarkdown paragraphMarkdown = new ParagraphMarkdown(child);
                markdownParagraphEnumerable.Add(paragraphMarkdown);
            }
            return markdownParagraphEnumerable;
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
                    switch (type)
                    {
                        case Italic:
                            markdownObject = new ItalicMarkdown(child);
                            break;
                        case Bold:
                            markdownObject = new BoldMarkdown(child);
                            break;
                        case Code:
                            markdownObject = new CodeMarkdown(child);
                            break;
                        default:
                            markdownObject = new MarkdownContainer(child);
                            break;
                    }
                    listMarkdownObjectOutLayer.Add(markdownObject);
                    match = match.NextMatch();
                }
            }
            return listMarkdownObjectOutLayer;
        }
    }
}
