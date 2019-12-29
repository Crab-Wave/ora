using System;

namespace ORA.API.Loggers
{
    public interface ILogger
    {
        void Info(Exception cause) => Log(LogLevel.Info, cause);

        void Info(string message) => Log(LogLevel.Info, message);

        void Debug(Exception cause) => Log(LogLevel.Debug, cause);

        void Debug(string message) => Log(LogLevel.Debug, message);

        void Error(Exception cause) => Log(LogLevel.Error, cause);

        void Error(string message) => Log(LogLevel.Error, message);

        void Log(LogLevel level, Exception cause);

        void Log(LogLevel level, string message);
    }
}
