using System;
using System.IO;

namespace DirectoryLockToSync;

/// <summary>
/// Keeps the background picture the user chose in a folder the program can always write to
/// (%AppData%\DirectoryLockToSync). The 2008 version copied it next to the program (Directory.GetCurrentDirectory),
/// which fails in Program Files and moves whenever a file dialog is used, so the picture could not be shown.
/// </summary>
public sealed class SkinImageStore
{
    private const string BaseName = "background";

    public SkinImageStore(string folderPath)
    {
        FolderPath = folderPath ?? throw new ArgumentNullException(nameof(folderPath));
    }

    /// <summary>The same folder the settings file lives in.</summary>
    public static string DefaultFolderPath => Path.GetDirectoryName(SettingsStore.DefaultFilePath)!;

    public string FolderPath { get; }

    /// <summary>Copies the chosen picture into the folder and returns its file name (to be saved in the settings).</summary>
    public string Import(string sourcePath)
    {
        if (!File.Exists(sourcePath)) throw new FileNotFoundException("The picture does not exist.", sourcePath);

        Directory.CreateDirectory(FolderPath);
        var fileName = BaseName + Path.GetExtension(sourcePath).ToLowerInvariant();
        var target = Path.Combine(FolderPath, fileName);

        if (!string.Equals(Path.GetFullPath(sourcePath), Path.GetFullPath(target), StringComparison.OrdinalIgnoreCase))
        {
            File.Copy(sourcePath, target, true);
        }

        // Only one background picture is kept: remove an older one with a different extension.
        foreach (var other in Directory.GetFiles(FolderPath, BaseName + ".*"))
        {
            if (!string.Equals(Path.GetFileName(other), fileName, StringComparison.OrdinalIgnoreCase)) File.Delete(other);
        }

        return fileName;
    }

    /// <summary>The full path of a saved picture, or null when there is none (or it was deleted).</summary>
    public string? GetPath(string fileName)
    {
        if (string.IsNullOrEmpty(fileName)) return null;

        var path = Path.Combine(FolderPath, Path.GetFileName(fileName));
        return File.Exists(path) ? path : null;
    }

    /// <summary>Deletes a saved picture. Does nothing if it is already gone.</summary>
    public void Remove(string fileName)
    {
        var path = GetPath(fileName);
        if (path != null) File.Delete(path);
    }
}
