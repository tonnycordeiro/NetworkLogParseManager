namespace NetworkLogParseManager.Collections
{
    public interface ILimitedConcurrentQueue<T>
    {
        int Threshold { get; set; }
        bool TryDeque(out T element);
        void Enqueue(T element);
        bool IsEmpty();
        bool IsFull();
    }
}