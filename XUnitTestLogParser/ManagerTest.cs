using NetworkLogParseManager.Managers;
using System;
using Xunit;

namespace XUnitTestLogParser
{
    public class ManagerTest
    {
        [Fact]
        public void ShouldGetDirectoryPathFromFullPath()
        {
            FileManager fileManager = new FileManager();
            string fullPath = @"c:\folder\file1.txt";
            string expected = @"c:\folder";
            Assert.Equal(expected, fileManager.GetDirectoryPath(fullPath));
        }

        [Fact]
        public void ShouldGetDirectoryPathFromRelativePath()
        {
            FileManager fileManager = new FileManager();
            string fullPath = @".\folder\file1.txt";
            string expected = @".\folder";
            Assert.Equal(expected, fileManager.GetDirectoryPath(fullPath));
        }
    }
}