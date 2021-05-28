using NetworkLogParseManager.Converters;
using NetworkLogParseManager.Managers;
using NetworkLogParseManager.Parsers;
using Newtonsoft.Json;
using System.IO;

namespace NetworkLogParseManager.Factories
{
    public abstract class ParserFactory : IParserFactory
    {
        public LogLineParser CreateLogLineParser(string parsingMapJsonFilePath, AbstractLogFactory sourceLogFactory, AbstractLogFactory targetLogFactory)
        {
            string jsonString;
            LogLineParser logLineParser;

            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
            settings.Converters.Add(new ElementLogParserJsonConverter());
            jsonString = File.ReadAllText(parsingMapJsonFilePath);

            logLineParser = JsonConvert.DeserializeObject<LogLineParser>(jsonString, settings);
            logLineParser.SourceLogLine = sourceLogFactory.CreateLogLine();
            logLineParser.TargetLogLine = targetLogFactory.CreateLogLine();

            logLineParser.PopulateLogLineFieldsFromParsingMapList();

            return logLineParser;
        }

        public abstract LogParsingManager CreateLogParsingManager(string sourceUrlFileName, string targetLocalFilePath, 
                                                        AbstractLogFactory formatIn01Factory, AbstractLogFactory formatOut01Factory, string parsingMapJsonFileName);
    }
}