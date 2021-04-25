using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Parsers.ElementParsers
{
    public interface IElementLogParser
    {
        public string Parse(string value); 
    }
}
