using MediaBrowser.Model.Logging;

namespace IsoContainerPlayback.DebugApp
{
    /// <summary>
    /// Basic <see cref="ILogManager" /> implementation.
    /// </summary>
    internal class LogManager : ILogManager
    {
        #region Events

        public event EventHandler? LoggerLoaded;

        #endregion

        #region Properties

        public LogSeverity LogSeverity { get; set; }
        public string ExceptionMessagePrefix { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #endregion

        #region Methods

        public void AddConsoleOutput()
        { }
        public void Flush()
        { }
        public ILogger GetLogger(string name)
        {
            return new Logger();
        }
        public Task ReloadLogger(LogSeverity severity, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public void RemoveConsoleOutput()
        { }

        #endregion
    }
}