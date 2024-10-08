namespace BDInfo
{
    /// <summary>
    /// A shim for the settings file in the original BDInfo project.
    /// </summary>
    internal static class BDInfoSettings
    {
        public static bool EnableSSIF => false;
        public static bool ExtendedStreamDiagnostics => true;
        public static bool FilterLoopingPlaylists => true;
        public static bool FilterShortPlaylists => true;
        public static double FilterShortPlaylistsValue => 1000;
        public static bool KeepStreamOrder => true;
    }
}