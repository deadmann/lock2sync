using System;

namespace DirectoryLockToSync;

/// <summary>The seven looks of the 2008 Setting window. The order is the order of its drop-down list.</summary>
public enum AppTheme
{
    LightBlue,
    LightSilver,
    LightBlack,
    DarkBlue,
    DarkOrange,
    DarkPurple,
    Simple
}

/// <summary>The names the Setting window shows for each <see cref="AppTheme"/> ("Light-Blue", "Dark-Orange", ...).</summary>
public static class AppThemeNames
{
    /// <summary>The display names, in the same order as the <see cref="AppTheme"/> values.</summary>
    public static readonly string[] All =
    {
        "Light-Blue", "Light-Silver", "Light-Black", "Dark-Blue", "Dark-Orange", "Dark-Purple", "Simple"
    };

    public static string ToText(AppTheme theme) => All[(int)theme];

    /// <summary>Returns the theme with that display name; anything unknown or empty gives the original default (Light-Blue).</summary>
    public static AppTheme FromText(string? text)
    {
        var index = Array.IndexOf(All, text);
        return index >= 0 ? (AppTheme)index : AppTheme.LightBlue;
    }
}
