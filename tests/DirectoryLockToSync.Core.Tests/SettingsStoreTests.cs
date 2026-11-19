using System;
using System.IO;
using Xunit;

namespace DirectoryLockToSync.Tests;

public class SettingsStoreTests : IDisposable
{
    private readonly string _root = Path.Combine(Path.GetTempPath(), "dlts-settings-" + Guid.NewGuid().ToString("N"));

    public void Dispose()
    {
        if (Directory.Exists(_root)) Directory.Delete(_root, recursive: true);
    }

    [Fact]
    public void Load_ReturnsDefaults_WhenFileDoesNotExist()
    {
        // Arrange
        var store = new SettingsStore(Path.Combine(_root, "settings.xml"));

        // Act
        var settings = store.Load();

        // Assert
        Assert.False(settings.KeepLastAddress);
        Assert.Equal(string.Empty, settings.LastAddress);
    }

    [Fact]
    public void SaveThenLoad_KeepsEverything()
    {
        // Arrange
        var store = new SettingsStore(Path.Combine(_root, "sub", "settings.xml"));
        var settings = new AppSettings
        {
            KeepLastAddress = true,
            SecurityQuestion = true,
            LastAddress = @"C:\Data\my.photos"
        };
        settings.SetSlot(1, @"C:\One");
        settings.SetSlot(10, @"D:\Ten");

        // Act
        store.Save(settings);
        var loaded = store.Load();

        // Assert
        Assert.True(loaded.KeepLastAddress);
        Assert.True(loaded.SecurityQuestion);
        Assert.Equal(@"C:\Data\my.photos", loaded.LastAddress);
        Assert.Equal(@"C:\One", loaded.GetSlot(1));
        Assert.Equal(string.Empty, loaded.GetSlot(5));
        Assert.Equal(@"D:\Ten", loaded.GetSlot(10));
        Assert.False(File.Exists(store.FilePath + ".tmp"));
    }

    [Fact]
    public void Load_ReturnsDefaults_WhenFileIsCorrupt()
    {
        // Arrange
        Directory.CreateDirectory(_root);
        var path = Path.Combine(_root, "settings.xml");
        File.WriteAllText(path, "this is not xml <<<");
        var store = new SettingsStore(path);

        // Act
        var settings = store.Load();

        // Assert
        Assert.False(settings.SecurityQuestion);
        Assert.Equal(AppSettings.PathSlotCount, settings.PathSlots.Length);
    }

    [Fact]
    public void Save_OverwritesAnExistingFile()
    {
        // Arrange
        var store = new SettingsStore(Path.Combine(_root, "settings.xml"));
        store.Save(new AppSettings { LastAddress = @"C:\Old" });

        // Act
        store.Save(new AppSettings { LastAddress = @"C:\New" });
        var loaded = store.Load();

        // Assert
        Assert.Equal(@"C:\New", loaded.LastAddress);
    }
}
