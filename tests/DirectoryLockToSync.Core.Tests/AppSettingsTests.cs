using System;
using Xunit;

namespace DirectoryLockToSync.Tests;

public class AppSettingsTests
{
    [Fact]
    public void NewSettings_StartOffAndEmpty()
    {
        // Arrange
        // Act
        var settings = new AppSettings();

        // Assert
        Assert.False(settings.KeepLastAddress);
        Assert.False(settings.SecurityQuestion);
        Assert.Equal(string.Empty, settings.LastAddress);
        Assert.Equal(AppSettings.PathSlotCount, settings.PathSlots.Length);
        Assert.All(settings.PathSlots, slot => Assert.Equal(string.Empty, slot));
    }

    [Fact]
    public void SetSlot_StoresTheUnlockedName_EvenWhenGivenTheLockedPath()
    {
        // Arrange
        var settings = new AppSettings();
        var lockedPath = @"C:\Data\Photos" + FolderLocker.LockSuffix;

        // Act
        settings.SetSlot(3, lockedPath);

        // Assert
        Assert.Equal(@"C:\Data\Photos", settings.GetSlot(3));
    }

    [Fact]
    public void ClearSlot_EmptiesOnlyThatSlot()
    {
        // Arrange
        var settings = new AppSettings();
        settings.SetSlot(1, @"C:\One");
        settings.SetSlot(2, @"C:\Two");

        // Act
        settings.ClearSlot(1);

        // Assert
        Assert.Equal(string.Empty, settings.GetSlot(1));
        Assert.Equal(@"C:\Two", settings.GetSlot(2));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(11)]
    [InlineData(-1)]
    public void Slots_RejectNumbersOutsideOneToTen(int slot)
    {
        // Arrange
        var settings = new AppSettings();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => settings.GetSlot(slot));
        Assert.Throws<ArgumentOutOfRangeException>(() => settings.SetSlot(slot, @"C:\Data"));
        Assert.Throws<ArgumentOutOfRangeException>(() => settings.ClearSlot(slot));
    }

    [Fact]
    public void PathSlots_AreAlwaysTenLong_AndNeverNull()
    {
        // Arrange
        var settings = new AppSettings();

        // Act
        settings.PathSlots = new[] { @"C:\One", null! };

        // Assert
        Assert.Equal(AppSettings.PathSlotCount, settings.PathSlots.Length);
        Assert.Equal(@"C:\One", settings.GetSlot(1));
        Assert.Equal(string.Empty, settings.GetSlot(2));
        Assert.Equal(string.Empty, settings.GetSlot(10));
    }
}
