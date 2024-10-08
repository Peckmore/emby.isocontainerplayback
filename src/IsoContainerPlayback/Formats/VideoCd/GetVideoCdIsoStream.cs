using MediaBrowser.Model.Services;

namespace IsoContainerPlayback.Formats.VideoCd
{
    /// <summary>
    /// Represents the API endpoint for reading a VideoCD ISO video stream.
    /// </summary>
    [Route("/Iso/VideoCdStream", "GET")]
    public class GetVideoCdIsoStream
    {
        #region Properties

        /// <summary>
        /// The full path (including filename) of the VideoCD ISO to open.
        /// </summary>
        [ApiMember(Description = "The full path (including filename) of the VideoCD ISO to open.",
                   IsRequired = true,
                   DataType = "string",
                   ParameterType = "path",
                   Verb = "GET",
                   AllowMultiple = false)]
        public string IsoPath { get; set; }

        #endregion
    }
}