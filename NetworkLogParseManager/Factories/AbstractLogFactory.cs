using NetworkLogParseManager.Models;
using NetworkLogParseManager.StreamFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.Factories
{
    public interface AbstractLogFactory
    {
        StreamFileReader CreateStreamFileReader(string path);
        StreamFileWriter CreateStreamFileWriter(string path);
        LogLine CreateLogLine();
    }
}
