using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Parsers.ElementParsers
{
    public class RoundElementLogParser : IElementLogParser
    {
        private int _digits;

        public int Digits { get => _digits; set => _digits = value; }

        public string Parse(string value)
        {
            try 
            {
                return Math.Round(Double.Parse(value,CultureInfo.InvariantCulture), _digits).ToString();
            }
            catch
            {
                throw;
            }
        }

    }
}
