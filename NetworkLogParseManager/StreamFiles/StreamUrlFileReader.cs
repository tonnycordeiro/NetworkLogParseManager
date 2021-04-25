using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.StreamFiles
{
    public class StreamUrlFileReader : StreamFileReader, IDisposable
    {
        private WebResponse _webResponse;

        public StreamUrlFileReader(string filePath) : base(filePath)
        {
        }

        protected override Stream GetStream(string filePath)
        {
            try
            {
                _webResponse = WebRequest.Create(filePath).GetResponse();
                return _webResponse.GetResponseStream();
            }
            catch
            {
                throw;
            }
        }
        protected override void DisposeMore()
        {
            _webResponse.Dispose();
        }
    }
}
