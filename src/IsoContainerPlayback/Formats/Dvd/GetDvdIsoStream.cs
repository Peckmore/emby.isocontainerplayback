using MediaBrowser.Model.Services;

namespace IsoContainerPlayback.Formats.Dvd
{
    /// <summary>
    /// Represents the API endpoint for reading a DVD ISO video stream.
    /// </summary>
    [Route("/Iso/DvdStream", "GET")]
    public class GetDvdIsoStream
    {
        #region Properties
        
        /// <summary>
        /// The full path (including filename) of the DVD ISO to open.
        /// </summary>
        [ApiMember(Description = "The full path (including filename) of the DVD ISO to open.",
                   IsRequired = true,
                   DataType = "string",
                   ParameterType = "path",
                   Verb = "GET",
                   AllowMultiple = false)]
        public string IsoPath { get; set; }
        /// <summary>
        /// The number of the title to play from the specified ISO (e.g., '2').
        /// </summary>
        [ApiMember(Description = "The number of the title to play from the specified ISO (e.g., '2').",
                   IsRequired = true,
                   DataType = "int",
                   ParameterType = "path",
                   Verb = "GET",
                   AllowMultiple = false)]
        public int Title { get; set; }

        #endregion
    }
}