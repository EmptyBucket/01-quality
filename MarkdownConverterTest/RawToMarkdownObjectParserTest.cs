using System.Linq;
using ConverterMarkdown.Markdown;
using ConverterMarkdown.MarkdownObj;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarkdownConverterTest
{
    [TestClass]
    public class RawToMarkdownObjectParserTest
    {
        private RawToMarkdownObjectParser mRawToMarkdownObjectParser;

        public RawToMarkdownObjectParserTest()
        {
            mRawToMarkdownObjectParser = new RawToMarkdownObjectParser();
        }

        [TestMethod]
        public void RawParagraph_Parse_ParagraphMarkdown()
        {
            string content = "";
            string raw = content;
            DocumentMarkdown documentMarkdown = mRawToMarkdownObjectParser.Parse(raw);
            IMarkdownObject paragraph = documentMarkdown.Child.First();
            Assert.IsInstanceOfType(paragraph, typeof(ParagraphMarkdown));
        }

        [TestMethod]
        public void RawItalic_Parse_TypeItalic()
        {
            string content = "content";
            string raw = $"_{content}_";
            DocumentMarkdown documentMarkdown = mRawToMarkdownObjectParser.Parse(raw);
            ParagraphMarkdown paragraph = (ParagraphMarkdown)documentMarkdown.Child.First();
            IMarkdownObject child = paragraph.Child.First();
            Assert.IsInstanceOfType(child, typeof(ItalicMarkdown));
        }

        [TestMethod]
        public void RawItalic_Parse_ItalicContent()
        {
            string content = "content";
            string raw = $"_{content}_";
            DocumentMarkdown documentMarkdown = mRawToMarkdownObjectParser.Parse(raw);
            ParagraphMarkdown paragraph = (ParagraphMarkdown)documentMarkdown.Child.First();
            IMarkdownObject child = paragraph.Child.First();
            TextMarkdown textMarkdown = (TextMarkdown)((ItalicMarkdown)child).Child.First();
            Assert.AreEqual(content, textMarkdown.Content);
        }

        [TestMethod]
        public void RawBold_Parse_TypeBold()
        {
            string content = "content";
            string raw = $"__{content}__";
            DocumentMarkdown documentMarkdown = mRawToMarkdownObjectParser.Parse(raw);
            ParagraphMarkdown paragraph = (ParagraphMarkdown)documentMarkdown.Child.First();
            IMarkdownObject child = paragraph.Child.First();
            Assert.IsInstanceOfType(child, typeof(BoldMarkdown));
        }

        [TestMethod]
        public void RawBold_Parse_ContentBold()
        {
            string content = "content";
            string raw = $"__{content}__";
            DocumentMarkdown documentMarkdown = mRawToMarkdownObjectParser.Parse(raw);
            ParagraphMarkdown paragraph = (ParagraphMarkdown)documentMarkdown.Child.First();
            IMarkdownObject child = paragraph.Child.First();
            TextMarkdown textMarkdown = (TextMarkdown)((BoldMarkdown)child).Child.First();
            Assert.AreEqual(content, textMarkdown.Content);
        }

        [TestMethod]
        public void RawCode_Parse_TypeCode()
        {
            string content = "content";
            string raw = $"`{content}`";
            DocumentMarkdown documentMarkdown = mRawToMarkdownObjectParser.Parse(raw);
            ParagraphMarkdown paragraph = (ParagraphMarkdown)documentMarkdown.Child.First();
            IMarkdownObject child = paragraph.Child.First();
            Assert.IsInstanceOfType(child, typeof(CodeMarkdown));
        }

        [TestMethod]
        public void RawCode_Parse_ContentCode()
        {
            string content = "content";
            string raw = $"`{content}`";
            DocumentMarkdown documentMarkdown = mRawToMarkdownObjectParser.Parse(raw);
            ParagraphMarkdown paragraph = (ParagraphMarkdown)documentMarkdown.Child.First();
            IMarkdownObject child = paragraph.Child.First();
            TextMarkdown textMarkdown = (TextMarkdown)((CodeMarkdown)child).Child.First();
            Assert.AreEqual(content, textMarkdown.Content);
        }

        [TestMethod]
        public void RawMixture_Parse_CorrectMarkdownDocument()
        {
            string italicContent = "italic";
            string boldContent = "bold";
            string codeContent = "code";
            string raw = $"_{italicContent}_ __{boldContent}__ `{codeContent}`";
            DocumentMarkdown documentMarkdown = mRawToMarkdownObjectParser.Parse(raw);
            IMarkdownObject documentChildParagraph = documentMarkdown.Child.ToArray()[0];
            IMarkdownObject childParagraphItalic = ((ParagraphMarkdown)documentChildParagraph).Child.ToArray()[0];
            IMarkdownObject childParagraphBold = ((ParagraphMarkdown)documentChildParagraph).Child.ToArray()[1];
            IMarkdownObject childParagraphCode = ((ParagraphMarkdown)documentChildParagraph).Child.ToArray()[2];
            Assert.IsInstanceOfType(documentChildParagraph, typeof(ParagraphMarkdown));
            Assert.IsInstanceOfType(childParagraphItalic, typeof(ItalicMarkdown));
            Assert.IsInstanceOfType(childParagraphBold, typeof(BoldMarkdown));
            Assert.IsInstanceOfType(childParagraphCode, typeof(CodeMarkdown));
        }

        [TestMethod]
        public void RawAttachmentBoldToItalic_Parse_ItalicBoldMarkdown()
        {
            string content = "content";
            string raw = $"_ __{content}__ _";
            DocumentMarkdown documentMarkdown = mRawToMarkdownObjectParser.Parse(raw);
            IMarkdownObject documentChildParagraph = documentMarkdown.Child.ToArray()[0];
            IMarkdownObject childParagraphItalic = ((ParagraphMarkdown)documentChildParagraph).Child.ToArray()[0];
            IMarkdownObject childItalicBold = ((ItalicMarkdown)childParagraphItalic).Child.ToArray()[0];
            Assert.IsInstanceOfType(documentChildParagraph, typeof(ParagraphMarkdown));
            Assert.IsInstanceOfType(childParagraphItalic, typeof(ItalicMarkdown));
            Assert.IsInstanceOfType(childItalicBold, typeof(BoldMarkdown));
        }

        [TestMethod]
        public void RawAttachmentItalicToCode_Parse_CodeMarkdown()
        {
            string content = "content";
            string raw = $"` __{content}__ `";
            DocumentMarkdown documentMarkdown = mRawToMarkdownObjectParser.Parse(raw);
            IMarkdownObject documentChildParagraph = documentMarkdown.Child.ToArray()[0];
            IMarkdownObject childParagraphCode = ((ParagraphMarkdown)documentChildParagraph).Child.ToArray()[0];
            IMarkdownObject childItalicText = ((CodeMarkdown)childParagraphCode).Child.ToArray()[0];
            Assert.IsInstanceOfType(documentChildParagraph, typeof(ParagraphMarkdown));
            Assert.IsInstanceOfType(childParagraphCode, typeof(CodeMarkdown));
            Assert.IsInstanceOfType(childItalicText, typeof(TextMarkdown));
        }

        [TestMethod]
        public void RawShieldItalic_Parse_TextMarkdown()
        {
            string content = "italic";
            string raw = $@"\_{content}\_";
            DocumentMarkdown documentMarkdown = mRawToMarkdownObjectParser.Parse(raw);
            IMarkdownObject documentChildParagraph = documentMarkdown.Child.ToArray()[0];
            IMarkdownObject childParagraphText = ((ParagraphMarkdown)documentChildParagraph).Child.ToArray()[0];
            Assert.IsInstanceOfType(documentChildParagraph, typeof(ParagraphMarkdown));
            Assert.IsInstanceOfType(childParagraphText, typeof(TextMarkdown));
        }

        [TestMethod]
        public void RawShieldBold_Parse_TextMarkdown()
        {
            string content = "bold";
            string raw = $@"\__{content}\__";
            DocumentMarkdown documentMarkdown = mRawToMarkdownObjectParser.Parse(raw);
            IMarkdownObject documentChildParagraph = documentMarkdown.Child.ToArray()[0];
            IMarkdownObject childParagraphText = ((ParagraphMarkdown)documentChildParagraph).Child.ToArray()[0];
            Assert.IsInstanceOfType(documentChildParagraph, typeof(ParagraphMarkdown));
            Assert.IsInstanceOfType(childParagraphText, typeof(TextMarkdown));
        }

        [TestMethod]
        public void RawShieldCode_Parse_TextMarkdown()
        {
            string content = "code";
            string raw = $@"\`{content}\`";
            DocumentMarkdown documentMarkdown = mRawToMarkdownObjectParser.Parse(raw);
            IMarkdownObject documentChildParagraph = documentMarkdown.Child.ToArray()[0];
            IMarkdownObject childParagraphText = ((ParagraphMarkdown)documentChildParagraph).Child.ToArray()[0];
            Assert.IsInstanceOfType(documentChildParagraph, typeof(ParagraphMarkdown));
            Assert.IsInstanceOfType(childParagraphText, typeof(TextMarkdown));
        }

        [TestMethod]
        public void RawNumberToItalic_Parse_TextMarkdown()
        {
            string content = "_12_3";
            string raw = $"{content}";
            DocumentMarkdown documentMarkdown = mRawToMarkdownObjectParser.Parse(raw);
            IMarkdownObject documentChildParagraph = documentMarkdown.Child.ToArray()[0];
            IMarkdownObject childParagraphText = ((ParagraphMarkdown)documentChildParagraph).Child.ToArray()[0];
            Assert.IsInstanceOfType(documentChildParagraph, typeof(ParagraphMarkdown));
            Assert.IsInstanceOfType(childParagraphText, typeof(TextMarkdown));
        }

        [TestMethod]
        public void RawUnmatched_Parse_Text()
        {
            string content = "__непарные _символы не считаются `выделением";
            string raw = $"{content}";
            DocumentMarkdown documentMarkdown = mRawToMarkdownObjectParser.Parse(raw);
            IMarkdownObject documentChildParagraph = documentMarkdown.Child.ToArray()[0];
            IMarkdownObject childParagraphText = ((ParagraphMarkdown)documentChildParagraph).Child.ToArray()[0];
            Assert.IsInstanceOfType(documentChildParagraph, typeof(ParagraphMarkdown));
            Assert.IsInstanceOfType(childParagraphText, typeof(TextMarkdown));
        }
    }
}
