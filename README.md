# IsoContainerPlayback plugin for Emby
The **IsoContainerPlayback** plugin will hopefully allow for **Emby** to play videos that are stored within ISO images of BluRay and DVD discs.

**Note:** At this time this plugin is incomplete, and cannot operate without potential support within **Emby** server.

## Projects
The solution consists of the following projects:

- **IsoContainerPlayback**
- **IsoContainerPlayback.DebugApp**
- **IsoContainerPlayback.Tests**

### IsoContainerPlayback
This is the **Emby** plugin itself. It consists of API endpoints to allow Emby to retrieve directory listings from an ISO, or open a `Stream` to a file within an ISO.

Any files opened from within an ISO are wrapped in an `OnDisposeStream`, which ensures that underlying objects are also disposed of when a stream is disposed.

**Note:** The plugin is still work-in-progress. The plugin will correctly register itself with **Emby** server, but the API endpoints may still need further work.

This project also includes the source to any required 3rd-party libraries, as plugins can only access framework libraries and the **Emby** SDK.

### IsoContainerPlayback.DebugApp
A basic console app, used for test and debugging purposes.

### IsoContainerPlayback.Tests
Currently empty, but will contain unit tests once the plugin is developed further.

## Acknowledgements
This project is made possible through the use of the following open source projects.

### DiscUtils
[Project Homepage](https://github.com/DiscUtils/DiscUtils)

**DiscUtils** is a .NET library to read and write ISO files and Virtual Machine disk files (VHD, VDI, XVA, VMDK, etc). **DiscUtils** is developed in C# with no native code (or P/Invoke).
