using MediaBrowser.Controller.Net;
using MediaBrowser.Model.Services;

namespace IsoContainerPlayback
{
    /// <summary>
    /// Represents the API endpoint for reading a file from within an ISO.
    /// </summary>
    [Route("/Iso/GetFile", "GET")]
    public class GetIsoFile
    {
        #region Properties

        /// <summary>
        /// The name (including path) of the file to open from within the specified ISO.
        /// </summary>
        [ApiMember(Description = "The name (including path) of the file to open from within the specified ISO.",
                   IsRequired = true,
                   DataType = "string",
                   ParameterType = "path",
                   Verb = "GET",
                   AllowMultiple = false)]
        [Unauthenticated] // Temporary whilst testing
        public string Filename { get; set; }
        // Hardcoded ISO path for now.
        public string IsoPath => IsoContainerPlaybackPlugin.IsoPath;

        #endregion
    }
}