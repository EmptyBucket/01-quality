using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConverterMarkdown.MarkdownObj;
using ConverterMarkdown.MarkdownObjectToTypeParser;

namespace MarkdownConverterTest
{
    [TestClass]
    public class MarkdownObjectToHTMLparserTest
    {
        private MarkdownObjectToHTMLParser mMarkdownObjectToHTMLparser;

        public MarkdownObjectToHTMLparserTest()
        {
            mMarkdownObjectToHTMLparser = new MarkdownObjectToHTMLParser();
        }

        [TestMethod]
        public void MarkdownObjectParagraph_Parse_HTMLParagraph()
        {
            var paragraphMarkdown = new ParagraphMarkdown();
            var documentMarkdown = new DocumentMarkdown(new List<IMarkdownObject> { paragraphMarkdown });
            string html = mMarkdownObjectToHTMLparser.Parse(documentMarkdown);
            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorParagraph.OpenTag));
            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorParagraph.CloseTag));
            Assert.IsTrue(html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorParagraph.OpenTag) < html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorParagraph.CloseTag));
        }

        [TestMethod]
        public void MarkdownObjectItalic_Parse_HTMLItalic()
        {
            var italicMarkdown = new ItalicMarkdown();
            var paragraphMarkdown = new ParagraphMarkdown(new List<IMarkdownObject> { italicMarkdown });
            var documentMarkdown = new DocumentMarkdown(new List<IMarkdownObject> { paragraphMarkdown });
            string html = mMarkdownObjectToHTMLparser.Parse(documentMarkdown);
            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorItalic.OpenTag));
            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorItalic.CloseTag));
            Assert.IsTrue(html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorItalic.OpenTag) < html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorItalic.CloseTag));
        }

        [TestMethod]
        public void MarkdownObjectBold_Parse_HTMLBold()
        {
            var italicMarkdown = new BoldMarkdown();
            var paragraphMarkdown = new ParagraphMarkdown(new List<IMarkdownObject> { italicMarkdown });
            var documentMarkdown = new DocumentMarkdown(new List<IMarkdownObject> { paragraphMarkdown });
            string html = mMarkdownObjectToHTMLparser.Parse(documentMarkdown);
            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorBold.OpenTag));
            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorBold.CloseTag));
            Assert.IsTrue(html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorBold.OpenTag) < html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorBold.CloseTag));
        }

        [TestMethod]
        public void MarkdownObjectCode_Parse_HTMLCode()
        {
            var italicMarkdown = new CodeMarkdown();
            var paragraphMarkdown = new ParagraphMarkdown(new List<IMarkdownObject> { italicMarkdown });
            var documentMarkdown = new DocumentMarkdown(new List<IMarkdownObject> { paragraphMarkdown });
            string html = mMarkdownObjectToHTMLparser.Parse(documentMarkdown);
            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorCode.OpenTag));
            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorCode.CloseTag));
            Assert.IsTrue(html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorCode.OpenTag) < html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorCode.CloseTag));
        }

        [TestMethod]
        public void MarkdownObjectDocument_Parse_HTMLBody()
        {
            var documentMarkdown = new DocumentMarkdown();
            string html = mMarkdownObjectToHTMLparser.Parse(documentMarkdown);
            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorDocument.OpenTag));
            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorDocument.CloseTag));
            Assert.IsTrue(html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorDocument.OpenTag) < html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorDocument.CloseTag));
        }

        [TestMethod]
        public void MarkdownObjectDocumentMixture_Parse_HTMLCorrect()
        {
            var italicMarkdown = new ItalicMarkdown();
            var boldMarkdown = new BoldMarkdown();
            var codeMarkdown = new CodeMarkdown();
            var paragraphMarkdown = new ParagraphMarkdown(new List<IMarkdownObject> { italicMarkdown, boldMarkdown, codeMarkdown });
            var documentMarkdown = new DocumentMarkdown(new List<IMarkdownObject> { paragraphMarkdown });
            string html = mMarkdownObjectToHTMLparser.Parse(documentMarkdown);
            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorParagraph.OpenTag));
            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorParagraph.CloseTag));
            Assert.IsTrue(html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorParagraph.OpenTag) < html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorParagraph.CloseTag));

            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorItalic.OpenTag));
            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorItalic.CloseTag));
            Assert.IsTrue(html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorItalic.OpenTag) < html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorItalic.CloseTag));

            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorBold.OpenTag));
            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorBold.CloseTag));
            Assert.IsTrue(html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorBold.OpenTag) < html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorBold.CloseTag));

            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorCode.OpenTag));
            Assert.IsTrue(html.Contains(mMarkdownObjectToHTMLparser.InterpretatorCode.CloseTag));
            Assert.IsTrue(html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorCode.OpenTag) < html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorCode.CloseTag));

            Assert.IsTrue(html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorItalic.CloseTag) < html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorBold.OpenTag));
            Assert.IsTrue(html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorBold.CloseTag) < html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorCode.OpenTag));
        }

        [TestMethod]
        public void MarkdownObjectDocumentAttacment_Parse_HTMLCorrect()
        {
            var boldMarkdown = new BoldMarkdown();
            var italicMarkdown = new ItalicMarkdown(new List<IMarkdownObject> { boldMarkdown });
            var paragraphMarkdown = new ParagraphMarkdown(new List<IMarkdownObject> { italicMarkdown });
            var documentMarkdown = new DocumentMarkdown(new List<IMarkdownObject> { paragraphMarkdown });
            string html = mMarkdownObjectToHTMLparser.Parse(documentMarkdown);

            Assert.IsTrue(html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorItalic.OpenTag) < html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorItalic.CloseTag));
            Assert.IsTrue(html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorBold.OpenTag) < html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorBold.CloseTag));
            Assert.IsTrue(html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorItalic.OpenTag) < html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorBold.OpenTag) && html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorItalic.CloseTag) > html.IndexOf(mMarkdownObjectToHTMLparser.InterpretatorBold.CloseTag));
        }
    }
}
