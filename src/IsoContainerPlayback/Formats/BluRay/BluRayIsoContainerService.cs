using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Services;

namespace IsoContainerPlayback.Formats.BluRay
{
    /// <summary>
    /// Represents the service that handles calls to the defined API endpoint.
    /// </summary>
    public class BluRayIsoContainerService : IService
    {
        #region Fields

        private readonly ILogger _logger;

        #endregion

        #region Construction

        /// <summary>Initializes a new instance of the <see cref="BluRayIsoContainerService" />.</summary>
        /// <param name="logManager">The log manager.</param>
        public BluRayIsoContainerService(ILogManager logManager)
        {
            _logger = logManager.GetLogger(nameof(BluRayIsoContainerService));

            _logger.Info($"{nameof(BluRayIsoContainerService)} is starting...", 0);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a <see cref="BluRayIsoStream"/> for the requested BluRay ISO and playlist.
        /// </summary>
        /// <param name="request">The BluRay ISO and playlist to stream.</param>
        /// <returns>A <see cref="BluRayIsoStream"/> for the requested BluRay ISO and playlist.</returns>
        public object Get(GetBluRayIsoStream request)
        {
            _logger.Info($"BluRay ISO `{request.IsoPath}` requested...", 0);
            
            return new BluRayIsoStream(request.IsoPath, request.Playlist);
        }

        #endregion
    }
}