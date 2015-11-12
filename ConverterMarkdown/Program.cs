using System;
using System.IO;
using ConverterMarkdown.Markdown;
using ConverterMarkdown.MarkdownObjectToTypeParser;

namespace ConverterMarkdown
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathInMarkdown = args[0];
            string typeInFile = Path.GetExtension(pathInMarkdown);
            if (typeInFile != ".md")
                throw new FileFormatException("Expected type .md, received {typeInFile}");

            string rawFileStr = File.ReadAllText(pathInMarkdown);

            var toHTMLParser = new MarkdownObjectToHTMLParser();
            var converterMarkdown = new MarkdownConverter(toHTMLParser);
            string html = converterMarkdown.Convert(rawFileStr);

            string pathOut;
            try
            {
                pathOut = args[1];
            }
            catch (IndexOutOfRangeException)
            {
                pathOut = Path.Combine(Environment.CurrentDirectory, "OutHTML.html");
            }
            File.WriteAllText(pathOut, html);
        }
    }
}
