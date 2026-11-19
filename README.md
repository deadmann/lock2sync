# Directory Lock To Sync

[![CI](https://github.com/deadmann/lock2sync/actions/workflows/ci.yml/badge.svg)](https://github.com/deadmann/lock2sync/actions/workflows/ci.yml)

A small Windows utility to lock and unlock a folder.

Started in 2007 by Hassan Faghihi (first release, V1.0.0.0: 2007-03-03).
See [HISTORY.md](HISTORY.md) for what changed in each version and how the dates were found.

Download: [GitHub Releases](https://github.com/deadmann/lock2sync/releases) or
[SourceForge](https://sourceforge.net/projects/dirlock2sync/files/).

> **Note:** locking renames the folder with an Explorer special-folder suffix, which hides it
> from normal browsing. This is a convenience, not security: anyone can rename it back.

## Status

Being rewritten (2026). Progress is tracked in the [issues](https://github.com/deadmann/lock2sync/issues).

- `src/DirectoryLockToSync.Core`: the lock/unlock logic, typed settings and folder session, done and tested.
- `src/DirectoryLockToSync`: the Krypton main window, in the original 2008 design (browse, lock, unlock, open, unload, 10 saved folders).
  All five windows of the 2008 program are ported as designer forms: main window, Setting (skin picture, themes, security),
  password Confirm, About and the start-up password Login. The installer and CI are next.
- The background picture is copied to `%AppData%\DirectoryLockToSync` (the 2008 version copied it next to the program,
  which Windows no longer allows in Program Files, so it stopped working).
- Settings are stored in `%AppData%\DirectoryLockToSync\settings.xml`; `DirectoryLockToSync.exe --settings <file>`
  uses another file instead.
- The 2008-2010 source is not in the tree. It is kept in git history under the tag `legacy-2008-2010`
  (`git show legacy-2008-2010:"Lock To Sync/DLTS.cs"`), exactly as it was. That alone does not build:
  the Component Factory Krypton Toolkit it used is no longer distributed (see "Dependencies").
  The tag `legacy-2008-2010-buildable` adds that DLL and a path to it, so the old source can be rebuilt.

## Requirements to work on this project

| Tool | Why | Install |
|---|---|---|
| .NET SDK 10 | build and test | https://dotnet.microsoft.com/download |
| An editor | Visual Studio, Rider or VS Code (C# Dev Kit) | any |
| GitHub CLI (`gh`) | issues and project board | `winget install GitHub.cli`, then `gh auth login` |
| Inno Setup 6 | build the installer | `winget install JRSoftware.InnoSetup` |

The app is built for **.NET Framework 4.8** (preinstalled on Windows 10 1903+ and Windows 11,
nothing to install for users) and for **.NET 10**, from the same source.

## Dependencies and where they come from

| Package | Origin |
|---|---|
| Krypton.Toolkit | The "Component Factory Krypton Toolkit" used by the 2008 version (v3.0.8) was continued as the **Krypton Suite**: https://github.com/Krypton-Suite/Standard-Toolkit , NuGet package `Krypton.Toolkit`, BSD-3-Clause. This is the replacement for the old Component Factory SDK. |
| xUnit | unit tests |

Versions are pinned in `Directory.Packages.props` and locked in `packages.lock.json` files, so a
restore always gets exactly the same packages from NuGet.

The old Krypton 3.0.8 DLL (`ComponentFactory.Krypton.Toolkit.dll`, SHA-256
`8225CED36CC3F1EA7CFE918AD9D14B1E222EB0DB6432F76B4E0E1E7FE1C1EAF7`) is not in this repository, and nothing here depends on it: the NuGet package replaces it.
For reference only: it was extracted from the installer `LockToSync.msi` (inside the SourceForge
download `Lock-To-Sync-V1.1.0.0 Premium.zip`) with `msiexec /a`, on Windows 11 in November 2026.
That old installer is not guaranteed to keep working on future Windows versions, so do not rely on it.

## Build and test

Open `DirectoryLockToSync.slnx` in your editor, or from the command line:

    dotnet test DirectoryLockToSync.slnx

## Install (for users)

You need Windows 10 (version 1903 or later) or Windows 11. Nothing else has to be installed first.

1. Download `DirectoryLockToSync-Setup-<version>.exe` and double-click it.
2. Windows may show a blue **"Windows protected your PC"** window, because the installer is not signed with a paid
   certificate. This is normal. Click **More info**, then **Run anyway**.
3. Choose **Install for me only** (no administrator needed) or **for all users**, then click **Next** and **Install**.
4. Start it from the Start menu: **Directory Lock To Sync**.

To remove it: **Settings > Apps > Installed apps > Directory Lock To Sync > Uninstall**.
Your settings stay in `%AppData%\DirectoryLockToSync` (delete that folder if you want them gone).

> **Important:** locking a folder only renames it, so uninstalling the program does **not** unlock your folders.
> Unlock them before you uninstall (the uninstaller reminds you).

## Build the installer (for developers)

1. Install the .NET SDK 10 and Inno Setup 6. In a terminal: `winget install JRSoftware.InnoSetup`.
2. From the repository folder run:

       powershell -ExecutionPolicy Bypass -File installer\build-installer.ps1

3. The installer appears in `artifacts\installer\DirectoryLockToSync-Setup-<version>.exe` (about 5.5 MB).
   The version number comes from `<Version>` in `src\DirectoryLockToSync\DirectoryLockToSync.csproj`.

The installer script is `installer\DirectoryLockToSync.iss`. Do not change its `AppId`: Windows uses it to upgrade an
earlier install in place.

## Publish a release (for the maintainer)

A release is a version tag plus the installer attached to it, shown on the GitHub **Releases** page. Anyone can download
from there without an account.

### The automatic way (use this one)

GitHub builds the installer for you (workflow `.github/workflows/release.yml`):

1. Set the version in `src\DirectoryLockToSync\DirectoryLockToSync.csproj` (`<Version>`), commit it and push it to `main`.
   Wait for the **CI** check on the commit to turn green (it builds and tests both targets).
2. Create the tag and push it. The tag must be `v` plus the version, for example `v4.0.1`:

       git tag v4.0.1
       git push origin v4.0.1

3. After a few minutes a **draft** release appears under **Releases**, with `DirectoryLockToSync-Setup-<version>.exe`
   and its `.sha256` file already attached and the notes (including the checksum) filled in. Nothing is public yet.
   Open the draft, read it, tick **pre-release** if it is only a test, and click **Publish release**.

A tag with a dash, like `v4.1.0-beta1`, is marked as a pre-release automatically.
If the tag does not match the project version, the run stops with a clear message and publishes nothing: delete the tag
(`git push origin :refs/tags/v4.0.1` and `git tag -d v4.0.1`), fix the version and tag again.

### The manual way (if GitHub Actions is not available)

Do these steps in order:

1. Set the version in `src\DirectoryLockToSync\DirectoryLockToSync.csproj` (`<Version>`), commit it and push it to `main`.
2. Build the installer: `powershell -ExecutionPolicy Bypass -File installer\build-installer.ps1`.
3. Write down its checksum, so people can check the download was not altered (it is not signed):

       Get-FileHash artifacts\installer\DirectoryLockToSync-Setup-<version>.exe -Algorithm SHA256

4. Create the release (needs the GitHub CLI, logged in with `gh auth login`). The tag is created from the pushed `main`:

       gh release create v<version> artifacts\installer\DirectoryLockToSync-Setup-<version>.exe --repo deadmann/lock2sync --title "Directory Lock To Sync <version>" --prerelease --notes-file release-notes.md

   `--prerelease` marks a test release; leave it out (or untick "pre-release" on the GitHub page) for the official one.
5. In the notes say what changed, that Windows 10 (1903+) or 11 is needed, how to get past the SmartScreen warning
   (**More info > Run anyway**), and paste the SHA-256 checksum.

A published release is public. If a release is wrong, delete its tag and release on GitHub and publish a new version
number instead of replacing the file.

### SourceForge (automatic)

When you click **Publish release** on GitHub, the workflow `.github/workflows/publish-sourceforge.yml` copies the
installer and its `.sha256` file to the project's Files area on SourceForge
(`Files / Directory Lock To Sync / <version> /`) and makes it the default download button.
Pre-releases are not copied.

How it works, and the one-time setup (already done for this repository):

- GitHub logs in to SourceForge with a **deploy key**. This is an SSH key made only for this job. Its public half is
  saved in the SourceForge account under **Me > Account Settings > SSH Settings**. Its private half is stored in the
  repository secret `SF_SSH_KEY` (**Settings > Secrets and variables > Actions**). Secrets are hidden: nobody can read them back.
- The secret `SF_API_KEY` is the SourceForge **Releases API Key** (**Account Settings > Preferences**). It is only used to
  set the default download.
- If a key is lost or leaked, make a new one, replace it in both places, and remove the old one.

If the upload fails, open **Actions > Publish to SourceForge > Run workflow**, type the tag (for example `v4.0.1`)
and run it again.

## User interface

Every window is a normal Windows Forms designer form: `Name.cs` has the behaviour, `Name.Designer.cs` holds the layout
(`InitializeComponent`), and `Name.resx` the resources. Open them in the Visual Studio or Rider designer to move or add
controls. The layout was taken from the original 2008 designer files, so control names (`btnLock`, `DrpBtnPath`,
`groupBox1`, ...) match the old project.

## Contributing

Every change is linked to a GitHub issue. Commit messages look like:

    #12: Core - FolderLocker: Add Lock and Unlock with tests

Bug reports and pull requests are welcome. Nothing is promised to be merged.

## License

GPL-3.0-or-later, see [LICENSE](LICENSE). Copyright (C) 2007-2026 Hassan Faghihi.
You may use and change it freely; if you distribute a changed version, you must
publish its source under the same license.
