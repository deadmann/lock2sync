using System;
using System.IO;
using Xunit;

namespace DirectoryLockToSync.Tests;

public class FolderSessionTests : IDisposable
{
    private readonly string _root = Path.Combine(Path.GetTempPath(), "dlts-session-" + Guid.NewGuid().ToString("N"));

    public FolderSessionTests() => Directory.CreateDirectory(_root);

    public void Dispose() => Directory.Delete(_root, recursive: true);

    private string MakeFolder(string name)
    {
        var path = Path.Combine(_root, name);
        Directory.CreateDirectory(path);
        return path;
    }

    [Fact]
    public void TryLoad_ReturnsFalse_ForMissingOrEmptyPaths()
    {
        // Arrange
        var session = new FolderSession();

        // Act
        var missing = session.TryLoad(Path.Combine(_root, "nope"));
        var empty = session.TryLoad("  ");

        // Assert
        Assert.False(missing);
        Assert.False(empty);
        Assert.False(session.IsLoaded);
    }

    [Fact]
    public void TryLoad_RemembersTheNormalName_EvenWhenGivenTheLockedPath()
    {
        // Arrange
        var folder = MakeFolder("secret.docs");
        var lockedPath = FolderLocker.Lock(folder);
        var session = new FolderSession();

        // Act
        var loaded = session.TryLoad(lockedPath);

        // Assert
        Assert.True(loaded);
        Assert.Equal(folder, session.BasePath);
        Assert.Equal(FolderState.Locked, session.State);
        Assert.Equal(lockedPath, session.CurrentPath);
    }

    [Fact]
    public void LockThenUnlock_MovesTheFolderAndTracksTheState()
    {
        // Arrange
        var folder = MakeFolder("photos");
        var session = new FolderSession();
        session.TryLoad(folder);

        // Act
        session.Lock();
        var afterLock = session.State;
        session.Unlock();
        var afterUnlock = session.State;

        // Assert
        Assert.Equal(FolderState.Locked, afterLock);
        Assert.Equal(FolderState.Unlocked, afterUnlock);
        Assert.True(Directory.Exists(folder));
        Assert.Equal(folder, session.CurrentPath);
    }

    [Fact]
    public void Lock_IsHarmless_WhenAlreadyLocked()
    {
        // Arrange
        var folder = MakeFolder("photos");
        var session = new FolderSession();
        session.TryLoad(folder);
        session.Lock();

        // Act
        session.Lock();

        // Assert
        Assert.Equal(FolderState.Locked, session.State);
    }

    [Fact]
    public void Lock_Throws_WhenTheFolderWasRemovedBehindOurBack()
    {
        // Arrange
        var folder = MakeFolder("photos");
        var session = new FolderSession();
        session.TryLoad(folder);
        Directory.Delete(folder);

        // Act & Assert
        Assert.Throws<DirectoryNotFoundException>(() => session.Lock());
        Assert.Equal(FolderState.Missing, session.State);
    }

    [Fact]
    public void Lock_Throws_WhenNothingIsLoaded()
    {
        // Arrange
        var session = new FolderSession();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => session.Lock());
        Assert.Throws<InvalidOperationException>(() => session.Unlock());
    }

    [Fact]
    public void Unload_ForgetsTheFolder_ButLeavesItOnDisk()
    {
        // Arrange
        var folder = MakeFolder("photos");
        var session = new FolderSession();
        session.TryLoad(folder);

        // Act
        session.Unload();

        // Assert
        Assert.False(session.IsLoaded);
        Assert.Null(session.CurrentPath);
        Assert.True(Directory.Exists(folder));
    }
}
