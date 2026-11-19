using Krypton.Toolkit;

namespace DirectoryLockToSync;

/// <summary>
/// The window frame every window of the program shares. It uses Krypton's Classic title style, which is the closest to the
/// plainer 2008 title bar. (The shadow around a window on Windows 10 and 11 is drawn by Windows itself, not by Krypton.)
/// </summary>
public class ChromeForm : KryptonForm
{
    public ChromeForm()
    {
        TitleStyle = KryptonFormTitleStyle.Classic;
    }
}
