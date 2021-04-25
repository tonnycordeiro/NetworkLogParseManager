using NetworkLogParseManager.Collections;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestLogParser
{
    public class CollectionsTest
    {
        [Fact]
        public void ShouldNotEnqueueInFullState()
        {
            int threshold = 10;
            ILimitedConcurrentQueue<string> queue = new LimitedConcurrentQueue<string>(
                new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8"}
                , threshold);
            queue.Enqueue("9");
            queue.Enqueue("10");

            Assert.Throws<InvalidOperationException>(() => queue.Enqueue("11"));
        }

        [Fact]
        public void ShouldEnqueueUntilFullState()
        {
            int threshold = 10;
            ILimitedConcurrentQueue<int> queue = new LimitedConcurrentQueue<int>(threshold);
            
            while(!queue.IsFull())
            {
                queue.Enqueue(threshold--);
            }

            Assert.True(queue.IsFull());
        }

        [Fact]
        public void ShouldDequeueUntilEmptyState()
        {
            string element;
            int threshold = 10;
            List<string> strList = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8" };

            ILimitedConcurrentQueue<string> queue = new LimitedConcurrentQueue<string>(strList, threshold);

            while (!queue.IsEmpty())
            {
                if (queue.TryDeque(out element))
                    strList.Remove(element);
            }

            Assert.Empty(strList);
        }

        [Fact]
        public void ShouldChangeThresholdToMore()
        {
            int threshold = 10;
            ILimitedConcurrentQueue<string> queue = new LimitedConcurrentQueue<string>(
                new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8" }
                , threshold);
            queue.Enqueue("9");
            queue.Enqueue("10");

            queue.Threshold = 12;
            queue.Enqueue("11");
            queue.Enqueue("12");

            Assert.True(queue.IsFull());
        }

        [Fact]
        public void ShouldChangeThresholdToLess()
        {
            int threshold = 10;
            ILimitedConcurrentQueue<string> queue = new LimitedConcurrentQueue<string>(
                new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8" }
                , threshold);

            queue.Threshold = 7;

            Assert.True(queue.IsFull());
        }


    }
}