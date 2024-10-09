using System;
using System.IO;

namespace IsoContainerPlayback
{
    internal static class Exceptions
    {
        #region Methods

        public static IOException IsoDoesNotExist(string isoPath)
        {
            return new IOException($"The requested ISO file does not exist or could not be accessed.\n\nPath: {isoPath}");
        }
        public static IOException IsoPlaylistDoesNotExist(string playlistFilename)
        {
            return new IOException($"The requested playlist does not exist on the specified ISO.\n\nPlaylist: {playlistFilename}");
        }
        public static IOException IsoVideoFileDoesNotExist(string videoPath)
        {
            return new IOException($"The requested video file does not exist on the specified ISO.\n\nVideo: {videoPath}");
        }
        public static ObjectDisposedException StreamDisposed()
        {
            return new ObjectDisposedException(null, "The stream has been closed.");
        }
        public static InvalidOperationException StreamDoesNotSupportTimeouts()
        {
            return new InvalidOperationException("The stream does not support timeouts.");
        }
        public static NotSupportedException StreamDoesNotSupportWriting()
        {
            return new NotSupportedException("The stream does not support writing.");
        }

        #endregion
    }
}