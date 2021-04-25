using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.StreamFiles
{
    public abstract class StreamFileReader : IDisposable, IStreamFileReader
    {
        private string _filePath;
        private Stream _stream;
        private StreamReader _streamReader;

        public StreamFileReader(string filePath)
        {
            _filePath = filePath;
            _stream = GetStream(_filePath);
            _streamReader = new StreamReader(_stream);
        }

        protected virtual Stream GetStream(string filePath)
        {
            return new FileStream(_filePath, FileMode.Open, FileAccess.Read);
        }

        public virtual string ReadLine()
        {
            try
            {
                return _streamReader.ReadLine();
            }
            catch
            {
                throw;
            }
        }

        protected abstract void DisposeMore();

        public void Dispose()
        {
            _streamReader.Dispose();
            _stream.Dispose();
            DisposeMore();
        }
    }
}
