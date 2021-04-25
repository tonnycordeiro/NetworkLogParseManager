namespace NetworkLogParseManager.StreamFiles
{
    public interface IStreamFileReader
    {
        void Dispose();
        string ReadLine();
    }
}