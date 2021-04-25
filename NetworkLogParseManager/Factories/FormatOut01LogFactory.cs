using NetworkLogParseManager.Models;
using NetworkLogParseManager.StreamFiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Factories
{
    public class FormatOut01LogFactory : AbstractLogFactory
    {
        public const string LOG_LINE_JSON_PATH = "./JSON/FormatOut01LogLine.json";

        public FormatOut01LogFactory()
        {
        }

        public LogLine CreateLogLine()
        {
            string jsonString = File.ReadAllText(LOG_LINE_JSON_PATH);
            return JsonConvert.DeserializeObject<LogLine>(jsonString);
        }

        public StreamFileReader CreateStreamFileReader(string path)
        {
            throw new NotImplementedException(); 
        }

        public StreamFileWriter CreateStreamFileWriter(string path)
        {
            return new FormatOut01LogFileWriter(path, CreateLogLine());
        }
    }
}
