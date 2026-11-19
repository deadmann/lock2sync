using System;
using System.Xml.Serialization;

namespace DirectoryLockToSync;

/// <summary>
/// User settings. Typed replacement for the 2008-2010 string flags ("1" / "0") and Path1..Path10.
/// Saved by <see cref="SettingsStore"/>. Everything starts off/empty, like the original defaults.
/// </summary>
public sealed class AppSettings
{
    public const int PathSlotCount = 10;

    private string[] _pathSlots = Normalize(null);

    /// <summary>Reopen the last folder when the program starts.</summary>
    public bool KeepLastAddress { get; set; }

    /// <summary>The folder that was loaded when the program last closed (normal, unlocked name).</summary>
    public string LastAddress { get; set; } = string.Empty;

    /// <summary>Ask "lock this folder first?" before unloading a folder or closing the program.</summary>
    public bool SecurityQuestion { get; set; }

    /// <summary>The info box of the main window lets the background picture show through.</summary>
    public bool TransparentInfo { get; set; }

    public SkinImageMode SkinImageMode { get; set; } = SkinImageMode.Tile;

    /// <summary>File name of the saved background picture inside the app-data folder; empty when there is none.</summary>
    public string SkinImageFile { get; set; } = string.Empty;

    public AppTheme Theme { get; set; } = AppTheme.LightBlue;

    /// <summary>Ask for a user name and password when the program starts.</summary>
    public bool LoginEnabled { get; set; }

    public string UserName { get; set; } = string.Empty;

    /// <summary>The salted hash of the password (see <see cref="PasswordHasher"/>); never the password itself.</summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>The 10 saved folders. Use <see cref="GetSlot"/> / <see cref="SetSlot"/> in code; this property exists for saving.</summary>
    [XmlArray("PathSlots")]
    [XmlArrayItem("Path")]
    public string[] PathSlots
    {
        get => _pathSlots;
        set => _pathSlots = Normalize(value);
    }

    /// <summary>Returns the saved folder of a slot (1 to 10), or an empty string when the slot is empty.</summary>
    public string GetSlot(int slot)
    {
        CheckSlot(slot);
        return _pathSlots[slot - 1];
    }

    /// <summary>
    /// Saves a folder into a slot (1 to 10). The normal (unlocked) name is stored, so the slot keeps working
    /// whether the folder is locked or not.
    /// </summary>
    public void SetSlot(int slot, string folderPath)
    {
        CheckSlot(slot);
        if (folderPath == null) throw new ArgumentNullException(nameof(folderPath));
        _pathSlots[slot - 1] = FolderLocker.GetUnlockedPath(folderPath);
    }

    public void ClearSlot(int slot)
    {
        CheckSlot(slot);
        _pathSlots[slot - 1] = string.Empty;
    }

    /// <summary>Turns the start-up login on with this user name and password (the password is stored as a hash).</summary>
    public void SetLogin(string userName, string password)
    {
        if (string.IsNullOrEmpty(userName)) throw new ArgumentException("The user name is empty.", nameof(userName));
        if (string.IsNullOrEmpty(password)) throw new ArgumentException("The password is empty.", nameof(password));

        UserName = userName;
        PasswordHash = PasswordHasher.Hash(password);
        LoginEnabled = true;
    }

    /// <summary>Turns the start-up login off and forgets the user name and password.</summary>
    public void ClearLogin()
    {
        LoginEnabled = false;
        UserName = string.Empty;
        PasswordHash = string.Empty;
    }

    /// <summary>True when the login is on and the user name (any letter case) and password (exact) are right.</summary>
    public bool CheckLogin(string userName, string password) =>
        LoginEnabled
        && string.Equals(UserName, userName, StringComparison.OrdinalIgnoreCase)
        && PasswordHasher.Verify(password, PasswordHash);

    /// <summary>True when this is the password that is stored now.</summary>
    public bool PasswordMatches(string password) => PasswordHasher.Verify(password, PasswordHash);

    private static void CheckSlot(int slot)
    {
        if (slot < 1 || slot > PathSlotCount)
            throw new ArgumentOutOfRangeException(nameof(slot), slot, "Slot must be between 1 and " + PathSlotCount + ".");
    }

    private static string[] Normalize(string[]? source)
    {
        var result = new string[PathSlotCount];
        for (var i = 0; i < PathSlotCount; i++)
        {
            result[i] = source != null && i < source.Length && source[i] != null ? source[i] : string.Empty;
        }

        return result;
    }
}
