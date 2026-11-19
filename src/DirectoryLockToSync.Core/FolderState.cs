namespace DirectoryLockToSync;

/// <summary>Where a folder currently is: under its normal name, under its locked name, or nowhere.</summary>
public enum FolderState
{
    Missing,
    Unlocked,
    Locked
}
