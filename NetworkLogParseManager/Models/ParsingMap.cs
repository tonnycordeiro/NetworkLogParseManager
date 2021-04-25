using NetworkLogParseManager.Converters;
using NetworkLogParseManager.Models;
using NetworkLogParseManager.Parsers.ElementParsers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Models
{
    public class ParsingMap
    {
        private LogField _sourceField;
        private LogField _targetField;
        private IElementLogParser _parser;

        public ParsingMap()
        {
            _sourceField = null;
            _targetField = null;
            _parser = null;
        }

        public LogField SourceField { get => _sourceField; set => _sourceField = value; }
        public LogField TargetField { get => _targetField; set => _targetField = value; }
        public IElementLogParser Parser { get => _parser; set => _parser = value; }
    }
}
