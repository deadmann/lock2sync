using System;
using System.IO;
using Xunit;

namespace DirectoryLockToSync.Tests;

public class SkinImageStoreTests : IDisposable
{
    private readonly string _root = Path.Combine(Path.GetTempPath(), "dlts-skin-" + Guid.NewGuid().ToString("N"));

    public SkinImageStoreTests() => Directory.CreateDirectory(_root);

    public void Dispose() => Directory.Delete(_root, recursive: true);

    private string MakePicture(string name, string content = "picture")
    {
        var path = Path.Combine(_root, name);
        File.WriteAllText(path, content);
        return path;
    }

    [Fact]
    public void Import_CopiesThePictureIntoTheStoreFolder_AndReturnsItsName()
    {
        // Arrange
        var source = MakePicture("holiday.JPG", "sea");
        var store = new SkinImageStore(Path.Combine(_root, "data"));

        // Act
        var fileName = store.Import(source);

        // Assert
        Assert.Equal("background.jpg", fileName);
        Assert.Equal("sea", File.ReadAllText(store.GetPath(fileName)!));
        Assert.True(File.Exists(source));
    }

    [Fact]
    public void Import_ReplacesTheOldPicture_EvenWhenItHasAnotherExtension()
    {
        // Arrange
        var store = new SkinImageStore(Path.Combine(_root, "data"));
        var oldName = store.Import(MakePicture("one.png", "old"));

        // Act
        var newName = store.Import(MakePicture("two.jpg", "new"));

        // Assert
        Assert.Null(store.GetPath(oldName));
        Assert.Equal("new", File.ReadAllText(store.GetPath(newName)!));
    }

    [Fact]
    public void Import_CanBeRepeatedWithTheStoredPictureItself()
    {
        // Arrange
        var store = new SkinImageStore(Path.Combine(_root, "data"));
        var name = store.Import(MakePicture("one.jpg", "same"));

        // Act
        var again = store.Import(store.GetPath(name)!);

        // Assert
        Assert.Equal(name, again);
        Assert.Equal("same", File.ReadAllText(store.GetPath(again)!));
    }

    [Fact]
    public void Import_Throws_WhenThePictureDoesNotExist()
    {
        // Arrange
        var store = new SkinImageStore(Path.Combine(_root, "data"));

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => store.Import(Path.Combine(_root, "nope.jpg")));
    }

    [Fact]
    public void GetPath_ReturnsNull_ForEmptyNamesMissingFilesAndFolderTricks()
    {
        // Arrange
        var store = new SkinImageStore(Path.Combine(_root, "data"));
        MakePicture("secret.txt");

        // Act
        var empty = store.GetPath(string.Empty);
        var missing = store.GetPath("background.jpg");
        var trick = store.GetPath(@"..\secret.txt");

        // Assert
        Assert.Null(empty);
        Assert.Null(missing);
        Assert.Null(trick);
    }

    [Fact]
    public void Remove_DeletesThePicture_AndIsHarmlessTheSecondTime()
    {
        // Arrange
        var store = new SkinImageStore(Path.Combine(_root, "data"));
        var name = store.Import(MakePicture("one.jpg"));

        // Act
        store.Remove(name);
        store.Remove(name);

        // Assert
        Assert.Null(store.GetPath(name));
    }
}
