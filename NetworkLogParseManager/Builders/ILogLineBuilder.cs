using NetworkLogParseManager.Models;

namespace NetworkLogParseManager.Builders
{
    public interface ILogLineBuilder
    {
        public LogLine LogLine { get; set; }
        void Append(string nextValue);
        void Clear();
        string[] Split(string lineContent);
        string ToString();
    }
}