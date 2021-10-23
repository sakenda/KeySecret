using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace KeySecret.DataAccess.Library
{
    public interface ILoggerContext
    {
        ILogger Logger { get; }
        LogLevel Level { get; set; }
        Exception Exception { get; set; }
        string Caller { get; }
        string Message { get; set; }
        public Stopwatch Watch { get; set; }
    }
}