using System;

namespace IsoContainerPlayback
{
    internal static class Exceptions
    {
        public static void StreamDisposed()
        {
            throw new ObjectDisposedException(null, "The stream has been closed.");
        }
    }
}