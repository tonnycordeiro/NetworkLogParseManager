using NetworkLogParseManager.Managers;
using NetworkLogParseManager.Parsers;

namespace NetworkLogParseManager.Factories
{
    public interface IParserFactory
    {
        LogLineParser CreateLogLineParser(string parsingMapJsonFilePath, AbstractLogFactory sourceLogFactory, AbstractLogFactory targetLogFactory);
        LogParsingManager CreateLogParsingManager(string sourceUrlFileName, string targetLocalFilePath, string parsingMapJsonFileName);
    }
}