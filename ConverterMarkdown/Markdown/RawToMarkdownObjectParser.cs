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
            string rootPattern = @"(?!\\)({0})(.*)(?!\\)\1";
            string pattern = string.Format(rootPattern, joinMarkdownObjectPatterns);
            mRegMarkdown = new Regex(pattern);
        }

        public DocumentMarkdown Parse(string rawFileStr)
        {
            DocumentMarkdown documentMarkdown = new DocumentMarkdown(rawFileStr);
            documentMarkdown.Child = ContentParse(documentMarkdown.Content);
            return documentMarkdown;
        }

        private List<MarkdownObject> ContentParse(string content)
        {
            List<MarkdownObject> listMarkdownObject = CurrentLayerParse(content);
            foreach (var item in listMarkdownObject)
                item.Child = ContentParse(item.Content);
            return listMarkdownObject;
        }

        public List<MarkdownObject> CurrentLayerParse(string rawStr)
        {
            List<MarkdownObject> listMarkdownObjectOutLayer = new List<MarkdownObject>();
            Match match = mRegMarkdown.Match(rawStr);
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

                string content = match.Groups[2].ToString();
                string type = match.Groups[1].ToString();
                MarkdownObject markdownObject;
                if (type == Italic)
                    markdownObject = new ItalicMarkdown(content);
                else if (type == Bold)
                    markdownObject = new BoldMarkdown(content);
                else if (type == Code)
                    markdownObject = new CodeMarkdown(content);
                else if (type == Paragraph)
                    markdownObject = new ParagraphMarkdown(content);
                else
                    markdownObject = new MarkdownObject(content);
                listMarkdownObjectOutLayer.Add(markdownObject);
                match = match.NextMatch();
            }

            return listMarkdownObjectOutLayer;
        }
    }
}
