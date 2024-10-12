namespace IsoContainerPlayback.DebugApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dirSvc = new IsoDirectoryService(new LogManager());
            var fileSvc = new IsoFileService(new LogManager());

            var entries = dirSvc.Get(new GetIsoDirectory() { }) as List<IsoDirectoryEntryInfo>;

            var bdmvEntry = entries?.FirstOrDefault(e => e.Name.StartsWith("BDMV") && e.IsDirectory);

            var bdmvEntries = dirSvc.Get(new GetIsoDirectory() { DirectoryPath = bdmvEntry.FullName }) as List<IsoDirectoryEntryInfo>;

            var streamEntry = bdmvEntries?.FirstOrDefault(e => e.Name.StartsWith("STREAM") && e.IsDirectory);

            var streamEntries = dirSvc.Get(new GetIsoDirectory() { DirectoryPath = streamEntry.FullName }) as List<IsoDirectoryEntryInfo>;

            var m2tsStream = fileSvc.Get(new GetIsoFile() { Filename = streamEntries.First().FullName }) as Stream;

            m2tsStream.Dispose();
        }
    }
}