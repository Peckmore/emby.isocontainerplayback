using BDInfo;
using System.IO;

namespace IsoContainerPlayback.Formats.BluRay
{
    /// <summary>
    /// An <see cref="IsoStream"/> implementation which represents a video stored on a BluRay ISO. The video is defined by an MPLS
    /// playlist file, and may be spread across multiple files within the ISO.
    /// </summary>
    public class BluRayIsoStream : IsoStream
    {
        #region Fields

        private readonly BDROM _bluRayIso;

        #endregion

        #region Construction

        /// <summary>
        /// Creates a new <see cref="BluRayIsoStream"/> instance.
        /// </summary>
        /// <param name="isoPath">The path on disk to the BluRay ISO file to open.</param>
        /// <param name="playlistFilename">The filename (without path) of the MPLS playlist to load from the specified ISO.</param>
        /// <exception cref="IOException">Thrown if the requested ISO cannot be found or accessed, or if the requested playlist does
        /// not exist on the specified ISO.</exception>
        public BluRayIsoStream(string isoPath, string playlistFilename)
        {
            // First we check whether the ISO specified exists and is accessible.
            if (!File.Exists(isoPath))
            {
                throw Exceptions.IsoDoesNotExist(isoPath);
            }

            // We've found the ISO, so let's create a BDROM instance.
            _bluRayIso = new BDROM(isoPath);

            // Now let's do a scan to get our playlist files.
            _bluRayIso.Scan();

            // We'll check whether the specified playlist exists on the ISO, and if it does we'll grab it.
            if (!_bluRayIso.PlaylistFiles.TryGetValue(playlistFilename, out var playlistFile))
            {
                throw Exceptions.IsoPlaylistDoesNotExist(playlistFilename);
            }

            // Now we'll loop through the playlist and grab a stream for each file specified by the playlist and add them to our list.
            foreach (var file in playlistFile.StreamClips)
            {
                // Create the stream to the file on the ISO.
                var fileStream = _bluRayIso.CdReader.OpenFile(file.StreamFile.DFileInfo.FullName, FileMode.Open);

                // Add the stream to our list of streams.
                FileStreams.Add(new StreamInfo(fileStream, Length, file.StreamFile.DFileInfo.Name));

                // Increment the total length for this stream.
                IncrementLength(fileStream.Length);
            }
        }

        #endregion

        #region Methods

        #region Protected

        /// <inheritdoc/>
        protected override void OnDispose(bool disposing)
        {
            // Close our BDROM image, which disposes the underlying stream and UdfReader.
            _bluRayIso.CloseDiscImage();
        }

        #endregion

        #endregion
    }
}