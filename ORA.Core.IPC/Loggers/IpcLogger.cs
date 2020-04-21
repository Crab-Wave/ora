using System;
using System.IO;
using System.Text.RegularExpressions;
using JKang.IpcServiceFramework;
using ORA.API.Loggers;

namespace ORA.Core.IPC.Loggers
{
    public class IpcLogger : ILogger
    {
        private IpcServiceClient<ILogger> _client;

        public IpcLogger(IpcServiceClient<ILogger> client)
        {
            this._client = client;
        }

        public void Log(LogLevel level, string message) =>
            this._client.InvokeAsync(logger => logger.Log(level, message)).Wait();

        public bool ShouldPrint() => this._client.InvokeAsync(logger => logger.ShouldPrint()).Result;

        public void ShouldPrint(bool value) => this._client.InvokeAsync(logger => logger.ShouldPrint(value)).Wait();

        public void Log(LogLevel level, Exception cause) => this.Log(level, cause.Message);
    }
}
