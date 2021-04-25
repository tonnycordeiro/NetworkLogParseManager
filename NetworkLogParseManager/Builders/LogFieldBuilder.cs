using NetworkLogParseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Builders
{
    public class LogFieldBuilder : ILogFieldBuilder
    {
        private LogFieldExpression _fieldExpressionStructure;

        public LogFieldBuilder(LogFieldExpression fieldExpressionStructure)
        {
            _fieldExpressionStructure = fieldExpressionStructure;
        }

        public string Build(string value)
        {
            try
            {
                if (_fieldExpressionStructure.IsExpression(value))
                    return _fieldExpressionStructure.Build(value);
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
