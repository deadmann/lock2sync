using System;
using System.IO;
using Xunit;

namespace DirectoryLockToSync.Tests;

public class FolderLockerTests : IDisposable
{
    private readonly string _root = Path.Combine(Path.GetTempPath(), "dlts-" + Guid.NewGuid().ToString("N"));

    public FolderLockerTests() => Directory.CreateDirectory(_root);

    public void Dispose() => Directory.Delete(_root, recursive: true);

    [Theory]
    [InlineData(@"C:\Data\Photos", false)]
    [InlineData(@"C:\Data\Photos.{F5175861-2688-11D0-9C5E-00AA00A45957}", true)]
    [InlineData(@"C:\Data\Photos.{f5175861-2688-11d0-9c5e-00aa00a45957}", true)]
    [InlineData(@"C:\v1.2\Photos", false)]
    public void IsLocked_ReturnsExpected(string path, bool expected)
    {
        // Arrange
        // Act
        var result = FolderLocker.IsLocked(path);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetUnlockedPath_KeepsDotsInTheFolderName()
    {
        // Arrange
        var locked = @"C:\Data\my.photos.2024" + FolderLocker.LockSuffix;

        // Act
        var result = FolderLocker.GetUnlockedPath(locked);

        // Assert
        Assert.Equal(@"C:\Data\my.photos.2024", result);
    }

    [Fact]
    public void Lock_RenamesFolderWithSuffix()
    {
        // Arrange
        var folder = Path.Combine(_root, "secret.docs");
        Directory.CreateDirectory(folder);

        // Act
        var lockedPath = FolderLocker.Lock(folder);

        // Assert
        Assert.Equal(folder + FolderLocker.LockSuffix, lockedPath);
        Assert.True(Directory.Exists(lockedPath));
        Assert.False(Directory.Exists(folder));
    }

    [Fact]
    public void Unlock_RestoresOriginalName()
    {
        // Arrange
        var folder = Path.Combine(_root, "secret.docs");
        Directory.CreateDirectory(folder);
        var lockedPath = FolderLocker.Lock(folder);

        // Act
        var unlockedPath = FolderLocker.Unlock(lockedPath);

        // Assert
        Assert.Equal(folder, unlockedPath);
        Assert.True(Directory.Exists(folder));
    }

    [Fact]
    public void Lock_ThrowsWhenFolderDoesNotExist()
    {
        // Arrange
        var missing = Path.Combine(_root, "missing");

        // Act & Assert
        Assert.Throws<DirectoryNotFoundException>(() => FolderLocker.Lock(missing));
    }

    [Fact]
    public void GetState_ReturnsUnlocked_WhenFolderHasNormalName()
    {
        // Arrange
        var folder = Path.Combine(_root, "photos");
        Directory.CreateDirectory(folder);

        // Act
        var state = FolderLocker.GetState(folder);

        // Assert
        Assert.Equal(FolderState.Unlocked, state);
    }

    [Fact]
    public void GetState_ReturnsLocked_ForBothTheNormalAndTheLockedPath()
    {
        // Arrange
        var folder = Path.Combine(_root, "photos");
        Directory.CreateDirectory(folder);
        var lockedPath = FolderLocker.Lock(folder);

        // Act
        var fromNormalPath = FolderLocker.GetState(folder);
        var fromLockedPath = FolderLocker.GetState(lockedPath);

        // Assert
        Assert.Equal(FolderState.Locked, fromNormalPath);
        Assert.Equal(FolderState.Locked, fromLockedPath);
    }

    [Fact]
    public void GetState_ReturnsMissing_WhenNeitherNameExists()
    {
        // Arrange
        var missing = Path.Combine(_root, "nothing-here");

        // Act
        var state = FolderLocker.GetState(missing);

        // Assert
        Assert.Equal(FolderState.Missing, state);
    }
}
