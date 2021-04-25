using NetworkLogParseManager.StreamFiles;
using NetworkLogParseManager.Collections;
using NetworkLogParseManager.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace NetworkLogParseManager.Managers
{
    public class LogParsingManager
    {
        private ILimitedConcurrentQueue<string> _toParseLinesQueue;
        private ILimitedConcurrentQueue<string> _parsedLinesQueue;
        private bool _isReadingCompleted;
        private bool _isParsingCompleted;

        private LogLineParser _logLineParser;
        private IStreamFileReader _streamFileReader;
        private IStreamFileWriter _streamFileWriter;

        public LogParsingManager(IStreamFileReader streamFileReader, IStreamFileWriter streamFileWriter,
                               LogLineParser logLineParser,
                               int thresholdQueueToParse,
                               int thresholdQueueToWrite)
        {
            _toParseLinesQueue = new LimitedConcurrentQueue<string>(thresholdQueueToParse); ;
            _parsedLinesQueue = new LimitedConcurrentQueue<string>(thresholdQueueToWrite);

            _streamFileReader = streamFileReader;
            _streamFileWriter = streamFileWriter;

            _logLineParser = logLineParser;

            _isReadingCompleted = false;
            _isParsingCompleted = false;
        }
        public bool IsReadingCompleted { get => _isReadingCompleted; set => _isReadingCompleted = value; }
        public bool IsParsingCompleted { get => _isParsingCompleted; set => _isParsingCompleted = value; }
        public LogLineParser LogLineParser { get => _logLineParser; set => _logLineParser = value; }

        

        public void Parse()
        {
            try
            {
                Thread fileReaderThread = new Thread(delegate ()
                {
                    while (!_isReadingCompleted)
                    {
                        while (!_toParseLinesQueue.IsFull())
                        {
                            string line = _streamFileReader.ReadLine();
                            if (line != null)
                            {
                                _toParseLinesQueue.Enqueue(line);
                            }
                            else
                            {
                                _isReadingCompleted = true;
                                break;
                            }
                        }
                    }
                    _streamFileReader.Dispose();
                });

                Thread parserThread = new Thread(delegate ()
                {
                    string line;
                    while (!_isReadingCompleted || !_toParseLinesQueue.IsEmpty())
                    {
                        while (!_parsedLinesQueue.IsFull() && _toParseLinesQueue.TryDeque(out line))
                        {
                            _parsedLinesQueue.Enqueue(_logLineParser.Parse(line));
                        }
                    }
                    _isParsingCompleted = true;

                });

                Thread fileWriterThread = new Thread(delegate ()
                {
                    string line;
                    _streamFileWriter.WriteHeader();
                    while (!_isParsingCompleted || !_parsedLinesQueue.IsEmpty())
                    {
                        while (_parsedLinesQueue.TryDeque(out line))
                        {
                            _streamFileWriter.WriteLine(line);
                        }
                    }
                    _streamFileWriter.Dispose();
                });

                fileReaderThread.Start();
                parserThread.Start();
                fileWriterThread.Start();
                fileWriterThread.Join();
            }
            catch
            {
                throw;
            }
        }

    }
}
