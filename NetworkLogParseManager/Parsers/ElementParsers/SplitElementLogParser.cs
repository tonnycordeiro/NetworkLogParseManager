using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Parsers.ElementParsers
{
    public class SplitElementLogParser : IElementLogParser
    {
        private string _separator;
        private int _index;

        public string Separator { get => _separator; set => _separator = value; }
        public int Index { get => _index; set => _index = value; }

        public string Parse(string value)
        {
            try
            {
                return value.Split(new string[] { _separator }, StringSplitOptions.RemoveEmptyEntries)[_index];
            }
            catch 
            { 
                throw; 
            }
        }
    }
}
