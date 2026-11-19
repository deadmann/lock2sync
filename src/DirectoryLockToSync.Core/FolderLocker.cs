using System;
using System.IO;

namespace DirectoryLockToSync;

/// <summary>
/// Locks a folder by renaming it with the Explorer CLSID suffix and unlocks it by removing the suffix.
/// This is the same behaviour as the 2007-2010 versions; it hides the folder from Explorer, it is not security.
/// </summary>
public static class FolderLocker
{
    public const string LockSuffix = ".{F5175861-2688-11D0-9C5E-00AA00A45957}";

    public static bool IsLocked(string folderPath) =>
        folderPath.EndsWith(LockSuffix, StringComparison.OrdinalIgnoreCase);

    public static string GetLockedPath(string folderPath) =>
        IsLocked(folderPath) ? folderPath : folderPath + LockSuffix;

    public static string GetUnlockedPath(string folderPath) =>
        IsLocked(folderPath) ? folderPath.Substring(0, folderPath.Length - LockSuffix.Length) : folderPath;

    /// <summary>Tells whether the folder exists under its normal name, under its locked name, or not at all.</summary>
    public static FolderState GetState(string folderPath)
    {
        if (Directory.Exists(GetUnlockedPath(folderPath))) return FolderState.Unlocked;
        if (Directory.Exists(GetLockedPath(folderPath))) return FolderState.Locked;
        return FolderState.Missing;
    }

    /// <summary>Renames the folder to its locked name and returns the new path. Does nothing if already locked.</summary>
    public static string Lock(string folderPath)
    {
        var target = GetLockedPath(folderPath);
        if (target != folderPath) Directory.Move(folderPath, target);
        return target;
    }

    /// <summary>Renames the folder back to its normal name and returns the new path. Does nothing if not locked.</summary>
    public static string Unlock(string folderPath)
    {
        var target = GetUnlockedPath(folderPath);
        if (target != folderPath) Directory.Move(folderPath, target);
        return target;
    }
}
