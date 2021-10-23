using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace KeySecret.DataAccess.Library
{
    public class LoggerContext : ILoggerContext
    {
        public ILogger Logger => _logger;
        private ILogger _logger;

        public string Caller { get; private set; }
        public LogLevel Level { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public Stopwatch Watch { get; set; }

        public LoggerContext(ILogger logger, [CallerMemberName] string methodName = "")
        {
            _logger = logger;
            Caller = methodName;
            Watch = new Stopwatch();
        }
    }

    public static class LoggerContextExtensions
    {
        public static void StartPerformanceLog(this LoggerContext loggerCtx, string info = "")
        {
            if (loggerCtx.Watch.IsRunning)
            {
                loggerCtx.Logger.LogWarning("Stopwatch is already running. Watch will be stopped an restarted.");
                loggerCtx.Watch.Stop();
            }

            loggerCtx.Watch.Start();
            loggerCtx.Logger.Log(LogLevel.Information, $"[{loggerCtx.Caller}({info})] Start Performance Log.");
        }
        public static void StopPerformanceLog(this LoggerContext loggerCtx)
        {
            if (!loggerCtx.Watch.IsRunning)
            {
                loggerCtx.Logger.LogWarning("Something wen't wrong. Stopwatch is not running.");
                return;
            }

            loggerCtx.Watch.Stop();
            loggerCtx.Logger.Log(LogLevel.Information, $"[{loggerCtx.Caller}] Stop Performance Log. Elapsed time: {loggerCtx.Watch.ElapsedMilliseconds}ms");
        }
        public static void RestartPerformanceLog(this LoggerContext loggerCtx, string info = "")
        {
            loggerCtx.StopPerformanceLog();
            loggerCtx.StartPerformanceLog(info);
        }

        public static void WriteException(this LoggerContext loggerCtx, Exception ex = null)
        {
            if (ex == null)
            {
                if (loggerCtx.Exception == null)
                {
                    loggerCtx.Logger.LogError("No exception found.");
                    return;
                }

                ex = loggerCtx.Exception;
            }

            loggerCtx.Logger.Log(loggerCtx.Level, FormatMessage(loggerCtx));
        }
        public static void WriteLog(this LoggerContext loggerCtx)
            => loggerCtx.Logger.Log(loggerCtx.Level, FormatMessage(loggerCtx));

        private static string FormatMessage(LoggerContext loggerCtx)
        {
            if (loggerCtx.Level == LogLevel.Error)
            {
                return $"[{loggerCtx.Caller}] [{loggerCtx.Level}]: {loggerCtx.Message}\r\n" +
                       $"Description: {loggerCtx.Exception.Message}\r\n" +
                       $"=> {loggerCtx.Exception.StackTrace}";
            }

            return $"[{loggerCtx.Caller}] [{loggerCtx.Level}]: {loggerCtx.Message}";
        }
    }
}