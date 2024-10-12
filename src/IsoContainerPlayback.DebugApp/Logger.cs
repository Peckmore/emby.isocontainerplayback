using MediaBrowser.Model.Logging;
using System.Text;

namespace IsoContainerPlayback.DebugApp
{
    /// <summary>
    /// Basic <see cref="ILogger" /> implementation.
    /// </summary>
    internal class Logger : ILogger
    {
        #region Methods

        #region Private

        private void Log(string message)
        {
            Console.WriteLine(message);
        }

        #endregion

        #region Public

        public void Debug(string message, params object[] paramList)
        {
            Log(message);
        }
        public void Debug(ReadOnlyMemory<char> message)
        { }
        public void Error(string message, params object[] paramList)
        {
            Log(message);
        }
        public void Error(ReadOnlyMemory<char> message)
        { }
        public void ErrorException(string message, Exception exception, params object[] paramList)
        {
            Log(message);
        }
        public void Fatal(string message, params object[] paramList)
        {
            Log(message);
        }
        public void FatalException(string message, Exception exception, params object[] paramList)
        {
            Log(message);
        }
        public void Info(string message, params object[] paramList)
        {
            Log(message);
        }
        public void Info(ReadOnlyMemory<char> message)
        { }
        public void Log(LogSeverity severity, string message, params object[] paramList)
        { }
        public void Log(LogSeverity severity, ReadOnlyMemory<char> message)
        { }
        public void LogMultiline(string message, LogSeverity severity, StringBuilder additionalContent)
        {
            Log(message);
        }
        public void Warn(string message, params object[] paramList)
        {
            Log(message);
        }
        public void Warn(ReadOnlyMemory<char> message)
        { }

        #endregion

        #endregion
    }
}