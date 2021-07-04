using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace headshot_fl
{
    class FileLoggerProvider : ILoggerProvider
    {
        private readonly FileLoggerConfig _config;
        private readonly ConcurrentDictionary<string, FileLogger> _loggers = new ConcurrentDictionary<string, FileLogger>();

        public FileLoggerProvider(FileLoggerConfig con) => (_config) = (con);

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new FileLogger(name, _config));

        public void Dispose() => _loggers.Clear();
    }
}