using NetworkLogParseManager.Parsers.ElementParsers;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestLogParser
{
    public class ElementParsersTest
    {
        [Fact]
        public void ShouldCloneValue()
        {
            string expectedValue = "x";
            CloneElementLogParser cloneParser = new CloneElementLogParser();

            Assert.Equal(expectedValue, cloneParser.Parse(expectedValue));
        }

        [Fact]
        public void ShouldMapValue()
        {
            string sourceValue1 = "x";
            string targetValue1 = "y";
            string sourceValue2 = "12";
            string targetValue2 = "58";

            MapOrCloneElementLogParser cloneParser = new MapOrCloneElementLogParser()
            {
                MapElements = new Dictionary<string, string>() { { sourceValue1, targetValue1 }, { sourceValue2, targetValue2 } }
            };

            Assert.Equal(targetValue1, cloneParser.Parse(sourceValue1));
            Assert.Equal(targetValue2, cloneParser.Parse(sourceValue2));
        }

        [Fact]
        public void ShouldCloneValueWhenCannotMap()
        {
            string sourceValue1 = "x";
            string targetValue1 = "y";
            string sourceValue2 = "12";
            string targetValue2 = "58";
            string sourceValueNotMapped = "1234";

            MapOrCloneElementLogParser cloneParser = new MapOrCloneElementLogParser()
            {
                MapElements = new Dictionary<string, string>() { { sourceValue1, targetValue1 }, { sourceValue2, targetValue2 } }
            };

            Assert.Equal(sourceValueNotMapped, cloneParser.Parse(sourceValueNotMapped));
        }

        [Fact]
        public void ShouldNotMapValueWhenArgumentIsNull()
        {
            string sourceValue1 = "x";
            string targetValue1 = "y";
            string sourceValue2 = "12";
            string targetValue2 = "58";

            MapOrCloneElementLogParser cloneParser = new MapOrCloneElementLogParser()
            {
                MapElements = new Dictionary<string, string>() { { sourceValue1, targetValue1 }, { sourceValue2, targetValue2 } }
            };

            Assert.Throws<ArgumentNullException>(() => cloneParser.Parse(null));
        }

        [Fact]
        public void ShouldPrintValueAndNotParse()
        {
            string sourceValue = "x";
            string valueToPrint = "y";

            PrintElementLogParser printParser = new PrintElementLogParser()
            {
                Value = valueToPrint
            };

            Assert.Equal(valueToPrint, printParser.Parse());
            Assert.Equal(valueToPrint, printParser.Parse(sourceValue));
            Assert.Equal(valueToPrint, printParser.Parse(null));
        }

        [Fact]
        public void ShouldRoundValue()
        {
            string sourceValue1 = "10.4999";
            string targetValue1 = "10";

            RoundElementLogParser roundParser = new RoundElementLogParser()
            {
                Digits = 0
            };

            Assert.Equal(targetValue1, roundParser.Parse(sourceValue1));
        }

        [Fact]
        public void ShouldNotRoundValueWhenArgumentIsNotNumeric()
        {
            string sourceValue1 = "aaa";

            RoundElementLogParser roundParser = new RoundElementLogParser()
            {
                Digits = 0
            };

            Assert.Throws<FormatException>(() => roundParser.Parse(sourceValue1));
        }


        [Fact]
        public void ShouldSplitValueAndGetValueByIndex()
        {
            string sourceValue1 = "ab cd ef";
            string targetValue1 = "ef";

            SplitElementLogParser splitParser = new SplitElementLogParser()
            {
                Index = 2,
                Separator = " "
            };

            Assert.Equal(targetValue1, splitParser.Parse(sourceValue1));
        }

        [Fact]
        public void ShouldNotSplitValueWhenIndexIsOutOfRange()
        {
            string sourceValue1 = "ab cd ef";

            SplitElementLogParser splitParser = new SplitElementLogParser()
            {
                Index = 4,
                Separator = " "
            };

            Assert.Throws<IndexOutOfRangeException>(() => splitParser.Parse(sourceValue1));
        }


        

    }
}