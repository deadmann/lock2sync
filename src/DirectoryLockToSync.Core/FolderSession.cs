using System;
using System.IO;

namespace DirectoryLockToSync;

/// <summary>
/// The folder the program currently has loaded and what can be done with it.
/// The folder is remembered by its normal (unlocked) name; its real state is looked up each time,
/// so it keeps working if the folder is locked, unlocked or removed outside the program.
/// </summary>
public sealed class FolderSession
{
    /// <summary>The loaded folder by its normal name, or null when nothing is loaded.</summary>
    public string? BasePath { get; private set; }

    public bool IsLoaded => BasePath != null;

    /// <summary>The state of the loaded folder right now (Missing when nothing is loaded).</summary>
    public FolderState State => BasePath == null ? FolderState.Missing : FolderLocker.GetState(BasePath);

    /// <summary>The path the folder has on disk right now (locked or normal name).</summary>
    public string? CurrentPath =>
        BasePath == null ? null : (State == FolderState.Locked ? FolderLocker.GetLockedPath(BasePath) : BasePath);

    /// <summary>Loads a folder given by its normal or locked name. Returns false when it does not exist.</summary>
    public bool TryLoad(string folderPath)
    {
        if (string.IsNullOrWhiteSpace(folderPath)) return false;
        if (FolderLocker.GetState(folderPath) == FolderState.Missing) return false;

        BasePath = FolderLocker.GetUnlockedPath(folderPath);
        return true;
    }

    /// <summary>Locks the loaded folder. Does nothing if it is already locked.</summary>
    public void Lock()
    {
        var path = RequirePath();
        var state = State;
        if (state == FolderState.Missing) throw new DirectoryNotFoundException("The folder no longer exists: " + path);
        if (state == FolderState.Unlocked) FolderLocker.Lock(path);
    }

    /// <summary>Unlocks the loaded folder. Does nothing if it is not locked.</summary>
    public void Unlock()
    {
        var path = RequirePath();
        var state = State;
        if (state == FolderState.Missing) throw new DirectoryNotFoundException("The folder no longer exists: " + path);
        if (state == FolderState.Locked) FolderLocker.Unlock(FolderLocker.GetLockedPath(path));
    }

    /// <summary>Forgets the loaded folder (the folder itself is not touched).</summary>
    public void Unload() => BasePath = null;

    private string RequirePath() =>
        BasePath ?? throw new InvalidOperationException("No folder is loaded.");
}
