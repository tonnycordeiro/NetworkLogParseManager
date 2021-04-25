using NetworkLogParseManager.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLogParseManager.StreamFiles
{
    public abstract class StreamFileWriter : IDisposable, IStreamFileWriter
    {
        string _filePath;
        FileStream _fileStream;
        StreamWriter _streamWriter;

        public string FilePath { get => _filePath; set => _filePath = value; }

        public StreamFileWriter(string filePath)
        {
            _filePath = filePath;

            try
            {
                FileManager fileManager = new FileManager();
                fileManager.DeleteFile(_filePath);
                fileManager.CreateDirectory(_filePath);

                _fileStream = new FileStream(_filePath, FileMode.CreateNew, FileAccess.Write);
                _streamWriter = new StreamWriter(_fileStream);
            }
            catch
            {
                throw;
            }
        }

        public virtual void WriteHeader() { }

        public virtual void WriteLine(string content)
        {
            _streamWriter.WriteLine(content);
        }

        public void Dispose()
        {
            _streamWriter.Dispose();
            _fileStream.Dispose();
        }
    }
}
