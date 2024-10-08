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

        public static IEnumerable<object[]> GetDiscs
        {
            get
            {
                return
                [
                    //[new BluRayIsoStream(@"..\..\..\..\..\resources\testdata\bluray_sequential.iso", "00000.MPLS")],
                    [new DvdIsoStream(@"..\..\..\..\..\resources\testdata\dvd.iso", 3)],
                    [new VideoCdIsoStream(@"..\..\..\..\..\resources\testdata\videocd.iso")]
                ];
            }
        }

        #endregion

        #region Methods

        private bool CheckResults(byte[] data, int expectedSize, long position)
        {
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
            return (byte)(position % 256);
        }

        #endregion

        #region Tests

        [Theory]
        [MemberData(nameof(GetDiscs))]
        public void CanRead(IsoStream disc)
        {
            disc.Should().NotBeNull();

            disc.CanRead.Should().BeTrue();
        }
        [Theory]
        [MemberData(nameof(GetDiscs))]
        public void CanSeek(IsoStream disc)
        {
            disc.Should().NotBeNull();

            disc.CanSeek.Should().BeTrue();
        }
        [Theory]
        [MemberData(nameof(GetDiscs))]
        public void CanTimeout(IsoStream disc)
        {
            disc.Should().NotBeNull();

            disc.CanTimeout.Should().BeFalse();
        }
        [Theory]
        [MemberData(nameof(GetDiscs))]
        public void CanWrite(IsoStream disc)
        {
            disc.Should().NotBeNull();

            disc.CanWrite.Should().BeFalse();
        }
        /// <summary>
        /// Read the entire stream from an ISO in 63 byte chunks using Read() and verify that each
        /// byte is correct.
        /// </summary>
        /// <remarks>This test uses a sequential ISO, where every video file consists of the values
        /// 0-255 in ascending order, repeating.
        /// <para>63 byte chunks were chosen as a value not divisible by 8 to ensure that video
        /// boundaries would occur mid-read and are handled correctly.</para></remarks>
        [Theory]
        [MemberData(nameof(GetDiscs))]
        public void Read(IsoStream disc)
        {
            disc.Should().NotBeNull();

            var index = 0;
            while (index < disc.Length)
            {
                var data = new byte[63];
                var bytesRead = disc.Read(data, 0, data.Length);
                CheckResults(data, bytesRead, index).Should().BeTrue();

                index += data.Length;
            }
        }
        /// <summary>
        /// Read the entire stream from an ISO one byte at a time using ReadByte() and verify that
        /// each byte is correct.
        /// </summary>
        /// <remarks>This test uses a sequential ISO, where every video file consists of the values
        /// 0-255 in ascending order, repeating.</remarks>
        [Theory]
        [MemberData(nameof(GetDiscs))]
        public void ReadByte(IsoStream disc)
        {
            disc.Should().NotBeNull();

            byte compareValue = 0;
            var fail = false;
            for (var index = 0; index < disc.Length; index++)
            {
                var b = (byte)disc.ReadByte();
                if (b != compareValue)
                {
                    fail = true;
                    break;
                }
                compareValue++;
            }

            fail.Should().BeFalse();
        }
        [Theory]
        [MemberData(nameof(GetDiscs))]
        public void ReadTimeout(IsoStream disc)
        {
            disc.Should().NotBeNull();

            var test = () => disc.ReadTimeout;
            test.Should().Throw<InvalidOperationException>();
        }
        [Theory]
        [MemberData(nameof(GetDiscs))]
        public void Seek(IsoStream disc)
        {
            disc.Should().NotBeNull();

            var rnd = new Random();

            // SeekOrigin.Begin
            for (var count = 1; count <= RandomSeekIterations; count++)
            {
                var offset = rnd.NextInt64(0, disc.Length - count);
                var expectedPosition = offset;

                disc.Seek(offset, SeekOrigin.Begin);
                disc.Position.Should().Be(expectedPosition);

                var data = new byte[count];
                var bytesRead = disc.Read(data, 0, data.Length);
                CheckResults(data, bytesRead, expectedPosition).Should().BeTrue();
            }

            // SeekOrigin.Current
            bool negative = false;
            for (var count = 1; count <= RandomSeekIterations; count++)
            {
                if (disc.Position > (0.9 * disc.Length))
                {
                    negative = true;
                }
                else if (disc.Position < (0.1 * disc.Length))
                {
                    negative = false;
                }

                long expectedPosition;
                long offset;
                if (negative)
                {
                    offset = rnd.NextInt64(-disc.Position, -count);
                    expectedPosition = disc.Position + offset;
                }
                else
                {
                    offset = rnd.NextInt64(0, disc.Length - disc.Position - count);
                    expectedPosition = disc.Position + offset;
                }

                disc.Seek(offset, SeekOrigin.Current);
                disc.Position.Should().Be(expectedPosition);

                var data = new byte[count];
                var bytesRead = disc.Read(data, 0, data.Length);
                CheckResults(data, bytesRead, expectedPosition).Should().BeTrue();
            }

            // SeekOrigin.End
            for (var count = 1; count <= RandomSeekIterations; count++)
            {
                var offset = rnd.NextInt64(count, disc.Length);
                var expectedPosition = disc.Length - offset;

                disc.Seek(offset, SeekOrigin.End);
                disc.Position.Should().Be(expectedPosition);

                var data = new byte[count];
                var bytesRead = disc.Read(data, 0, data.Length);
                CheckResults(data, bytesRead, expectedPosition).Should().BeTrue();
            }
        }
        [Theory]
        [MemberData(nameof(GetDiscs))]
        public void SetLength(IsoStream disc)
        {
            disc.Should().NotBeNull();

            var test = () => disc.SetLength(10);
            test.Should().Throw<NotSupportedException>();
        }
        [Theory]
        [MemberData(nameof(GetDiscs))]
        public void Write(IsoStream disc)
        {
            disc.Should().NotBeNull();

            var test = () => disc.Write(new byte[10], 0, 10);
            test.Should().Throw<NotSupportedException>();
        }
        [Theory]
        [MemberData(nameof(GetDiscs))]
        public void WriteByte(IsoStream disc)
        {
            disc.Should().NotBeNull();

            var test = () => disc.WriteByte(0);
            test.Should().Throw<NotSupportedException>();
        }
        [Theory]
        [MemberData(nameof(GetDiscs))]
        public void WriteTimeout(IsoStream disc)
        {
            disc.Should().NotBeNull();

            var test = () => disc.WriteTimeout;
            test.Should().Throw<InvalidOperationException>();
        }

        #endregion
    }
}