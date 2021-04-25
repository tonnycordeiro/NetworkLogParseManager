﻿using NetworkLogParseManager.Models;
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
    public class FormatIn01LogFactory : AbstractLogFactory
    {
        public const string LOG_LINE_JSON_PATH = "./JSON/FormatIn01LogLine.json";

        public FormatIn01LogFactory()
        {
        }

        public LogLine CreateLogLine()
        {
            string jsonString = File.ReadAllText(LOG_LINE_JSON_PATH);
            return JsonConvert.DeserializeObject<LogLine>(jsonString);
        }

        public StreamFileReader CreateStreamFileReader(string path)
        {
            return new StreamUrlFileReader(path);
        }

        public StreamFileWriter CreateStreamFileWriter(string path)
        {
            throw new NotImplementedException();
        }
    }
}
