using NetworkLogParseManager.Factories;
using NetworkLogParseManager.Managers;
using NetworkLogParseManager.Models;
using NetworkLogParseManager.Parsers;
using NetworkLogParseManager.Parsers.ElementParsers;
using NetworkLogParseManager.StreamFiles;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace XUnitTestLogParser
{
    public class ParsersTest
    {
        private static LogLineParser GetLogLineParser()
        {
            List<ParsingMap> parsingMapList = new List<ParsingMap>()
            {
                new ParsingMap()
                {
                    SourceField = null,
                    TargetField = new LogField() { Index = 0, Id = "provider" },
                    Parser = new PrintElementLogParser() { Value = "MINHA CDN" }
                },
                new ParsingMap()
                {
                    SourceField = new LogField() { Index = 3, Id = "http-request" },
                    TargetField = new LogField() { Index = 1, Id = "http-method" },
                    Parser = new SplitElementLogParser() { Index = 0, Separator = " " }
                },
                new ParsingMap()
                {
                    SourceField = new LogField() { Index = 1, Id = "status-code" },
                    TargetField = new LogField() { Index = 2, Id = "status-code" },
                    Parser = new CloneElementLogParser()
                },
                new ParsingMap()
                {
                    SourceField = new LogField() { Index = 3, Id = "http-request" },
                    TargetField = new LogField() { Index = 3, Id = "uri-path" },
                    Parser = new SplitElementLogParser() { Index = 1, Separator = " " }
                },
                new ParsingMap()
                {
                    SourceField = new LogField() { Index = 4, Id = "time-taken" },
                    TargetField = new LogField() { Index = 4, Id = "time-taken" },
                    Parser = new RoundElementLogParser() { Digits = 0 }
                },
                new ParsingMap()
                {
                    SourceField = new LogField() { Index = 0, Id = "response-size" },
                    TargetField = new LogField() { Index = 5, Id = "response-size" },
                    Parser = new CloneElementLogParser()
                },
                new ParsingMap()
                {
                    SourceField = new LogField() { Index = 2, Id = "cache-status" },
                    TargetField = new LogField() { Index = 6, Id = "cache-status" },
                    Parser = new MapOrCloneElementLogParser() {
                        MapElements = new Dictionary<string, string>(){ {"INVALIDATE", "REFRESH_HIT"} } }
                }
            };

            LogFieldExpression logFieldExpression = new LogFieldExpression()
            {
                Delimiter = '"',
                SeparatorPattern = @"\s"
            };

            LogLineParser logLineParser = new LogLineParser(parsingMapList);
            logLineParser.SourceLogLine = new LogLine() { ExpressionStructure = logFieldExpression, Separator = '|' };
            logLineParser.TargetLogLine = new LogLine() { ExpressionStructure = logFieldExpression, Separator = ' ' };

            return logLineParser;
        }

        [Fact]
        public void ShouldParseLine()
        {
            LogLineParser logLineParser = GetLogLineParser();

            string originalValue = "312|200|HIT|\"GET /examplefile.txt HTTP/1.1\"|100.2";
            string expectedValue = "\"MINHA CDN\" GET 200 /examplefile.txt 100 312 HIT";

            Assert.Equal(expectedValue, logLineParser.Parse(originalValue));
        }

        [Fact]
        public void ShouldNotParseLineWhenIndexedFieldNotExistsInSourceLine()
        {
            List<ParsingMap> parsingMapList = new List<ParsingMap>()
            {
                new ParsingMap()
                {
                    SourceField = new LogField() { Index = 0 },
                    TargetField = new LogField() { Index = 1 },
                    Parser = new CloneElementLogParser()
                },
                new ParsingMap()
                {
                    SourceField = new LogField() { Index = 2 },
                    TargetField = new LogField() { Index = 2 },
                    Parser = new CloneElementLogParser()
                }
            };

            LogFieldExpression logFieldExpression = new LogFieldExpression()
            {
                Delimiter = '"',
                SeparatorPattern = @"\s"
            };

            LogLine sourceLogLine = new LogLine() { ExpressionStructure = logFieldExpression, Separator = '|' };
            LogLine targetLogLine = new LogLine() { ExpressionStructure = logFieldExpression, Separator = ' ' };

            LogLineParser logLineParser = new LogLineParser(sourceLogLine, targetLogLine, parsingMapList);

            string originalValue = "312|HIT";

            Assert.Throws<IndexOutOfRangeException>(() => logLineParser.Parse(originalValue));
        }

        [Fact]
        public void ShouldNotParseLineWhenAtLeastOneElementParserFail()
        {
            List<ParsingMap> parsingMapList = new List<ParsingMap>()
            {
                new ParsingMap()
                {
                    SourceField = new LogField() { Index = 0 },
                    TargetField = new LogField() { Index = 0 },
                    Parser = new CloneElementLogParser()
                },
                new ParsingMap()
                {
                    SourceField = new LogField() { Index = 1 },
                    TargetField = new LogField() { Index = 1 },
                    Parser = new RoundElementLogParser() { Digits = 2 }
                }
            };

            LogFieldExpression logFieldExpression = new LogFieldExpression()
            {
                Delimiter = '"',
                SeparatorPattern = @"\s"
            };

            LogLine sourceLogLine = new LogLine() { ExpressionStructure = logFieldExpression, Separator = '|' };
            LogLine targetLogLine = new LogLine() { ExpressionStructure = logFieldExpression, Separator = ' ' };

            LogLineParser logLineParser = new LogLineParser(sourceLogLine, targetLogLine, parsingMapList);

            string originalValue = "312|5.a";

            Assert.Throws<FormatException>(() => logLineParser.Parse(originalValue));
        }

        [Fact]
        public void ShouldBuildLogLineFieldsArray()
        {
            LogLineParser logLineParser = GetLogLineParser();
            logLineParser.PopulateLogLineFieldsFromParsingMapList();
            
            LogField[] sourceFieldsArray = logLineParser.ParsingMapList.Select(m => m.SourceField).ToArray();
            LogField[] targetFieldsArray = logLineParser.ParsingMapList.Select(m => m.TargetField).ToArray();

            Assert.Equal(sourceFieldsArray, logLineParser.SourceLogLine.LogFieldArray);
            Assert.Equal(targetFieldsArray, logLineParser.TargetLogLine.LogFieldArray);
        }

        [Fact]
        public void ShouldParseFiles()
        {
            LogLineParser logLineParser = GetLogLineParser();
            StringBuilder expectedStringBuilder = new StringBuilder();
            StringBuilder actualStringBuilder = new StringBuilder();
            ParserFactory parserFactory = new FormatIn01ToFormatOut01ParserFactory();

            string originalValue1 = "312|200|HIT|\"GET /examplefile.txt HTTP/1.1\"|100.2";
            string expectedValue1 = "\"MINHA CDN\" GET 200 /examplefile.txt 100 312 HIT";
            string originalValue2 = "101|200|MISS|\"POST /myImages HTTP/1.1\"|319.4";
            string expectedValue2 = "\"MINHA CDN\" POST 200 /myImages 319 101 MISS";

            expectedStringBuilder.Append(expectedValue1);
            expectedStringBuilder.Append(expectedValue2);

            Queue<string> readingQueue = new Queue<string>();
            readingQueue.Enqueue(originalValue1);
            readingQueue.Enqueue(originalValue2);

            Moq.Mock<IStreamFileReader> mockReader = new Moq.Mock<IStreamFileReader>();
            mockReader.Setup(r => r.ReadLine()).Returns(() => {
                if (readingQueue.Count == 0)
                    return null;
                else
                    return readingQueue.Dequeue(); 
            });

            Moq.Mock<IStreamFileWriter> mockWriter = new Moq.Mock<IStreamFileWriter>();
            mockWriter.Setup(w => w.WriteLine(It.IsAny<string>())).Callback<string>(parsedLine => actualStringBuilder.Append(parsedLine));

            Moq.Mock<IParserFactory> mockParserFactory = new Moq.Mock<IParserFactory>();
            mockParserFactory.Setup(factory =>
                factory.CreateLogLineParser(It.IsAny<string>(), It.IsAny<AbstractLogFactory>(), It.IsAny<AbstractLogFactory>()))
                .Returns(logLineParser);
            mockParserFactory.Setup(factory =>
                factory.CreateLogParsingManager(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new LogParsingManager(mockReader.Object, mockWriter.Object, logLineParser, 10, 10));

            LogParsingManager logParsingManager = mockParserFactory.Object.CreateLogParsingManager(String.Empty, String.Empty, String.Empty);
            logParsingManager.Parse();

            Assert.Equal(expectedStringBuilder.ToString(), actualStringBuilder.ToString());
        }
    }
}