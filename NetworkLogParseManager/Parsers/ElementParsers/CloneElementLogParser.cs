using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Parsers.ElementParsers
{
    public class CloneElementLogParser : IElementLogParser
    {
        public string Parse(string value)
        {
            return value;
        }
    }
}
