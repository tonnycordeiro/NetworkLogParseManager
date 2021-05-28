using NetworkLogParseManager.Factories;
using NetworkLogParseManager.Managers;
using NetworkLogParseManager.Parsers;
using NetworkLogParseManager.StreamFiles;
using Microsoft.Extensions.Configuration;
using System;

namespace NetworkLogParseManager
{
    class Program
    {
        static void Main(string[] args)
        {
            FormatIn01ToFormatOut01ParserFactory parserFactory = new FormatIn01ToFormatOut01ParserFactory();

            if (args.Length < 2)
            {
                Console.WriteLine("The correct arguments should be: [source url] [target file path]");
                return;
            }

            try
            {
                LogParsingManager logParsingManager = parserFactory.CreateLogParsingManager(args[0], args[1], new FormatIn01LogFactory(), new FormatOut01LogFactory());
                logParsingManager.Parse();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
