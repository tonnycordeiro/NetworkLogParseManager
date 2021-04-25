using NetworkLogParseManager.Providers;
using System;
using Xunit;

namespace XUnitTestLogParser
{
    public class ConfigTest
    {
        [Fact]
        public void ShouldGetNumericThresholdForQueueToParse()
        {
            ConfigProvider configProvider = new ConfigProvider();
            Assert.IsType<int>(configProvider.ThresholdQueueToParse);
        }

        [Fact]
        public void ShouldGetNumericThresholdForQueueToWrite()
        {
            ConfigProvider configProvider = new ConfigProvider();
            Assert.IsType<int>(configProvider.ThresholdQueueToWrite);
        }
    }
}