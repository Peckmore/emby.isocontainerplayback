using DiscUtils.Iso9660;
using System.IO;
using File = System.IO.File;

namespace IsoContainerPlayback.Formats.VideoCd
{
    /// <summary>
    /// An <see cref="IsoStream"/> implementation which represents a video stored on a VideoCD ISO. The video 
    /// may be spread across multiple files within the ISO.
    /// </summary>
    public class VideoCdIsoStream : IsoStream
    {
        #region Fields

        private readonly CDReader _cdReader;
        private readonly Stream _cdStream;

        #endregion

        #region Construction

        /// <summary>
        /// Creates a new <see cref="VideoCdIsoStream"/> instance.
        /// </summary>
        /// <param name="isoPath">The path on disk to the VideoCD ISO file to open.</param>
        /// <exception cref="IOException">Thrown if the requested ISO cannot be found or accessed.</exception>
        public VideoCdIsoStream(string isoPath)
        {
            // First we check whether the ISO specified exists and is accessible.
            if (!File.Exists(isoPath))
            {
                throw new IOException($"The requested ISO file does not exist or could not be accessed.\n\nPath: {isoPath}");
            }

            // We've found the ISO, so let's create a stream to it.
            _cdStream = File.OpenRead(isoPath);

            // Now we'll create our CDReader for the ISO.
            _cdReader = new CDReader(_cdStream, true);

            // Now we'll check whether a VideoCD DAT file exists on the ISO to know whether we have a valid ISO.
            var datPath = GenerateDatPath(1);
            if (!_cdReader.FileExists(datPath))
            {
                throw new IOException("No video files were found on the specified ISO.");
            }

            // Now we'll loop through all DAT files, starting at "01", and add them to our list.
            for (var x = 1; x < 100; x++)
            {
                // Generate the DAT path.
                datPath = GenerateDatPath(x);

                // Now check the DAT exists and add it to our list if it does.
                if (_cdReader.FileExists(datPath))
                {
                    // Create the stream to the file on the ISO.
                    var fileStream = _cdReader.OpenFile(datPath, FileMode.Open);

                    // Add the stream to our list of streams.
                    FileStreams.Add(new StreamInfo(fileStream, Length, Path.GetFileName(datPath)));

                    // Increment the total length for this stream.
                    IncrementLength(fileStream.Length);
                }
                else
                {
                    // DATs have to be continuous and sequential, so if we haven't found a DAT we've reached the end
                    // of the video files for this VideoCD.
                    break;
                }
            }
        }

        #endregion

        #region Methods

        #region Private

        private string GenerateDatPath(int part)
        {
            return $@"MPEGAV\AVSEQ{part:d2}.DAT";
        }

        #endregion

        #region Protected

        /// <inheritdoc/>
        protected override void OnDispose(bool disposing)
        {
            // Dispose of our CDReader.
            _cdReader.Dispose();

            // Dispose of our ISO file stream.
            _cdStream.Dispose();
        }

        #endregion

        #endregion
    }
}