namespace NetworkLogParseManager.StreamFiles
{
    public interface IStreamFileWriter
    {
        string FilePath { get; set; }

        void Dispose();
        void WriteHeader();
        void WriteLine(string content);
    }
}