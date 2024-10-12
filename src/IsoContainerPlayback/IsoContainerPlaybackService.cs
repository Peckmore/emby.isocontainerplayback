using DiscUtils.Udf;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Services;
using System;
using System.Collections.Generic;
using System.IO;
using File = System.IO.File;

namespace IsoContainerPlayback
{
    /// <summary>
    /// Represents the service that handles calls to the API endpoints.
    /// </summary>
    public class IsoContainerPlaybackService : IService
    {
        #region Fields

        private readonly ILogger _logger;

        #endregion

        #region Construction

        /// <summary>Initializes a new instance of the <see cref="IsoContainerPlaybackService" /> service.</summary>
        /// <param name="logManager">The log manager.</param>
        public IsoContainerPlaybackService(ILogManager logManager)
        {
            _logger = logManager.GetLogger(nameof(IsoContainerPlaybackService));

            _logger.Info($"{nameof(IsoContainerPlaybackService)} is started.", 0);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a list of <see cref="IsoDirectoryEntryInfo" /> entries for files and directories within a directory on the specified ISO.
        /// </summary>
        /// <param name="request">The directory to query, and the ISO to query it from.</param>
        /// <returns>A list of <see cref="IsoDirectoryEntryInfo" /> objects for each entry within the directory, if it exists; otherwise
        /// <see langword="null" />.</returns>
        public object Get(GetIsoDirectory request)
        {
            _logger.Info($"Directory listing for directory `{request.DirectoryPath}` requested from ISO '{request.IsoPath}'...");

            if (string.IsNullOrEmpty(request.DirectoryPath))
            {
                _logger.Info("Requested directory was empty, so setting to root...");
                request.DirectoryPath = Path.DirectorySeparatorChar.ToString();
            }

            try
            {
                // Check that the requested ISO exists.
                _logger.Info("Checking that the requested ISO exists...");
                if (File.Exists(request.IsoPath))
                {
                    // The ISO exists, so grab a stream to it.
                    _logger.Info("ISO exists - opening stream...");
                    using (var isoStream = File.OpenRead(request.IsoPath))
                    {
                        // Create our UdfReader so we can access the ISO contents.
                        _logger.Info("Creating ISO UdfReader...");
                        using (var isoReader = new UdfReader(isoStream))
                        {
                            // Check that the requested directory exists within the ISO.
                            _logger.Info("Checking that the requested directory exists within the ISO...");
                            if (isoReader.DirectoryExists(request.DirectoryPath))
                            {
                                // The directory exists, so we'll grab a directory listing for it and return it.
                                _logger.Info("Directory exists - enumerating contents...");

                                // Create a list to store our directory entries.
                                var response = new List<IsoDirectoryEntryInfo>();

                                // Get the directory info.
                                var dirInfo = isoReader.GetDirectoryInfo(request.DirectoryPath);

                                // First enumerate directories.
                                _logger.Info($"Directories: {dirInfo.GetDirectories().Length} found)...");
                                foreach (var dir in dirInfo.GetDirectories())
                                {
                                    // Add each entry to our response.
                                    response.Add(new IsoDirectoryEntryInfo(dir.Name, dir.FullName, true));
                                }

                                // Then enumerate files.
                                _logger.Info($"Files: {dirInfo.GetFiles().Length} found)...");
                                foreach (var file in dirInfo.GetFiles())
                                {
                                    // Add each entry to our response.
                                    response.Add(new IsoDirectoryEntryInfo(file.Name, file.FullName, false));
                                }

                                // Now reurn the list.
                                _logger.Info($"Returning directory contents ({response.Count} entries found)...");
                                return response;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // If any errors occurs then just swallow them so we don't bring anything down.
                _logger.ErrorException("There was a problem accessing the specified ISO or its contents.", ex);
            }

            // If we get here then either there was an error, or the requested ISO or directory within the ISO could not be found, so
            // return null.
            _logger.Info("The ISO or directory specified could not be found, or an error occurred - returning null.");
            return null;
        }
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