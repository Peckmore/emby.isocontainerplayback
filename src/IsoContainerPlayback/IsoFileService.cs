using DiscUtils.Udf;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Services;
using System;
using System.IO;
using File = System.IO.File;

namespace IsoContainerPlayback
{
    /// <summary>
    /// Represents the service that handles calls to the "GetIsoFile" API endpoint.
    /// </summary>
    public class IsoFileService : IService
    {
        #region Fields

        private readonly ILogger _logger;

        #endregion

        #region Construction

        /// <summary>Initializes a new instance of the <see cref="IsoFileService" /> service.</summary>
        /// <param name="logManager">The log manager.</param>
        public IsoFileService(ILogManager logManager)
        {
            _logger = logManager.GetLogger(nameof(IsoFileService));

            _logger.Info($"{nameof(IsoFileService)} is started.", 0);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns an <see cref="OnDisposeStream" /> for the requested file from within the specified ISO.
        /// </summary>
        /// <param name="request">The file to open, and the ISO to open it from.</param>
        /// <returns>An <see cref="OnDisposeStream" /> for the requested file, if it exists; otherwise <see langword="null" />.</returns>
        public object Get(GetIsoFile request)
        {
            _logger.Info($"File `{request.Filename}` requested from ISO '{request.IsoPath}'...", 0);

            try
            {
                // Check that the requested ISO exists.
                _logger.Info("Checking that the requested ISO exists...");
                if (File.Exists(request.IsoPath))
                {
                    // The ISO exists, so grab a stream to it.
                    _logger.Info("ISO exists - opening stream...");
                    var isoStream = File.OpenRead(request.IsoPath);

                    // Create our UdfReader so we can access the ISO contents.
                    _logger.Info("Creating ISO UdfReader...");
                    var isoReader = new UdfReader(isoStream);

                    // Check that the requested file exists within the ISO.
                    _logger.Info("Checking that the requested file exists within the ISO...");
                    if (isoReader.FileExists(request.Filename))
                    {
                        // The file exists, so we'll grab a stream to it and return it. However, we'll wrap it in an OnDisposeStream
                        // so that when the stream is disposed of, the underlying UdfReader and FileStream are also disposed of.
                        _logger.Info("File exists - creating stream...");
                        return new OnDisposeStream(isoReader.OpenFile(request.Filename, FileMode.Open), () =>
                        {
                            // When this stream is diposed of, also dispose of our UdfReader and underlying FileStream.
                            _logger.Info("Stream disposing - cleaning up...");

                            _logger.Info("Disposing of UdfReader...");
                            isoReader.Dispose();
                            
                            _logger.Info("Disposing of ISO FileStream...");
                            isoStream.Dispose();
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // If any errors occurs then just swallow them so we don't bring anything down.
                _logger.ErrorException("There was a problem accessing the specified ISO or its contents.", ex);
            }

            // If we get here then either there was an error, or the requested ISO or file within the ISO could not be found, so
            // return null.
            _logger.Info("The ISO or file specified could not be found, or an error occurred - returning null.");
            return null;
        }

        #endregion
    }
}