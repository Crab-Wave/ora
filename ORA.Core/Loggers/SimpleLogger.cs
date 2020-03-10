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
        private bool _shouldPrint;

        public SimpleLogger(string name)
        {
            this._dateFormat = "dd/MM/yyyy HH:mm:ss";
            string newName = Regex.Replace(name, "([^_A-Z])([A-Z])", "$1-$2").ToLower();
            Directory.CreateDirectory("logs");
            this._stream = File.CreateText("logs" + "/" + newName + "-" + DateTime.Now.ToFileTime() + ".log");

            this._fileFormat = "[{0}] [{1}] [" + name + "] - {2}\n";
            this._outFormat = "[" + name + "/{0}] - {1}";
            this._shouldPrint = false;
        }

        public void Log(LogLevel level, string message)
        {
            string output = String.Format(this._outFormat, level.ToString().ToUpper(), message);
            if (this._shouldPrint)
                Console.WriteLine(output);
            this._stream.Write(this._fileFormat, DateTime.Now.ToString(this._dateFormat),
                level.ToString().ToUpper(), message);
            this._stream.Flush();
        }

        public bool ShouldPrint() => this._shouldPrint;

        public void ShouldPrint(bool value) => this._shouldPrint = value;

        public void Log(LogLevel level, Exception cause) => this.Log(level, cause.Message);
    }
}
