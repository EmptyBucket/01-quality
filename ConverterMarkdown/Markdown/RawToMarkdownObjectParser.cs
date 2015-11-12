using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ConverterMarkdown.MarkdownObj;

namespace ConverterMarkdown.Markdown
{
    public class RawToMarkdownObjectParser
    {
        private const string PatternItalic = @"(?:[^_\\]|^)(_(?!_|\d+))(.+?[^_\\])_(?!_|\d+)";
        private const string PatterBold = @"(?:[^\\]|^)(__)(.+?[^\\])__";
        private const string PatternCode = @"(?:[^\\]|^)(`)(.+?[^\\])`";

        private readonly Regex regItalic = new Regex(PatternItalic);
        private readonly Regex regBold = new Regex(PatterBold);
        private readonly Regex regCode = new Regex(PatternCode);

        private const string Italic = "_";
        private const string Bold = "__";
        private const string Code = "`";

        private readonly Regex[] mRegArray;

        public RawToMarkdownObjectParser()
        {
            mRegArray = new Regex[]
            {
                regItalic,
                regBold,
                regCode
            };
        }

        public DocumentMarkdown Parse(string rawStr)
        {
            IEnumerable<ParagraphMarkdown> paragraphs = ParagraphParse(rawStr);
            var documentMarkdown = new DocumentMarkdown(paragraphs);
            return documentMarkdown;
        }

        private IEnumerable<ParagraphMarkdown> ParagraphParse(string rawStr)
        {
            var markdownParagraphEnumerable = new List<ParagraphMarkdown>();
            string patternParagraph = $@"{Environment.NewLine}\s*{Environment.NewLine}";
            var regParagraph = new Regex(patternParagraph);
            string[] paragraphs = regParagraph.Split(rawStr);
            foreach (var item in paragraphs)
            {
                IEnumerable<IMarkdownObject> child = RawParse(item);
                var paragraphMarkdown = new ParagraphMarkdown(child);
                markdownParagraphEnumerable.Add(paragraphMarkdown);
            }
            return markdownParagraphEnumerable;
        }

        private Match FirstFoundSearchPattern(string str, Regex[] regArray)
        {
            Match minMatch = regArray
                .Select(reg => reg.Match(str))
                .Aggregate((minCurMatch, curMatch) =>
                {
                    if (!minCurMatch.Success && curMatch.Success)
                        return curMatch;
                    else if (curMatch.Success && minCurMatch.Index > curMatch.Index)
                        return curMatch;
                    else
                        return minCurMatch;
                });
            return minMatch;
        }

        private TextMarkdown GetTextBeforeFound(Match match, string currentStr)
        {
            var str = new string(currentStr.Take(match.Index).ToArray());
            var textMarkdown = new TextMarkdown(str);
            return textMarkdown;
        }

        private IMarkdownObject GetTypedMarkdownObject(string type, string contents)
        {
            MarkdownContainer markdownObject;
            switch (type)
            {
                case Code:
                    TextMarkdown text = new TextMarkdown(contents);
                    var childCode = new List<IMarkdownObject>()
                    {
                        text
                    };
                    markdownObject = new CodeMarkdown(childCode);
                    break;
                case Italic:
                    markdownObject = new ItalicMarkdown(RawParse(contents));
                    break;
                case Bold:
                    markdownObject = new BoldMarkdown(RawParse(contents));
                    break;
                default:
                    markdownObject = new MarkdownContainer(RawParse(contents));
                    break;
            }
            return markdownObject;
        }

        private IEnumerable<IMarkdownObject> RawParse(string rawStr)
        {
            var listMarkdownObjectOutLayer = new List<IMarkdownObject>();

            string currentStr = rawStr;
            Match match = FirstFoundSearchPattern(currentStr, mRegArray);
            if (!match.Success)
                listMarkdownObjectOutLayer.Add(new TextMarkdown(rawStr));
            else
            {
                while(match.Success)
                {
                    if (match.Index > 0)
                        listMarkdownObjectOutLayer.Add(GetTextBeforeFound(match, currentStr));

                    string contents = match.Groups[2].ToString();
                    string type = match.Groups[1].ToString();
                    IMarkdownObject markdownObject = GetTypedMarkdownObject(type, contents);                   
                    listMarkdownObjectOutLayer.Add(markdownObject);

                    currentStr = new string(currentStr.Skip(match.Index + match.Length).ToArray());
                    match = FirstFoundSearchPattern(currentStr, mRegArray);
                    if(!match.Success && currentStr != string.Empty)
                    {
                        var textMarkdown = new TextMarkdown(currentStr);
                        listMarkdownObjectOutLayer.Add(textMarkdown);
                    }
                }
            }
            return listMarkdownObjectOutLayer;
        }
    }
}
