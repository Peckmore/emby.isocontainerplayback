using FluentAssertions;
using IsoContainerPlayback.Formats.Dvd;
using IsoContainerPlayback.Formats.VideoCd;

namespace IsoContainerPlayback.Tests
{
    public class StreamTests
    {
        #region Constants

        private const int RandomSeekIterations = 10000;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the various supported test ISO images to supply to our tests.
        /// </summary>
        public static IEnumerable<object[]> GetIsos
        {
            get
            {
                return
                [
                    // Currently unable to generate a fake BluRay using test data, so we have no BluRay test for now. However,
                    // given that all of the formats use the same base stream, they should all either pass or fail! :)
                    //[new BluRayIsoStream(@"..\..\..\..\..\resources\test_discs\bluray.iso", "00000.MPLS")],
                    [new DvdIsoStream(@"..\..\..\..\..\resources\test_discs\dvd.iso", 3)],
                    [new VideoCdIsoStream(@"..\..\..\..\..\resources\test_discs\videocd.iso")]
                ];
            }
        }

        #endregion

        #region Methods

        private void CheckDisposedStream(IsoStream iso)
        {
            // Check that the following properties return the correct values.
            iso.CanRead.Should().BeFalse();
            iso.CanSeek.Should().BeFalse();
            iso.CanTimeout.Should().BeFalse();
            iso.CanWrite.Should().BeFalse();

            // Check that the following properties and methods throw an ObjectDisposedException.
            var testString = () => _ = iso.ActiveFile;
            testString.Should().Throw<ObjectDisposedException>();

            var testLong = () => _ = iso.Length;
            testLong.Should().Throw<ObjectDisposedException>();

            testLong = () => _ = iso.Position;
            testLong.Should().Throw<ObjectDisposedException>();

            var testVoid = () => iso.Flush();
            testVoid.Should().Throw<ObjectDisposedException>();

            var testInt = () => iso.Read(new byte[1], 0, 1);
            testInt.Should().Throw<ObjectDisposedException>();

            var testByte = () => iso.ReadByte();
            testByte.Should().Throw<ObjectDisposedException>();

            testLong = () => iso.Seek(0, SeekOrigin.Begin);
            testLong.Should().Throw<ObjectDisposedException>();
        }
        private bool CheckResults(byte[] data, int expectedSize, long position)
        {
            // This is a helper method to iterate through a byte array and check that the value at each byte is of the expected value.
            // The position of the first byte within the file it is taken from is supplied, meaning we can calculate the expected value
            // at any position within the array, and check it against the actual value.

            for (int x = 0; x < expectedSize; x++)
            {
                if (data[x] != GetSequentialValue(position + x))
                {
                    return false;
                }
            }

            return true;
        }
        private byte GetSequentialValue(long position)
        {
            // Because we know that all of our data files consist of the values 0-255, repeating for the length of the file, we can
            // determine the correct value at any position within a file by taking the modulus of the position wth 256.

            return (byte)(position % 256);
        }

        #endregion

        #region Tests

