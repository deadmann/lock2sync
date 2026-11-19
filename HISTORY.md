# History and changes

## Version 4.0.0 (2026)

A full rewrite. The look and the way it works are the same as the 2008 program; the inside is new.

- **New:** a real installer (`Setup.exe`). Install for yourself without administrator rights, or for all users.
- **New:** the program runs on .NET Framework 4.8, which is already part of Windows 10 (1903 or later) and Windows 11.
- **New:** the source code is published for the first time, under the GPL-3.0 license.
- **Fixed:** the background picture works again. The old version saved it next to the program, and Windows
  no longer allows that inside Program Files. It is now saved in `%AppData%\DirectoryLockToSync`.
- **Fixed:** the password is no longer stored as plain text. It is stored as a salted hash.
- **Changed:** settings are kept in `%AppData%\DirectoryLockToSync\settings.xml`.
- **Changed:** the Krypton toolkit now comes from the maintained Krypton Suite package (the old Component Factory
  toolkit is not available any more).
- **Tests:** the lock and unlock logic, the settings and the password check are covered by automatic tests.

Remember: locking a folder only renames it. It hides the folder from normal browsing, but it is not strong security.

## Older versions (2007 to 2010)

These were released as programs only, without source code. The source of the 1.1.0.0 Premium version is kept
in this repository under the tag `legacy-2008-2010`.

| Version | Date | How the date was found |
|---|---|---|
| 1.0.0.0 | 2007-03-03 | Compile date stored inside the program file; program only |
| 2.0.0.4 | not checked yet | |
| 2.1.0.0 | not checked yet | |
| 2.2.0.0 | not checked yet | |
| 3.0.0.0 | not checked yet | |
| 3.1.0.0 | not checked yet | |
| 1.1.0.0 Premium | 2010-06-04 (upload date) | The only version whose source code survives; its files are dated 2008 to 2010 |

The upload dates on SourceForge are not the real dates, because all the old files were uploaded together in 2010.
The real dates come from the files themselves (the compile date inside the program and the dates inside the zip files).

## Where to download

- GitHub: https://github.com/deadmann/lock2sync/releases
- SourceForge: https://sourceforge.net/projects/dirlock2sync/ (project created 2010-05-17)
