using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace headshot_fl
{
    class FileLogger : ILogger
    {
        private readonly string referenceName;
        private readonly FileLoggerConfig _config;
        public FileLogger(
            string name,
            FileLoggerConfig con) => (referenceName, _config) = (name, con);
        public IDisposable BeginScope<TState>(TState state) => default;

        // public bool IsEnabled(LogLevel logLevel) => logLevel == _config.Level;
        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            if (_config.EventId == 0 || _config.EventId == eventId.Id)
            {
                File.AppendAllText(_config.FileName, DateTime.Now.ToString()+
                 $" {referenceName} - {formatter(state, exception)}"+Environment.NewLine);
            }
        }
    }
}