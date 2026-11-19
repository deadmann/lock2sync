using System;
using System.IO;
using System.Xml.Serialization;

namespace DirectoryLockToSync;

/// <summary>Loads and saves <see cref="AppSettings"/> as an XML file. Works the same on .NET Framework 4.8 and .NET 10.</summary>
public sealed class SettingsStore
{
    private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(AppSettings));

    public SettingsStore(string filePath)
    {
        FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
    }

    /// <summary>%AppData%\DirectoryLockToSync\settings.xml</summary>
    public static string DefaultFilePath =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DirectoryLockToSync", "settings.xml");

    public string FilePath { get; }

    /// <summary>Returns the saved settings, or fresh defaults when the file is missing or cannot be read.</summary>
    public AppSettings Load()
    {
        try
        {
            if (!File.Exists(FilePath)) return new AppSettings();

            using (var stream = File.OpenRead(FilePath))
            {
                return Serializer.Deserialize(stream) as AppSettings ?? new AppSettings();
            }
        }
        catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is InvalidOperationException)
        {
            return new AppSettings();
        }
    }

    /// <summary>Writes the settings; a half-written file never replaces a good one.</summary>
    public void Save(AppSettings settings)
    {
        if (settings == null) throw new ArgumentNullException(nameof(settings));

        var directory = Path.GetDirectoryName(FilePath);
        if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);

        var temp = FilePath + ".tmp";
        using (var stream = File.Create(temp))
        {
            Serializer.Serialize(stream, settings);
        }

        File.Copy(temp, FilePath, true);
        File.Delete(temp);
    }
}
