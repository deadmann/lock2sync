using System;
using System.IO;
using Xunit;

namespace DirectoryLockToSync.Tests;

public class AppSettingsSkinAndLoginTests : IDisposable
{
    private readonly string _root = Path.Combine(Path.GetTempPath(), "dlts-skinlogin-" + Guid.NewGuid().ToString("N"));

    public void Dispose()
    {
        if (Directory.Exists(_root)) Directory.Delete(_root, recursive: true);
    }

    [Fact]
    public void NewSettings_HaveTheOriginalDefaults()
    {
        // Arrange
        // Act
        var settings = new AppSettings();

        // Assert
        Assert.False(settings.TransparentInfo);
        Assert.Equal(SkinImageMode.Tile, settings.SkinImageMode);
        Assert.Equal(string.Empty, settings.SkinImageFile);
        Assert.Equal(AppTheme.LightBlue, settings.Theme);
        Assert.False(settings.LoginEnabled);
    }

    [Theory]
    [InlineData("Light-Blue", AppTheme.LightBlue)]
    [InlineData("Dark-Orange", AppTheme.DarkOrange)]
    [InlineData("Simple", AppTheme.Simple)]
    [InlineData("", AppTheme.LightBlue)]
    [InlineData(null, AppTheme.LightBlue)]
    [InlineData("nonsense", AppTheme.LightBlue)]
    public void FromText_FindsTheTheme_AndFallsBackToLightBlue(string? text, AppTheme expected)
    {
        // Arrange
        // Act
        var theme = AppThemeNames.FromText(text);

        // Assert
        Assert.Equal(expected, theme);
    }

    [Fact]
    public void ToText_AndFromText_AreInverses_ForEveryTheme()
    {
        // Arrange
        // Act & Assert
        foreach (AppTheme theme in Enum.GetValues(typeof(AppTheme)))
        {
            Assert.Equal(theme, AppThemeNames.FromText(AppThemeNames.ToText(theme)));
        }
    }

    [Fact]
    public void CheckLogin_AcceptsTheUserInAnyLetterCase_ButThePasswordOnlyExactly()
    {
        // Arrange
        var settings = new AppSettings();
        settings.SetLogin("Hassan", "Pa55word");

        // Act
        var right = settings.CheckLogin("hassan", "Pa55word");
        var wrongCase = settings.CheckLogin("Hassan", "pa55word");
        var wrongUser = settings.CheckLogin("someone", "Pa55word");

        // Assert
        Assert.True(right);
        Assert.False(wrongCase);
        Assert.False(wrongUser);
    }

    [Fact]
    public void SetLogin_StoresOnlyAHash()
    {
        // Arrange
        var settings = new AppSettings();

        // Act
        settings.SetLogin("Hassan", "Pa55word");

        // Assert
        Assert.True(settings.LoginEnabled);
        Assert.DoesNotContain("Pa55word", settings.PasswordHash);
        Assert.True(settings.PasswordMatches("Pa55word"));
        Assert.False(settings.PasswordMatches("other"));
    }

    [Fact]
    public void ClearLogin_ForgetsEverything_AndNoLoginPasses()
    {
        // Arrange
        var settings = new AppSettings();
        settings.SetLogin("Hassan", "Pa55word");

        // Act
        settings.ClearLogin();

        // Assert
        Assert.False(settings.LoginEnabled);
        Assert.Equal(string.Empty, settings.UserName);
        Assert.Equal(string.Empty, settings.PasswordHash);
        Assert.False(settings.CheckLogin("Hassan", "Pa55word"));
    }

    [Theory]
    [InlineData("", "x")]
    [InlineData("x", "")]
    public void SetLogin_RejectsAnEmptyUserOrPassword(string user, string password)
    {
        // Arrange
        var settings = new AppSettings();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => settings.SetLogin(user, password));
        Assert.False(settings.LoginEnabled);
    }

    [Fact]
    public void SaveThenLoad_KeepsTheSkinThemeAndLogin()
    {
        // Arrange
        var store = new SettingsStore(Path.Combine(_root, "settings.xml"));
        var settings = new AppSettings
        {
            TransparentInfo = true,
            SkinImageMode = SkinImageMode.Zoom,
            SkinImageFile = "background.jpg",
            Theme = AppTheme.DarkPurple
        };
        settings.SetLogin("Hassan", "Pa55word");

        // Act
        store.Save(settings);
        var loaded = store.Load();

        // Assert
        Assert.True(loaded.TransparentInfo);
        Assert.Equal(SkinImageMode.Zoom, loaded.SkinImageMode);
        Assert.Equal("background.jpg", loaded.SkinImageFile);
        Assert.Equal(AppTheme.DarkPurple, loaded.Theme);
        Assert.True(loaded.CheckLogin("hassan", "Pa55word"));
        Assert.DoesNotContain("Pa55word", File.ReadAllText(store.FilePath));
    }
}
