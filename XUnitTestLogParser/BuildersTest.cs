using NetworkLogParseManager.Builders;
using NetworkLogParseManager.Models;
using System;
using Xunit;

namespace XUnitTestLogParser
{
    public class BuildersTest
    {
        [Fact]
        public void ShouldBuildExpressionFieldWithDelimiters()
        {
            string fieldWithTwoWords = "WORD1 WORD2";
            string fieldWithMoreWords = "WORD1 WORD2 WORD3";

            LogFieldExpression logFieldExpression = new LogFieldExpression()
            {
                Delimiter = '"',
                SeparatorPattern = @"\s"
            };
            ILogFieldBuilder logFieldBuilder = new LogFieldBuilder(logFieldExpression);

            Assert.Equal(logFieldBuilder.Build(fieldWithTwoWords),$"\"{fieldWithTwoWords}\"");
            Assert.Equal(logFieldBuilder.Build(fieldWithMoreWords), $"\"{fieldWithMoreWords}\"");
        }

        [Fact]
        public void ShouldBuildSimpleFieldWithoutDelimiters()
        {
            string field = "WORD1";

            LogFieldExpression logFieldExpression = new LogFieldExpression()
            {
                Delimiter = '"',
                SeparatorPattern = @"\s"
            };
            ILogFieldBuilder logFieldBuilder = new LogFieldBuilder(logFieldExpression);

            Assert.Equal(logFieldBuilder.Build(field), field);
        }

        [Fact]
        public void ShouldSplitLogLine()
        {
            string lineContent = "312|200|HIT|\"GET / examplefile.txt HTTP / 1.1\"|100.2";
            string[] expectedFields = new string[] { "312","200","HIT","GET / examplefile.txt HTTP / 1.1","100.2"};

            LogFieldExpression logFieldExpression = new LogFieldExpression()
            {
                Delimiter = '"',
                SeparatorPattern = @"\s"
            };
            LogLine logLine = new LogLine() { ExpressionStructure = logFieldExpression, Separator = '|' };
            ILogLineBuilder logLineBuilder = new LogLineBuilder(logLine);
            string[] actualFields = logLineBuilder.Split(lineContent);

            Assert.Equal(expectedFields, actualFields);
        }

        [Fact]
        public void ShouldSplitLogLineWithLineSeparatorEqualToExpressionSeparator()
        {
            string lineContent = "312 200 HIT \"GET / examplefile.txt HTTP / 1.1\" 100.2";
            string[] expectedFields = new string[] { "312", "200", "HIT", "GET / examplefile.txt HTTP / 1.1", "100.2" };

            LogFieldExpression logFieldExpression = new LogFieldExpression()
            {
                Delimiter = '"',
                SeparatorPattern = @"\s"
            };
            LogLine logLine = new LogLine() { ExpressionStructure = logFieldExpression, Separator = ' ' };
            ILogLineBuilder logLineBuilder = new LogLineBuilder(logLine);

            string[] actualFields = logLineBuilder.Split(lineContent);

            Assert.Equal(expectedFields, actualFields);
        }

        [Fact]
        public void ShouldSplitLogLineWithExpressionAtStart()
        {
            string lineContent = "\"GET / examplefile.txt HTTP / 1.1\"|312|200|HIT|100.2";
            string[] expectedFields = new string[] { "GET / examplefile.txt HTTP / 1.1", "312", "200", "HIT", "100.2" };

            LogFieldExpression logFieldExpression = new LogFieldExpression()
            {
                Delimiter = '"',
                SeparatorPattern = @"\s"
            };
            LogLine logLine = new LogLine() { ExpressionStructure = logFieldExpression, Separator = '|' };
            ILogLineBuilder logLineBuilder = new LogLineBuilder(logLine);
            string[] actualFields = logLineBuilder.Split(lineContent);

            Assert.Equal(expectedFields, actualFields);
        }

        [Fact]
        public void ShouldSplitLogLineWithExpressionAtEnd()
        {
            string lineContent = "312|200|HIT|100.2|\"GET / examplefile.txt HTTP / 1.1\"";
            string[] expectedFields = new string[] { "312", "200", "HIT", "100.2", "GET / examplefile.txt HTTP / 1.1" };

            LogFieldExpression logFieldExpression = new LogFieldExpression()
            {
                Delimiter = '"',
                SeparatorPattern = @"\s"
            };
            LogLine logLine = new LogLine() { ExpressionStructure = logFieldExpression, Separator = '|' };
            ILogLineBuilder logLineBuilder = new LogLineBuilder(logLine);
            string[] actualFields = logLineBuilder.Split(lineContent);

            Assert.Equal(expectedFields, actualFields);
        }

        [Fact]
        public void ShouldSplitLogLineWithEqualExpressionsValue()
        {
            string lineContent = "312|200|HIT|100.2|\"GET / examplefile.txt HTTP / 1.1\"|\"GET / examplefile.txt HTTP / 1.1\"";
            string[] expectedFields = new string[] { "312", "200", "HIT", "100.2", "GET / examplefile.txt HTTP / 1.1", "GET / examplefile.txt HTTP / 1.1" };

            LogFieldExpression logFieldExpression = new LogFieldExpression()
            {
                Delimiter = '"',
                SeparatorPattern = @"\s"
            };
            LogLine logLine = new LogLine() { ExpressionStructure = logFieldExpression, Separator = '|' };
            ILogLineBuilder logLineBuilder = new LogLineBuilder(logLine);
            string[] actualFields = logLineBuilder.Split(lineContent);

            Assert.Equal(expectedFields, actualFields);
        }

        [Fact]
        public void ShouldBuildLogLine()
        {
            string expectedLineContent = "312|200|HIT|\"GET / examplefile.txt HTTP / 1.1\"|100.2|\"POST / toys.txt HTTP / 1.1\"";
            string[] fields = new string[] { "312", "200", "HIT", "GET / examplefile.txt HTTP / 1.1", "100.2","POST / toys.txt HTTP / 1.1" };

            LogFieldExpression logFieldExpression = new LogFieldExpression()
            {
                Delimiter = '"',
                SeparatorPattern = @"\s"
            };
            LogLine logLine = new LogLine() { ExpressionStructure = logFieldExpression, Separator = '|' };
            ILogLineBuilder logLineBuilder = new LogLineBuilder(logLine);

            foreach(string field in fields)
            {
                logLineBuilder.Append(field);
            }

            Assert.Equal(expectedLineContent, logLineBuilder.ToString());
        }

        [Fact]
        public void ShouldClearLogLine()
        {
            string[] fields = new string[] { "312", "200", "HIT", "GET / examplefile.txt HTTP / 1.1", "100.2", "POST / toys.txt HTTP / 1.1" };

            LogFieldExpression logFieldExpression = new LogFieldExpression()
            {
                Delimiter = '"',
                SeparatorPattern = @"\s"
            };
            LogLine logLine = new LogLine() { ExpressionStructure = logFieldExpression, Separator = '|' };
            ILogLineBuilder logLineBuilder = new LogLineBuilder(logLine);

            foreach (string field in fields)
            {
                logLineBuilder.Append(field);
            }
            logLineBuilder.Clear();
            Assert.Equal(String.Empty, logLineBuilder.ToString());
        }

    }
}
