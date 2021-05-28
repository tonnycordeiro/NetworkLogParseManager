using NetworkLogParseManager.Models;

namespace NetworkLogParseManager.Builders
{
    public interface ILogLineBuilder
    {
        public ILogLine LogLine { get; set; }
        void Append(string nextValue);
        void Clear();
        string[] Split(string lineContent);
        string ToString();
    }
}