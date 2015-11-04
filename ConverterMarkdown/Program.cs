using System;
using System.IO;
using ConverterMarkdown.Markdown;

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

            string rawFileStr;
            using (var fileStream = new FileStream(pathInMarkdown, FileMode.Open))
            using (var streamReader = new StreamReader(fileStream))
                rawFileStr = streamReader.ReadToEnd();

            string html = ConverterMarkdown<MarkdownObjectToHTMLParser>.Parse(rawFileStr);

            string pathOut;
            try
            {
                pathOut = args[1];
            }
            catch (IndexOutOfRangeException)
            {
                pathOut = Path.Combine(Environment.CurrentDirectory, "OutHTML.html");
            }
            using (var fileStream = new FileStream(pathOut, FileMode.Create))
            using (var streamWriter = new StreamWriter(fileStream))
                streamWriter.Write(html);
        }
    }
}
