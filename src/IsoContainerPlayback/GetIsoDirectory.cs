using MediaBrowser.Controller.Net;
using MediaBrowser.Model.Services;
using System.Collections.Generic;

namespace IsoContainerPlayback
{
    /// <summary>
    /// Represents the API endpoint for retrieving a directory listing for a directory within an ISO.
    /// </summary>
    [Route("/Iso/GetDirectory", "GET")]
    [Unauthenticated] // Temporary whilst testing
    public class GetIsoDirectory : IReturn<IList<IsoDirectoryEntryInfo>>
    {
        #region Properties

        /// <summary>
        /// The path within the specified ISO to get a directory listing for.
        /// </summary>
        [ApiMember(Description = "The path within the specified ISO to get a directory listing for.",
                   IsRequired = true,
                   DataType = "string",
                   ParameterType = "query",
                   Verb = "GET",
                   AllowMultiple = false)]
        public string DirectoryPath { get; set; }
        // Hardcoded ISO path for now.
        public string IsoPath => IsoContainerPlaybackPlugin.IsoPath;

        #endregion
    }
}