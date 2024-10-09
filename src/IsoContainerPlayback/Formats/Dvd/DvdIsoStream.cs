using DiscUtils.Udf;
using System.IO;
using File = System.IO.File;

namespace IsoContainerPlayback.Formats.Dvd
{
    /// <summary>
    /// An <see cref="IsoStream"/> implementation which represents a video stored on a DVD ISO. The video is defined by a title
    /// number, and may be spread across multiple files within the ISO.
    /// </summary>
    public class DvdIsoStream : IsoStream
    {
        #region Fields

        private readonly UdfReader _dvdReader;
        private readonly Stream _dvdStream;

        #endregion

        #region Construction

        /// <summary>
        /// Creates a new <see cref="DvdIsoStream"/> instance.
        /// </summary>
        /// <param name="isoPath">The path on disk to the DVD ISO file to open.</param>
        /// <param name="title">The value of the title to be streamed from the ISO (e.g., VTS_[title]_1.VOB).</param>
        /// <exception cref="IOException">Thrown if the requested ISO cannot be found or accessed, or if the requested title does
        /// not exist on the specified ISO.</exception>
        public DvdIsoStream(string isoPath, int title)
        {
            // First we check whether the ISO specified exists and is accessible.
            if (!File.Exists(isoPath))
            {
                throw Exceptions.IsoDoesNotExist(isoPath);
            }

            // We've found the ISO, so let's create a stream to it.
            _dvdStream = File.OpenRead(isoPath);

            // Now we'll create our UdfReader for the ISO.
            _dvdReader = new UdfReader(_dvdStream);

            // We'll check whether the specified playlist exists on the ISO, and if it does we'll grab it.
            var vobPath = GenerateVobPath(title, 1);
            if (!_dvdReader.FileExists(vobPath))
            {
                throw Exceptions.IsoVideoFileDoesNotExist(vobPath);
            }

            // Now we'll loop through all matching VOB files, starting at "01", and add them to our list.
            for (var x = 1; x < 100; x++)
            {
                // Generate the VOB path.
                vobPath = GenerateVobPath(title, x);

                // Check whether the VOB exists, and add it to our list if it does.
                if (_dvdReader.FileExists(vobPath))
                {
                    // Create the stream to the file on the ISO.
                    var fileStream = _dvdReader.OpenFile(vobPath, FileMode.Open);

                    // Add the stream to our list of streams.
                    FileStreams.Add(new StreamInfo(fileStream, Length, Path.GetFileName(vobPath)));

                    // Increment the total length for this stream.
                    IncrementLength(fileStream.Length);
                }
                else
                {
                    // VOBs have to be continuous and sequential, so if we haven't found a VOB we've reached the end of the video
                    // files for this title.
                    break;
                }
            }
        }

        #endregion

        #region Methods

        #region Private

        private string GenerateVobPath(int title, int part)
        {
            // Generate the expected video file name for a VOB file, based on the title and part numbers.
            return $@"VIDEO_TS\VTS_{title:d2}_{part}.VOB";
        }

        #endregion

        #region Protected

        /// <inheritdoc/>
        protected override void OnDispose(bool disposing)
        {
            // Dispose of our UdfReader.
            _dvdReader.Dispose();

            // Dispose of our ISO file stream.
            _dvdStream.Dispose();
        }

        #endregion

        #endregion
    }
}