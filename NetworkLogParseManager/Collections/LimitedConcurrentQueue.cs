using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Collections
{
    /// <summary>
    /// A "Limited Concurrent Queue" is a concurrent queue limited by a parameter.
    /// In the other words, it cannot have more elements then what is defined in its threshold.
    /// It should be used to avoid memory overload.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LimitedConcurrentQueue<T> : ILimitedConcurrentQueue<T>
    {
        private int _threshold;
        private ConcurrentQueue<T> _queue;

        public LimitedConcurrentQueue(int threshold)
        {
            _threshold = threshold;
            _queue = new ConcurrentQueue<T>();
        }

        public LimitedConcurrentQueue(IEnumerable<T> collection, int threshold)
        {
            _threshold = threshold;
            _queue = new ConcurrentQueue<T>(collection);
        }

        public int Threshold { get => _threshold; set => _threshold = value; }

        public bool IsFull()
        {
            return _queue.Count >= _threshold;
        }

        public bool IsEmpty()
        {
            return _queue.Count == 0;
        }

        public void Enqueue(T element)
        {
            if (!IsFull())
            {
                _queue.Enqueue(element);
            }
            else
            {
                throw new InvalidOperationException("You cannot enqueue elements when the threshold has already been reached before");
            }
        }

        public bool TryDeque(out T element)
        {
            return _queue.TryDequeue(out element);
        }


    }
}
