# Directory Lock To Sync

A small Windows utility to lock and unlock a folder.

Started in 2007 by Hassan Faghihi (first release, V1.0.0.0: 2007-03-03).
See [HISTORY.md](HISTORY.md) for the version history and how the dates were established.

> **Note:** locking renames the folder with an Explorer special-folder suffix, which hides it
> from normal browsing. This is a convenience, not security: anyone can rename it back.

## Status

Being rewritten (2026). Progress is tracked in the [issues](https://github.com/deadmann/lock2sync/issues).

- `src/DirectoryLockToSync.Core`: the lock/unlock logic, done and tested.
- The Krypton user interface, installer and CI are planned for v4.0.0.
- `Lock To Sync/` and `Lock To Sync.sln` are the 2008-2010 source, kept as a reference only.
  They do not build: the Component Factory Krypton Toolkit they used is no longer distributed
  (see "Dependencies").

## Requirements to work on this project

| Tool | Why | Install |
|---|---|---|
| .NET SDK 10 | build and test | https://dotnet.microsoft.com/download |
| An editor | Visual Studio, Rider or VS Code (C# Dev Kit) | any |
| GitHub CLI (`gh`) | issues and project board | `winget install GitHub.cli`, then `gh auth login` |

The app is built for **.NET Framework 4.8** (preinstalled on Windows 10 1903+ and Windows 11,
nothing to install for users) and for **.NET 10**, from the same source.

## Dependencies and where they come from

| Package | Origin |
|---|---|
| Krypton.Toolkit | The "Component Factory Krypton Toolkit" used by the 2008 version (v3.0.8) was continued as the **Krypton Suite**: https://github.com/Krypton-Suite/Standard-Toolkit , NuGet package `Krypton.Toolkit`, BSD-3-Clause. This is the replacement for the old Component Factory SDK. |
| xUnit | unit tests |

The old Krypton 3.0.8 DLL is inside the 2008 installer (`LockToSync.msi`, in the SourceForge
download). A pinned copy of the Krypton package will be kept in `vendor/` so the build can
never be lost again.

## Build and test

Open `DirectoryLockToSync.slnx` in your editor, or from the command line:

    dotnet test DirectoryLockToSync.slnx

`Lock To Sync.sln` is the old 2008 solution; ignore it.

## Contributing

Every change is linked to a GitHub issue. Commit messages look like:

    #12: Core - FolderLocker: Add Lock and Unlock with tests

Bug reports and pull requests are welcome. Nothing is promised to be merged.

## License

GPL-3.0-or-later, see [LICENSE](LICENSE). Copyright (C) 2007-2026 Hassan Faghihi.
You may use and change it freely; if you distribute a changed version, you must
publish its source under the same license.
