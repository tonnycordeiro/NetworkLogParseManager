using NetworkLogParseManager.Builders;
using NetworkLogParseManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Parsers
{
    public class LogLineParser
    {
        ILogLineBuilder _sourceLogLineBuilder;
        ILogLineBuilder _targetLogLineBuilder;
        
        List<ParsingMap> _parsingMapList;
        LogLine _sourceLogLine;
        LogLine _targetLogLine;

        [JsonConstructor]
        public LogLineParser(List<ParsingMap> parsingMapList)
        {
            _sourceLogLine = null;
            _targetLogLine = null;
            _sourceLogLineBuilder = null;
            _targetLogLineBuilder = null;
            _parsingMapList = parsingMapList;
        }


        public LogLineParser(LogLine sourceLogLine, LogLine targetLogLine, List<ParsingMap> parsingMapList)
        {
            SourceLogLine = sourceLogLine;
            TargetLogLine = targetLogLine;
            ParsingMapList = parsingMapList;
        }

        public string Parse(string lineContent)
        {
            try
            {
                string[] sourceElements = _sourceLogLineBuilder.Split(lineContent);

                _targetLogLineBuilder.Clear();
                foreach (ParsingMap map in _parsingMapList)
                {
                    _targetLogLineBuilder.Append(map.Parser.Parse(map.SourceField != null ? sourceElements[map.SourceField.Index] : null));
                }
                return _targetLogLineBuilder.ToString();
            }
            catch 
            { 
                throw; 
            }

        }

        public void PopulateLogLineFieldsFromParsingMapList()
        {
            _sourceLogLineBuilder.LogLine.LogFieldArray = _parsingMapList.Select(m => m.SourceField).ToArray();
            _targetLogLineBuilder.LogLine.LogFieldArray = _parsingMapList.Select(m => m.TargetField).ToArray();
        }

        protected ILogLineBuilder SourceLogLineBuilder { get => _sourceLogLineBuilder; }
        public ILogLineBuilder TargetLogLineBuilder { get => _targetLogLineBuilder; }
        public List<ParsingMap> ParsingMapList { get => _parsingMapList; set => _parsingMapList = value; }
        public LogLine SourceLogLine { get => _sourceLogLine; 
                                       set { _sourceLogLine = value; _sourceLogLineBuilder = new LogLineBuilder(value); } 
                                     }
        public LogLine TargetLogLine { get => _targetLogLine; 
                                       set { _targetLogLine = value; _targetLogLineBuilder = new LogLineBuilder(value); }
                                     }

    }
}
