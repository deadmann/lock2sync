using Krypton.Toolkit;

namespace DirectoryLockToSync;

/// <summary>Maps the seven 2008 theme names to the Krypton palettes they always meant.</summary>
internal static class ThemeMapper
{
    public static PaletteMode ToPaletteMode(AppTheme theme)
    {
        switch (theme)
        {
            case AppTheme.LightSilver: return PaletteMode.Office2007Silver;
            case AppTheme.LightBlack: return PaletteMode.Office2007Black;
            case AppTheme.DarkBlue: return PaletteMode.SparkleBlue;
            case AppTheme.DarkOrange: return PaletteMode.SparkleOrange;
            case AppTheme.DarkPurple: return PaletteMode.SparklePurple;
            case AppTheme.Simple: return PaletteMode.ProfessionalOffice2003;
            default: return PaletteMode.Office2007Blue;
        }
    }
}
