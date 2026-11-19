# Directory Lock To Sync - Project Memory

## Decisions (agreed)
- Source of truth: D:\Projects\2008\Lock-To-Sync-V1.1.0.0 Premium (never edited; this repo is a replica).
- Target: dual build net48 + net10.0(-windows) from one SDK-style source; WinForms + Krypton.Toolkit 110.26.11.306 (BSD-3, NuGet).
  net48 is the main download (preinstalled on Win10 1903+/11, no runtime prompt); net10 optional. Next version: 4.0.0.
  Keep a copy of the Krypton package + packages.lock.json in the repo so the build can never be lost again.
  The old Krypton 3.0.8 DLL is inside the old Setup1\Debug\LockToSync.msi (recoverable via msiexec /a).
  Goal: premium look, GitHub-ready, installable. Avoid C# features missing on net48 (ranges, etc.) in shared code.
  Krypton is WinForms-only; WPF libraries (MahApps, WPF-UI) look different. A WPF version may come later.
- Name: "Directory Lock To Sync"; drop "Premium" (the plain WinForms source is lost). GitHub repo (empty, created by the author): https://github.com/deadmann/lock2sync - the repo keeps the short name lock2sync, the product name stays "Directory Lock To Sync".
- Never use "Tak System" (it never existed as a company). Publisher/author: Hassan Faghihi.
- Step 1 = keep current behaviour exactly. Improvements below are deferred.
- Git history: root commit 2007-03-03 (README/HISTORY), source import 2008-11-22, then new work with real dates.
  Dates are backed by evidence (see HISTORY.md); do not rewrite them.

## Facts about the original
- Lock = Directory.Move(path, path + ".{F5175861-2688-11D0-9C5E-00AA00A45957}"); unlock = rename back. Obfuscation, not security.
- Forms: KryptonDLTS (main), KryptonSetting, KryptonInput, KryptonAbout, Login; plain twins: DLTS, Setting, Input, About.
- Settings are string flags ("1"/"0") via Properties.Settings (Image, Imagef, Transparent, Active, SecurityQuestion).
- Krypton 3.0.8 (2008) DLL is lost; the project had no HintPath.
- AssemblyInfo.cs still says AssemblyCompany "Tak-System (None)" -> replace with the author's name when porting.

## Dating evidence
- V1.0.0.0 (SourceForge, Normal Ver): PE compile timestamp 2007-03-03 09:16 UTC; strings "2007 By Hassan Faghihi"; zip entries dated 2007-03-01..03.
- SourceForge upload dates (2010-05-17 / 2010-06-04) are one batch re-upload, not real dates.
- SourceForge: https://sourceforge.net/projects/dirlock2sync/ ; Normal Ver folder has V1.0.0.0, V2.0.0.4, V2.1.0.0, V2.2.0.0, V3.0.0.0, V3.1.0.0.
  V2.0.0.4 / V2.1.0.0 (2.8 MB) not yet checked for source; V2.2+/V3.x (14 MB) probably installers.
- Original source files: Settings.cs 2008-11-22 (oldest code), project key 2008-11-25, last edits 2010-04-27, copyright "2008-2010".

## Deferred ideas
1. Lock method: add NTFS ACL deny (keeps rename compat) -> later real encryption with password (new format).
2. Replace string "1"/"0" settings with typed bool settings; drop the Login form if unused.
3. Merge each Krypton/plain form pair into one (plain twins are dead code).
4. Replace empty catch{} with real error messages (why lock failed: in use, no rights, path too long).
5. Drag & drop a folder onto the window; remember last folders; command-line / Explorer context menu.
6. Installer: MSIX or WiX/Inno Setup instead of the 2008 vdproj + WinInstaller 3.1 prerequisites; optional winget manifest.
7. GitHub: repo, README with screenshots, LICENSE, GitHub Actions build+release, versioned tags.
8. Optional WPF version later.
9. DONE in step 1: lock/unlock logic is in DirectoryLockToSync.Core (FolderLocker) with tests on net48 + net10.0.
   (The original Split('.') logic is awkward but does not break on dotted folder names; earlier note was wrong.)
10. New signing: the old temporary .pfx was dropped; use a fresh certificate if signing is wanted.
11. Check the V2.0.0.4 / V2.1.0.0 zips for source and dates; if present, add per-version commits at their true dates.
