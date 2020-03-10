using System;

namespace ORA.API.Loggers
{
    public interface ILogger
    {
        void Info(Exception cause) => this.Log(LogLevel.Info, cause);

        void Info(string message) => this.Log(LogLevel.Info, message);

        void Debug(Exception cause) => this.Log(LogLevel.Debug, cause);

        void Debug(string message) => this.Log(LogLevel.Debug, message);

        void Error(Exception cause) => this.Log(LogLevel.Error, cause);

        void Error(string message) => this.Log(LogLevel.Error, message);

        void Log(LogLevel level, Exception cause);

        void Log(LogLevel level, string message);

        bool ShouldPrint();

        void ShouldPrint(bool value);
    }
}
