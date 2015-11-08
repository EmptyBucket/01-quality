﻿using System;
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

        public IEnumerable<IMarkdownObject> RawParse(string rawStr)
        {
            List<IMarkdownObject> listMarkdownObjectOutLayer = new List<IMarkdownObject>();

            string currentStr = rawStr;
            Match match = FirstFoundSearchPattern(currentStr, mRegArray);
            if (!match.Success)
                listMarkdownObjectOutLayer.Add(new TextMarkdown(rawStr));
            else
            {
                int indexStartText = 0;

                while(match.Success)
                {
                    if (match.Index > indexStartText)
                    {
                        int indexEndText = match.Index;
                        string str = new string(rawStr.Take(indexEndText).Skip(indexStartText).ToArray());
                        TextMarkdown textMarkdown = new TextMarkdown(str);
                        listMarkdownObjectOutLayer.Add(textMarkdown);
                    }

                    string contents = match.Groups[2].ToString();

                    string type = match.Groups[1].ToString();
                    MarkdownContainer markdownObject;
                    switch (type)
                    {
                        case Code:
                            TextMarkdown text = new TextMarkdown(contents);
                            IEnumerable<IMarkdownObject> childCode = new List<IMarkdownObject>()
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
                    listMarkdownObjectOutLayer.Add(markdownObject);

                    indexStartText = match.Index + match.Length;
                    currentStr = new string(rawStr.Skip(indexStartText).ToArray());
                    match = FirstFoundSearchPattern(currentStr, mRegArray);
                    if(!match.Success)
                    {
                        TextMarkdown textMarkdown = new TextMarkdown(currentStr);
                        listMarkdownObjectOutLayer.Add(textMarkdown);
                    }
                }
            }
            return listMarkdownObjectOutLayer;
        }
    }
}
