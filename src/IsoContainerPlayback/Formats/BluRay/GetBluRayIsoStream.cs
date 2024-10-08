using MediaBrowser.Model.Services;

namespace IsoContainerPlayback.Formats.BluRay
{
    /// <summary>
    /// Represents the API endpoint for reading a BluRay ISO video stream.
    /// </summary>
    [Route("/Iso/BluRayStream", "GET")]
    public class GetBluRayIsoStream
    {
        #region Properties

        /// <summary>
        /// The full path (including filename) of the BluRay ISO to open.
        /// </summary>
        [ApiMember(Name= "IsoPath",
                   Description = "The full path (including filename) of the BluRay ISO to open.",
                   IsRequired = true,
                   DataType = "string",
                   ParameterType = "path",
                   Verb = "GET",
                   AllowMultiple = false)]
        public string IsoPath { get; set; }
        /// <summary>
        /// The name (excluding path) of the playlist to play from the specified ISO (e.g., '00000.MPLS').
        /// </summary>
        /// <remarks>Please note that this is case-sensitive.</remarks>
        [ApiMember(Description = "The name (excluding path) of the playlist to play from the specified ISO (e.g., '00000.MPLS'). Please note that this is case-sensitive.",
                   IsRequired = true,
                   DataType = "string",
                   ParameterType = "path",
                   Verb = "GET",
                   AllowMultiple = false)]
        public string Playlist { get; set; }

        #endregion
    }
}