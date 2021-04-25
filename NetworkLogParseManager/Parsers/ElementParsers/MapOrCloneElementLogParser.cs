using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Parsers.ElementParsers
{
    public class MapOrCloneElementLogParser : IElementLogParser
    {
        private Dictionary<string, string> _mapElements;

        public Dictionary<string, string> MapElements { get => _mapElements; set => _mapElements = value; }

        public string Parse(string value)
        {
            try
            {
                if (_mapElements.ContainsKey(value))
                    return _mapElements[value];
                else
                    return value;
            }
            catch
            {
                throw;
            }
        }
    }
}
