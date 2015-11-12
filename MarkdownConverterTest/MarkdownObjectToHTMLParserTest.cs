using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConverterMarkdown.MarkdownObj;
using ConverterMarkdown.MarkdownObjectToTypeParser;

namespace MarkdownConverterTest
{
    [TestClass]
    public class MarkdownObjectToHTMLParserTest
    {
        [TestMethod]
        public void MarkdownObjectParagraph_Parse_HTMLParagraph()
        {
            ParagraphMarkdown paragraphMarkdown = new ParagraphMarkdown();
            DocumentMarkdown documentMarkdown = new DocumentMarkdown(new List<IMarkdownObject> { paragraphMarkdown });
            MarkdownObjectToHTMLParser markdownObjectToHTMLparser = new MarkdownObjectToHTMLParser();
            string html = markdownObjectToHTMLparser.Parse(documentMarkdown);
            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorParagraph.OpenTag));
            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorParagraph.CloseTag));
            Assert.IsTrue(html.IndexOf(markdownObjectToHTMLparser.InterpretatorParagraph.OpenTag) < html.IndexOf(markdownObjectToHTMLparser.InterpretatorParagraph.CloseTag));
        }

        [TestMethod]
        public void MarkdownObjectItalic_Parse_HTMLItalic()
        {
            ItalicMarkdown italicMarkdown = new ItalicMarkdown();
            ParagraphMarkdown paragraphMarkdown = new ParagraphMarkdown(new List<IMarkdownObject> { italicMarkdown });
            DocumentMarkdown documentMarkdown = new DocumentMarkdown(new List<IMarkdownObject> { paragraphMarkdown });
            MarkdownObjectToHTMLParser markdownObjectToHTMLparser = new MarkdownObjectToHTMLParser();
            string html = markdownObjectToHTMLparser.Parse(documentMarkdown);
            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorItalic.OpenTag));
            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorItalic.CloseTag));
            Assert.IsTrue(html.IndexOf(markdownObjectToHTMLparser.InterpretatorItalic.OpenTag) < html.IndexOf(markdownObjectToHTMLparser.InterpretatorItalic.CloseTag));
        }

        [TestMethod]
        public void MarkdownObjectBold_Parse_HTMLBold()
        {
            BoldMarkdown italicMarkdown = new BoldMarkdown();
            ParagraphMarkdown paragraphMarkdown = new ParagraphMarkdown(new List<IMarkdownObject> { italicMarkdown });
            DocumentMarkdown documentMarkdown = new DocumentMarkdown(new List<IMarkdownObject> { paragraphMarkdown });
            MarkdownObjectToHTMLParser markdownObjectToHTMLparser = new MarkdownObjectToHTMLParser();
            string html = markdownObjectToHTMLparser.Parse(documentMarkdown);
            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorBold.OpenTag));
            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorBold.CloseTag));
            Assert.IsTrue(html.IndexOf(markdownObjectToHTMLparser.InterpretatorBold.OpenTag) < html.IndexOf(markdownObjectToHTMLparser.InterpretatorBold.CloseTag));
        }

        [TestMethod]
        public void MarkdownObjectCode_Parse_HTMLCode()
        {
            CodeMarkdown italicMarkdown = new CodeMarkdown();
            ParagraphMarkdown paragraphMarkdown = new ParagraphMarkdown(new List<IMarkdownObject> { italicMarkdown });
            DocumentMarkdown documentMarkdown = new DocumentMarkdown(new List<IMarkdownObject> { paragraphMarkdown });
            MarkdownObjectToHTMLParser markdownObjectToHTMLparser = new MarkdownObjectToHTMLParser();
            string html = markdownObjectToHTMLparser.Parse(documentMarkdown);
            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorCode.OpenTag));
            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorCode.CloseTag));
            Assert.IsTrue(html.IndexOf(markdownObjectToHTMLparser.InterpretatorCode.OpenTag) < html.IndexOf(markdownObjectToHTMLparser.InterpretatorCode.CloseTag));
        }

        [TestMethod]
        public void MarkdownObjectDocument_Parse_HTMLBody()
        {
            DocumentMarkdown documentMarkdown = new DocumentMarkdown();
            MarkdownObjectToHTMLParser markdownObjectToHTMLparser = new MarkdownObjectToHTMLParser();
            string html = markdownObjectToHTMLparser.Parse(documentMarkdown);
            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorDocument.OpenTag));
            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorDocument.CloseTag));
            Assert.IsTrue(html.IndexOf(markdownObjectToHTMLparser.InterpretatorDocument.OpenTag) < html.IndexOf(markdownObjectToHTMLparser.InterpretatorDocument.CloseTag));
        }

        [TestMethod]
        public void MarkdownObjectDocumentMixture_Parse_HTMLCorrect()
        {
            ItalicMarkdown italicMarkdown = new ItalicMarkdown();
            BoldMarkdown boldMarkdown = new BoldMarkdown();
            CodeMarkdown codeMarkdown = new CodeMarkdown();
            ParagraphMarkdown paragraphMarkdown = new ParagraphMarkdown(new List<IMarkdownObject> { italicMarkdown, boldMarkdown, codeMarkdown });
            DocumentMarkdown documentMarkdown = new DocumentMarkdown(new List<IMarkdownObject> { paragraphMarkdown });
            MarkdownObjectToHTMLParser markdownObjectToHTMLparser = new MarkdownObjectToHTMLParser();
            string html = markdownObjectToHTMLparser.Parse(documentMarkdown);
            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorParagraph.OpenTag));
            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorParagraph.CloseTag));
            Assert.IsTrue(html.IndexOf(markdownObjectToHTMLparser.InterpretatorParagraph.OpenTag) < html.IndexOf(markdownObjectToHTMLparser.InterpretatorParagraph.CloseTag));

            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorItalic.OpenTag));
            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorItalic.CloseTag));
            Assert.IsTrue(html.IndexOf(markdownObjectToHTMLparser.InterpretatorItalic.OpenTag) < html.IndexOf(markdownObjectToHTMLparser.InterpretatorItalic.CloseTag));

            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorBold.OpenTag));
            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorBold.CloseTag));
            Assert.IsTrue(html.IndexOf(markdownObjectToHTMLparser.InterpretatorBold.OpenTag) < html.IndexOf(markdownObjectToHTMLparser.InterpretatorBold.CloseTag));

            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorCode.OpenTag));
            Assert.IsTrue(html.Contains(markdownObjectToHTMLparser.InterpretatorCode.CloseTag));
            Assert.IsTrue(html.IndexOf(markdownObjectToHTMLparser.InterpretatorCode.OpenTag) < html.IndexOf(markdownObjectToHTMLparser.InterpretatorCode.CloseTag));

            Assert.IsTrue(html.IndexOf(markdownObjectToHTMLparser.InterpretatorItalic.CloseTag) < html.IndexOf(markdownObjectToHTMLparser.InterpretatorBold.OpenTag));
            Assert.IsTrue(html.IndexOf(markdownObjectToHTMLparser.InterpretatorBold.CloseTag) < html.IndexOf(markdownObjectToHTMLparser.InterpretatorCode.OpenTag));
        }

        [TestMethod]
        public void MarkdownObjectDocumentAttacment_Parse_HTMLCorrect()
        {
            BoldMarkdown boldMarkdown = new BoldMarkdown();
            ItalicMarkdown italicMarkdown = new ItalicMarkdown(new List<IMarkdownObject> { boldMarkdown });
            ParagraphMarkdown paragraphMarkdown = new ParagraphMarkdown(new List<IMarkdownObject> { italicMarkdown });
            DocumentMarkdown documentMarkdown = new DocumentMarkdown(new List<IMarkdownObject> { paragraphMarkdown });
            MarkdownObjectToHTMLParser markdownObjectToHTMLparser = new MarkdownObjectToHTMLParser();
            string html = markdownObjectToHTMLparser.Parse(documentMarkdown);

            Assert.IsTrue(html.IndexOf(markdownObjectToHTMLparser.InterpretatorItalic.OpenTag) < html.IndexOf(markdownObjectToHTMLparser.InterpretatorItalic.CloseTag));
            Assert.IsTrue(html.IndexOf(markdownObjectToHTMLparser.InterpretatorBold.OpenTag) < html.IndexOf(markdownObjectToHTMLparser.InterpretatorBold.CloseTag));
            Assert.IsTrue(html.IndexOf(markdownObjectToHTMLparser.InterpretatorItalic.OpenTag) < html.IndexOf(markdownObjectToHTMLparser.InterpretatorBold.OpenTag) && html.IndexOf(markdownObjectToHTMLparser.InterpretatorItalic.CloseTag) > html.IndexOf(markdownObjectToHTMLparser.InterpretatorBold.CloseTag));
        }
    }
}
