using NetworkLogParseManager.Managers;
using NetworkLogParseManager.Parsers;
using NetworkLogParseManager.Providers;
using NetworkLogParseManager.StreamFiles;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Factories
{
    public class FormatIn01ToFormatOut01ParserFactory : ParserFactory
    {
        public const string PARSING_MAP_JSON_PATH = "./JSON/FormatIn01ToFormatOut01ParsingMap.json";

        public FormatIn01ToFormatOut01ParserFactory()
        {
        }

        public override LogParsingManager CreateLogParsingManager(string sourceUrlFileName, string targetLocalFilePath, 
            AbstractLogFactory formatIn01Factory, AbstractLogFactory formatOut01Factory, string parsingMapJsonFileName = PARSING_MAP_JSON_PATH)
        {
            LogParsingManager logParsingManager;
            ConfigProvider cfgProvider = new ConfigProvider();

            try
            {
                LogLineParser logLineParser = CreateLogLineParser(parsingMapJsonFileName, formatIn01Factory, formatOut01Factory);

                IStreamFileReader streamFileReader = formatIn01Factory.CreateStreamFileReader(sourceUrlFileName);
                IStreamFileWriter streamFileWriter =
                    formatOut01Factory.CreateStreamFileWriter(targetLocalFilePath);

                logParsingManager = new LogParsingManager(streamFileReader,
                                                            streamFileWriter,
                                                            logLineParser,
                                                            cfgProvider.ThresholdQueueToParse,
                                                            cfgProvider.ThresholdQueueToWrite);
                return logParsingManager;
            }
            catch
            {
                throw;
            }
        }
    }
}
