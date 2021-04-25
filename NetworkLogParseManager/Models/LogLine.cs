using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Models
{
    public class LogLine
    {
        private LogField[] _logFieldArray;
        private char _separator;
        private LogFieldExpression _expressionStructure;

        public LogLine()
        {
            _logFieldArray = null;
            _separator = '\0';
            _expressionStructure = null;
        }

        public char Separator { get => _separator; set => _separator = value; }
        public LogField[] LogFieldArray { get => _logFieldArray; set => _logFieldArray = value; }
        public char ExpressionDelimiter { get => _expressionStructure.Delimiter; }
        public string ExpressionSeparatorPattern { get => _expressionStructure.SeparatorPattern; }
        public LogFieldExpression ExpressionStructure { get => _expressionStructure; set => _expressionStructure = value; }

    }
}
