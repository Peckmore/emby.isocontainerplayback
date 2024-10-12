using System;
using System.IO;

namespace IsoContainerPlayback
{
    // Adapted from: https://stackoverflow.com/a/34079296
    /// <summary>
    /// A <see cref="Stream" /> wrapper that can be used to perform additional cleanup when the stream is disposed.
    /// </summary>
    internal class OnDisposeStream : Stream
    {
        #region Fields

        private readonly Action _onDispose;
        private readonly Stream _stream;

        #endregion

        #region Construction

        /// <summary>
        /// Creates a new <see cref="OnDisposeStream" /> instance.
        /// </summary>
        /// <param name="stream">The <see cref=Stream" /> to wrap.</param>
        /// <param name="onDispose">The <see cref="Action" /> to invoke when the stream is disposed.</param>
        public OnDisposeStream(Stream stream, Action onDispose)
        {
            // Set our variables.
            _stream = stream;
            _onDispose = onDispose;
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public override bool CanRead => _stream.CanRead;
        /// <inheritdoc />
        public override bool CanSeek => _stream.CanSeek;
        /// <inheritdoc />
        public override bool CanWrite => _stream.CanWrite;
        /// <inheritdoc />
        public override long Length => _stream.Length;
        /// <inheritdoc />
        public override long Position
        {
            get { return _stream.Position; }
            set { _stream.Position = value; }
        }

        #endregion

        #region Methods

        #region Protected

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _stream.Dispose();
            _onDispose();
            base.Dispose(disposing);
        }

        #endregion

        #region Public

        /// <inheritdoc />
        public override void Flush()
        {
            _stream.Flush();
        }
        /// <inheritdoc />
        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }
        /// <inheritdoc />
        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }
        /// <inheritdoc />
        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }
        /// <inheritdoc />
        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }

        #endregion

        #endregion
    }
}