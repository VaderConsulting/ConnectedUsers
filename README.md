# ConnectedUsers

VB.NET WinForms tool that enumerates SMB sessions and open files on a chosen computer. `frmMain` ("Connections") calls `clsConnection.GetConnections` / `GetOpenFiles` (Netapi32 file and session enumeration) and shows computers, users, idle time, connect time, and open file paths in a tree. Radio buttons switch between computer and user grouping. Built for administrators checking who is connected to a share host.

**Source last updated:** 2004-08-17  
**Language:** VB.NET  
**Target:** .NET Framework (Visual Studio .NET 2003 solution format)  
**Output:** WinForms executable

## Solution structure

| Project | Language | Type | Purpose |
| --- | --- | --- | --- |
| `ConnectedUsers` | VB.NET | WinForms exe | Enumerate sessions and open files; tree UI on `frmMain` |

## How to open

Open `ConnectedUsers.sln` in Visual Studio .NET 2003 or Visual Studio 2005 to 2010 that still opens Format Version 8.00 solutions. `clsConnection.vb` and the `.vbproj` may appear as `.example` redacted copies in this tree.

## Requirements

- Visual Studio .NET 2003 or Visual Studio 2005 to 2010 (for Format Version 8.00)
- .NET Framework matching the project file
- Network rights to call Netapi32 session/file enum APIs on the target computer

## Attribution and provenance

Working copy from my Historical Dev folder.

## License

MIT. See `LICENSE`.
