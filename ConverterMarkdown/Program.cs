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
            if (args.Length == 0)
            {
                Console.WriteLine("Type 1 command-line argument - the path to the input file markdaun 2 command-line argument (optional) - the path to the output file html");
                return;
            }
            string pathInMarkdown = args[0];
            string typeInFile = Path.GetExtension(pathInMarkdown);
            if (typeInFile != ".md")
            {
                Console.WriteLine($"Expected type in file .md, received {typeInFile}");
                return;
            }

            string rawFileStr = File.ReadAllText(pathInMarkdown);

            var toHTMLParser = new MarkdownObjectToHTMLParser();
            var converterMarkdown = new MarkdownConverter(toHTMLParser);
            string html = converterMarkdown.Convert(rawFileStr);

            string pathOut;
            if(args.Length < 2)
                pathOut = Path.Combine(Environment.CurrentDirectory, "OutHTML.html");
            else
            {
                pathOut = args[1];
                string typeOutFile = Path.GetExtension(pathOut);
                if (typeOutFile != ".html")
                {
                    Console.WriteLine($"Expected type out file html, received {typeOutFile}");
                    return;
                }
            }
            File.WriteAllText(pathOut, html);
        }
    }
}
