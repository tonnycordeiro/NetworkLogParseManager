using NetworkLogParseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Builders
{
    public class LogLineBuilder : ILogLineBuilder
    {
        private List<string> _fieldList;
        private ILogLine _logLine;
        private ILogFieldBuilder _logFieldBuilder;

        public LogLineBuilder(ILogLine logLine)
        {
            _fieldList = new List<string>();
            _logLine = logLine;

            if (logLine != null)
                _logFieldBuilder = new LogFieldBuilder(logLine.ExpressionStructure);
            else
                _logFieldBuilder = null;
        }

        public ILogLine LogLine { get => _logLine; set => _logLine = value; }

        #region PRIVATE METHODS
        private static string[] RecoverExpressions(Dictionary<string, string> expressionsWithSeparators, string[] rowValues)
        {
            return rowValues.Select(v =>
            {
                if (expressionsWithSeparators.ContainsKey(v))
                    return expressionsWithSeparators[v];
                return v;
            }).ToArray();
        }

        private string IsolateExpressions(string lineContent, ref Dictionary<string, string> expressionsWithSeparators)
        {
            string lineContentCopy = lineContent;
            string lineContentAux = String.Format("{0}{1}{0}", _logLine.Separator, lineContent);
            Regex regexExpressionWithSeparators;

            try
            {
                regexExpressionWithSeparators = new Regex(
                                            String.Format("[{0}]([{1}]([^{1}]+)[{1}])[{0}]",
                                                _logLine.Separator, _logLine.ExpressionDelimiter)
                                            );

                foreach (Match expressionMatch in regexExpressionWithSeparators.Matches(lineContentAux))
                {
                    Regex regexExpressionSeparator = new Regex(_logLine.ExpressionSeparatorPattern);
                    string expressionWithDelimiters = expressionMatch.Groups[1].ToString();
                    string expressionValue = expressionMatch.Groups[2].ToString();
                    string key = regexExpressionSeparator.Replace(expressionWithDelimiters, String.Empty);

                    lineContentCopy = lineContentCopy.Replace(expressionWithDelimiters, key);
                    if (!expressionsWithSeparators.ContainsKey(key))
                    {
                        expressionsWithSeparators.Add(key, expressionValue);
                    }
                }
            }
            catch
            {
                throw;
            }

            return lineContentCopy;
        }

        #endregion PRIVATE METHODS

        #region PUBLIC METHODS
        public string[] Split(string lineContent)
        {
            string rowCopy;
            Dictionary<string, string> expressionsWithSeparators = new Dictionary<string, string>();
            string[] rowValues;

            try
            {
                rowCopy = IsolateExpressions(lineContent, ref expressionsWithSeparators);

                rowValues = rowCopy.Split(new char[] { _logLine.Separator }, StringSplitOptions.RemoveEmptyEntries);

                return RecoverExpressions(expressionsWithSeparators, rowValues);
            }
            catch
            {
                throw;
            }
        }

        public void Append(string fieldValue)
        {
            try
            {
                _fieldList.Add(_logFieldBuilder.Build(fieldValue));
            }
            catch
            {
                throw;
            }
        }

        public void Clear()
        {
            _fieldList.Clear();
        }

        public override string ToString()
        {
            return String.Join(_logLine.Separator, _fieldList);
        }
        #endregion PUBLIC METHODS

    }
}
