namespace IsoContainerPlayback.DebugApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var isoSvc = new IsoContainerPlaybackService(new LogManager());

            var entries = isoSvc.Get(new GetIsoDirectory() { }) as List<IsoDirectoryEntryInfo>;

            var bdmvEntry = entries?.FirstOrDefault(e => e.Name.StartsWith("BDMV") && e.IsDirectory);

            var bdmvEntries = isoSvc.Get(new GetIsoDirectory() { DirectoryPath = bdmvEntry.FullName }) as List<IsoDirectoryEntryInfo>;

            var streamEntry = bdmvEntries?.FirstOrDefault(e => e.Name.StartsWith("STREAM") && e.IsDirectory);

            var streamEntries = isoSvc.Get(new GetIsoDirectory() { DirectoryPath = streamEntry.FullName }) as List<IsoDirectoryEntryInfo>;

            var m2tsStream = isoSvc.Get(new GetIsoFile() { Filename = streamEntries.First().FullName }) as Stream;

            m2tsStream.Dispose();
        }
    }
}