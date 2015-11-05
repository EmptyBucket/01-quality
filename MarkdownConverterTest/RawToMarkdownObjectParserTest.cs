using System;
using System.Collections.Generic;
using ConverterMarkdown.Markdown;
using ConverterMarkdown.MarkdownObj;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarkdownConverterTest
{
    [TestClass]
    public class RawToMarkdownObjectParserTest
    {
        [TestMethod]
        public void RawStr_CurrentLayerParse_CorrectMarkdownObjectsContent()
        {
            string rawStr = string.Join("",
                new string[]
                {
                    "Simple text one",
                    "_italic_",
                    $"{Environment.NewLine} {Environment.NewLine}paragraph{Environment.NewLine} {Environment.NewLine}",
                    "Simple text two",
                    "`code`",
                    "__bold__"
                });

            RawToMarkdownObjectParser parser = new RawToMarkdownObjectParser();
            List<MarkdownObject> listMarkdownObject = parser.CurrentLayerParse(rawStr);

            TextMarkdown textMarkdownOne = new TextMarkdown("Simple text one");
            ItalicMarkdown italicMarkdown = new ItalicMarkdown("italic");
            ParagraphMarkdown paragraphMarkdown = new ParagraphMarkdown("paragraph");
            TextMarkdown textMarkdownTwo = new TextMarkdown("Simple text two");
            BoldMarkdown boldMarkdown = new BoldMarkdown("bold");
            CodeMarkdown codeMarkdown = new CodeMarkdown("code");
            Assert.AreEqual(textMarkdownOne.Content, listMarkdownObject[0].Content);
            Assert.AreEqual(italicMarkdown.Content, listMarkdownObject[1].Content);
            Assert.AreEqual(paragraphMarkdown.Content, listMarkdownObject[2].Content);
            Assert.AreEqual(textMarkdownTwo.Content, listMarkdownObject[3].Content);
            Assert.AreEqual(codeMarkdown.Content, listMarkdownObject[4].Content);
            Assert.AreEqual(boldMarkdown.Content, listMarkdownObject[5].Content);
        }
    }
}
