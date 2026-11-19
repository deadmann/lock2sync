using System;
using System.Windows.Forms;

namespace DirectoryLockToSync;

internal static class Program
{
    /// <summary>Usage: DirectoryLockToSync.exe [--settings &lt;file&gt;]  (the file defaults to %AppData%\DirectoryLockToSync\settings.xml)</summary>
    [STAThread]
    private static void Main(string[] args)
    {
#if NET
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
#endif
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm(new SettingsStore(GetSettingsPath(args))));
    }

    private static string GetSettingsPath(string[] args)
    {
        for (var i = 0; i < args.Length - 1; i++)
        {
            if (string.Equals(args[i], "--settings", StringComparison.OrdinalIgnoreCase)) return args[i + 1];
        }

        return SettingsStore.DefaultFilePath;
    }
}
