using NetworkLogParseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.StreamFiles
{
    public class FormatOut01LogFileWriter : StreamFileWriter
    {
        private const string VERSION_HEADER_VALUE = "1.0";
        private const string FIELDS_ID_SEPARATOR = " ";

        LogLine _logLine;

        public FormatOut01LogFileWriter(string fileName, LogLine logLine) : base(fileName)
        {
            _logLine = logLine;
        }

        public override void WriteHeader()
        {
            try
            {
                this.WriteLine(String.Format("#Version: {0}", VERSION_HEADER_VALUE));
                this.WriteLine(String.Format("#Date: {0:dd/MM/yyyy HH:mm:ss}", DateTime.Now));
                this.WriteLine(
                               String.Format("#Fields: {0}",
                                  String.Join(
                                                FIELDS_ID_SEPARATOR,
                                                _logLine.LogFieldArray.Select(f => f.Id)
                                              )
                                 )
                              );
            }
            catch
            {
                throw;
            }
        }
    }
}
