using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Services;

namespace IsoContainerPlayback.Formats.VideoCd
{
    /// <summary>
    /// Represents the service that handles calls to the defined API endpoint.
    /// </summary>
    public class VideoCdIsoContainerService : IService
    {
        #region Fields

        private readonly ILogger _logger;

        #endregion

        #region Construction

        /// <summary>Initializes a new instance of the <see cref="VideoCdIsoContainerService" /> service.</summary>
        /// <param name="logManager">The log manager.</param>
        public VideoCdIsoContainerService(ILogManager logManager)
        {
            _logger = logManager.GetLogger(nameof(VideoCdIsoContainerService));

            _logger.Info($"{nameof(VideoCdIsoContainerService)} is started.", 0);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a <see cref="VideoCdIsoStream"/> for the requested VideoCD ISO.
        /// </summary>
        /// <param name="request">The VideoCD ISO to stream.</param>
        /// <returns>A <see cref="VideoCdIsoStream"/> for the requested VideoCD ISO.</returns>
        public object Get(GetVideoCdIsoStream request)
        {
            _logger.Info($"VideoCD ISO `{request.IsoPath}` requested...", 0);

            return new VideoCdIsoStream(request.IsoPath);
        }

        #endregion
    }
}