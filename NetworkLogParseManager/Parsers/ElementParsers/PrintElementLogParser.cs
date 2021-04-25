using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Parsers.ElementParsers
{
    public class PrintElementLogParser : IElementLogParser
    {
        private string _value;

        public string Value { get => _value; set => _value = value; }

        public string Parse(string ignoredValue = null)
        {
            return _value;
        }
    }
}
