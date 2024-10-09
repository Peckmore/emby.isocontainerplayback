using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Services;

namespace IsoContainerPlayback.Formats.Dvd
{
    /// <summary>
    /// Represents the service that handles calls to the defined API endpoint.
    /// </summary>
    public class DvdIsoContainerService : IService
    {
        #region Fields

        private readonly ILogger _logger;

        #endregion

        #region Construction

        /// <summary>Initializes a new instance of the <see cref="DvdIsoContainerService" /> service.</summary>
        /// <param name="logManager">The log manager.</param>
        public DvdIsoContainerService(ILogManager logManager)
        {
            _logger = logManager.GetLogger(nameof(DvdIsoContainerService));

            _logger.Info($"{nameof(DvdIsoContainerService)} is started.", 0);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a <see cref="DvdIsoStream"/> for the requested DVD ISO and title.
        /// </summary>
        /// <param name="request">The DVD ISO and title to stream.</param>
        /// <returns>A <see cref="DvdIsoStream"/> for the requested DVD ISO and title.</returns>
        public object Get(GetDvdIsoStream request)
        {
            _logger.Info($"DVD ISO `{request.IsoPath}` requested...", 0);
            
            return new DvdIsoStream(request.IsoPath, request.Title);
        }

        #endregion
    }
}