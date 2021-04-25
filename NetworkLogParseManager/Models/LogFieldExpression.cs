using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Models
{
    /// <summary>
    /// "Field expression" is a long field inside a line delimited by characters, usually double quotes
    /// </summary>
    public class LogFieldExpression
    {
        private char _delimiter;
        private string _separatorPattern;

        public LogFieldExpression()
        {
            _delimiter = '\0';
            _separatorPattern = null;
        }

        public char Delimiter { get => _delimiter; set => _delimiter = value; }
        public string SeparatorPattern { get => _separatorPattern; set => _separatorPattern = value; }

        public bool IsExpression(string value)
        {
            try
            {
                Regex separatorRegex = new Regex(SeparatorPattern);
                return separatorRegex.IsMatch(value);
            }
            catch
            {
                throw;
            }
        }

        public string Build(string value)
        {
            return String.Format("{0}{1}{0}", Delimiter, value);
        }

    }
}
