using System.IO;
using ConverterMarkdown.Markdown;
using ConverterMarkdown.MarkdownObjectToTypeParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarkdownConverterTest
{
    [TestClass]
    public class ConverterMarkdownTest
    {
        [TestMethod]
        public void MarkdownText_ConvertToHTML_CorrectHTMLText()
        {
            MarkdownObjectToHTMLParser toHTMLParser = new MarkdownObjectToHTMLParser();
            MarkdownToTypeConverter converterMarkdown = new MarkdownToTypeConverter(toHTMLParser);
            string rawMarkdown = File.ReadAllText("/Resources/MarkdownText.md");
            string html = converterMarkdown.Convert(rawMarkdown);

            string exceptedHTML = File.ReadAllText("/Resources/HTMLText.html");
            Assert.AreEqual(exceptedHTML, html);
        }
    }
}