        /// <summary>
        /// Verify that <see cref="IsoStream.CanRead" /> returns <see langword="true"/>.
        /// </summary>
        [Theory]
        [MemberData(nameof(GetIsos))]
        public void CanRead(IsoStream iso)
        {
            iso.Should().NotBeNull();

            iso.CanRead.Should().BeTrue();
        }
        /// <summary>
        /// Verify that <see cref="IsoStream.CanSeek" /> returns <see langword="true"/>.
        /// </summary>
        [Theory]
        [MemberData(nameof(GetIsos))]
        public void CanSeek(IsoStream iso)
        {
            iso.Should().NotBeNull();

            iso.CanSeek.Should().BeTrue();
        }
        /// <summary>
        /// Verify that <see cref="IsoStream.CanTimeout" /> returns <see langword="false"/>.
        /// </summary>
        [Theory]
        [MemberData(nameof(GetIsos))]
        public void CanTimeout(IsoStream iso)
        {
            iso.Should().NotBeNull();

            iso.CanTimeout.Should().BeFalse();
        }
        /// <summary>
        /// Verify that <see cref="IsoStream.CanWrite" /> returns <see langword="false"/>.
        /// </summary>
        [Theory]
        [MemberData(nameof(GetIsos))]
        public void CanWrite(IsoStream iso)
        {
            iso.Should().NotBeNull();

            iso.CanWrite.Should().BeFalse();
        }
        [Theory]
        [MemberData(nameof(GetIsos))]
        public void Close(IsoStream iso)
        {
            iso.Should().NotBeNull();

            // Close the ISO.
            iso.Close();

            // Check it has closed/disposed correctly.
            CheckDisposedStream(iso);
        }
        [Theory]
        [MemberData(nameof(GetIsos))]
        public void Dispose(IsoStream iso)
        {
            iso.Should().NotBeNull();

            // Dispose the ISO.
            iso.Dispose();

            // Check it has closed/disposed correctly.
            CheckDisposedStream(iso);
        }
        /// <summary>
        /// Read the entire stream from an ISO in 63 byte chunks using <see cref="IsoStream.Read" /> and verify that each byte is correct.
        /// </summary>
        /// <remarks>63 byte chunks were chosen as a value not divisible by 8 to ensure that video boundaries would occur mid-read
        /// and are handled correctly.</remarks>
        [Theory]
        [MemberData(nameof(GetIsos))]
        public void Read(IsoStream iso)
        {
            iso.Should().NotBeNull();

            // Create a variable to track our expected position within the ISO.
            var position = 0;

            // Loop through the length of the ISO.
            while (position < iso.Length)
            {
                // Create our 63 byte array to read data into.
                var data = new byte[63];

                // Read 63 bytes from the ISO (using it's own internal position).
                var bytesRead = iso.Read(data, 0, data.Length);

                // Check that the data in the array matches what we expect.
                CheckResults(data, bytesRead, position).Should().BeTrue();

                // Increment our position tracker.
                position += data.Length;
            }
        }
        /// <summary>
        /// Read the entire stream from an ISO one byte at a time using <see cref="IsoStream.ReadByte" /> and verify that each byte
        /// is correct.
        /// </summary>
        [Theory]
        [MemberData(nameof(GetIsos))]
        public void ReadByte(IsoStream iso)
        {
            iso.Should().NotBeNull();

            // Create a variable to track what we expect each read byte to be. Because we are reading one byte at a time we can simply
            // increase this variable by one after each read. And as it is a byte, once we get to 255 it will wrap around back to 0.
            byte compareValue = 0;

            // A variable to track whether any comparisons have failed.
            var fail = false;

            // Loop through the length of the ISO.
            for (var position = 0; position < iso.Length; position++)
            {
                // Read a byte from the ISO.
                var b = (byte)iso.ReadByte();

                // Check that the value matches what we expect.
                if (b != compareValue)
                {
                    // If we're here then the value didn't match, so set our fail flag to true.
                    fail = true;

                    // There's no point checking any further of the ISO as the test has failed, so abort the loop.
                    break;
                }

                // Increment our expected read value, ready for the next byte.
                compareValue++;
            }

            // Indicate whether the test passed or failed.
            fail.Should().BeFalse();
        }
        /// <summary>
        /// Verify that <see cref="IsoStream" /> throws an exception if <see cref="IsoStream.ReadTimeout" /> is accessed.
        /// </summary>
        [Theory]
        [MemberData(nameof(GetIsos))]
        public void ReadTimeout(IsoStream iso)
        {
            iso.Should().NotBeNull();

            var test = () => iso.ReadTimeout;
            test.Should().Throw<InvalidOperationException>();
        }
        /// <summary>
        /// Perform random seeks across an <see cref="IsoStream" /> and read chunks of data in increasing size using <see cref="IsoStream.ReadByte" />
        /// and verify that each byte is correct.
        /// </summary>
        [Theory]
        [MemberData(nameof(GetIsos))]
        public void Seek(IsoStream iso)
        {
            iso.Should().NotBeNull();

            // We need a RNG to generate random seek amounts.
            var rnd = new Random();

            // We'll test all 3 SeekOrigin types, and we'll perform several thousand seeks per ISO to make sure there are no issues with
            // any positions, boundaries, etc. We'll also increment how many bytes we read each time (from 1 to our iteration count) to
            // stress all different types of reads, boundaries, etc.

            // SeekOrigin.Begin
            for (var count = 1; count <= RandomSeekIterations; count++)
            {
                // Generate a new offset for how far into the ISO we'll jump (from 0). We subtract count to ensure we'll always have
                // enough bytes to read.
                var offset = rnd.NextInt64(0, iso.Length - count);

                // Store the expected ISO position.
                var expectedPosition = offset;

                // Set the position by performing a seek.
                iso.Seek(offset, SeekOrigin.Begin);

                // Verify that the position is correct.
                iso.Position.Should().Be(expectedPosition);

                // Create our byte array to read data into.
                var data = new byte[count];

                // Read the bytes from the ISO (using it's own internal position now that we've set it).
                var bytesRead = iso.Read(data, 0, data.Length);

                // Check that the data in the array matches what we expect.
                CheckResults(data, bytesRead, expectedPosition).Should().BeTrue();
            }

            // SeekOrigin.Current

            // To seek based on the current position we'll switch direction (forwards or backwards) depending on how close we are to the
            // start or end of the ISO. This boolean will track our direction.
            bool negative = false;

            for (var count = 1; count <= RandomSeekIterations; count++)
            {
                if (iso.Position > (0.9 * iso.Length))
                {
                    // If we're near the end of the ISO, switch to moving backwards.
                    negative = true;
                }
                else if (iso.Position < (0.1 * iso.Length))
                {
                    // Else if we're near the start of the ISO, switch to moving forwards.
                    negative = false;
                }

                // Variables to store offset and expected position.
                long expectedPosition;
                long offset;
                if (negative)
                {
                    // We're moving backwards, so our offset has to be negative, and somewhere between where we are now (iso.Position)
                    // and the start of the ISO. We buffer by the length of count to ensure there is enough data to read.
                    offset = rnd.NextInt64(-iso.Position, -count);

                    // Store the expected ISO position.
                    expectedPosition = iso.Position + offset;
                }
                else
                {
                    // We're moving forwards, so our offset has to be positive, and somewhere between where we are now and the end of
                    // the ISO (iso.Length - iso.Position). We buffer by the length of count to ensure there is enough data to read.
                    offset = rnd.NextInt64(0, iso.Length - iso.Position - count);

                    // Store the expected ISO position.
                    expectedPosition = iso.Position + offset;
                }

                // Set the position by performing a seek.
                iso.Seek(offset, SeekOrigin.Current);

                // Verify that the position is correct.
                iso.Position.Should().Be(expectedPosition);

                // Create our byte array to read data into.
                var data = new byte[count];

                // Read the bytes from the ISO (using it's own internal position now that we've set it).
                var bytesRead = iso.Read(data, 0, data.Length);

                // Check that the data in the array matches what we expect.
                CheckResults(data, bytesRead, expectedPosition).Should().BeTrue();
            }

            // SeekOrigin.End
            for (var count = 1; count <= RandomSeekIterations; count++)
            {
                // Generate a new offset for how far into the ISO we'll jump (from the end). We add count to ensure we'll always have
                // enough bytes to read.
                var offset = rnd.NextInt64(count, iso.Length);

                // Store the expected ISO position.
                var expectedPosition = iso.Length - offset;

                // Set the position by performing a seek.
                iso.Seek(offset, SeekOrigin.End);

                // Verify that the position is correct.
                iso.Position.Should().Be(expectedPosition);

                // Create our byte array to read data into.
                var data = new byte[count];

                // Read the bytes from the ISO (using it's own internal position now that we've set it).
                var bytesRead = iso.Read(data, 0, data.Length);

                // Check that the data in the array matches what we expect.
                CheckResults(data, bytesRead, expectedPosition).Should().BeTrue();
            }
        }
        /// <summary>
        /// Verify that an <see cref="IsoStream" /> throws an exception if <see cref="IsoStream.SetLength" /> is accessed.
        /// </summary>
        [Theory]
        [MemberData(nameof(GetIsos))]
        public void SetLength(IsoStream iso)
        {
            iso.Should().NotBeNull();

            var test = () => iso.SetLength(10);
            test.Should().Throw<NotSupportedException>();
        }
        /// <summary>
        /// Verify that an <see cref="IsoStream" /> throws an exception if <see cref="IsoStream.Write" /> is accessed.
        /// </summary>
        [Theory]
        [MemberData(nameof(GetIsos))]
        public void Write(IsoStream iso)
        {
            iso.Should().NotBeNull();

            var test = () => iso.Write(new byte[10], 0, 10);
            test.Should().Throw<NotSupportedException>();
        }
        /// <summary>
        /// Verify that an <see cref="IsoStream" /> throws an exception if <see cref="IsoStream.WriteByte" /> is accessed.
        /// </summary>
        [Theory]
        [MemberData(nameof(GetIsos))]
        public void WriteByte(IsoStream iso)
        {
            iso.Should().NotBeNull();

            var test = () => iso.WriteByte(0);
            test.Should().Throw<NotSupportedException>();
        }
        /// <summary>
        /// Verify that an <see cref="IsoStream" /> throws an exception if <see cref="IsoStream.WriteTimeout" /> is accessed.
        /// </summary>
        [Theory]
        [MemberData(nameof(GetIsos))]
        public void WriteTimeout(IsoStream iso)
        {
            iso.Should().NotBeNull();

            var test = () => iso.WriteTimeout;
            test.Should().Throw<InvalidOperationException>();
        }

        #endregion
    }
}