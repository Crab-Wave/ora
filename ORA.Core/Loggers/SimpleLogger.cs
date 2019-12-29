using System;
using System.IO;
using System.Text.RegularExpressions;
using ORA.API.Loggers;

namespace ORA.Core.Loggers
{
    public class SimpleLogger : ILogger
    {
        private readonly string _dateFormat;
        private readonly string _fileFormat;
        private readonly string _outFormat;
        private readonly StreamWriter _stream;

        public SimpleLogger(string name)
        {
            _dateFormat = "dd/MM/yyyy HH:mm:ss";
            var newName = Regex.Replace(name, "([^_A-Z])([A-Z])", "$1-$2").ToLower();
            try
            {
                Directory.CreateDirectory("logs");
                _stream = File.CreateText("logs" + "/" + newName + "-" + DateTime.Now.ToFileTime() + ".log");
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
                throw;
            }

            _fileFormat = "[{0}] [{1}] [" + name + "] - {2}\n";
            _outFormat = "[" + name + "/{0}] - {1}";
        }

        public void Log(LogLevel level, string message)
        {
            var output = string.Format(_outFormat, level.ToString().ToUpper(), message);
            Console.WriteLine(output);
            try
            {
                _stream.Write(_fileFormat, DateTime.Now.ToString(_dateFormat), level.ToString().ToUpper(), message);
                _stream.Flush();
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Log(LogLevel level, Exception cause)
        {
            Log(level, cause.Message);
        }
    }
}
