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
        var store = new SettingsStore(GetSettingsPath(args));

        // When the password is active, ask for it before the main window exists; Exit simply ends the program.
        var settings = store.Load();
        if (settings.LoginEnabled)
        {
            using (var login = new LoginForm(settings))
            {
                if (login.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            }
        }

        Application.Run(new MainForm(store));
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
