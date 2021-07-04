using System;
using Microsoft.Extensions.Logging;

namespace headshot_fl
{
    class FileLoggerConfig
    {
        public int EventId { get; set; }
        public LogLevel Level { get; set; }
        public string FileName { get; set; } = "Application.log";
    }
}