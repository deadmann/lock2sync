using System;
using Krypton.Toolkit;

namespace DirectoryLockToSync;

/// <summary>
/// The window "chrome" every Krypton window of the program shares, kept in code on purpose: the designer rewrites the
/// *.Designer.cs files and drops settings it does not serialize, so anything placed there can silently disappear.
/// Windows inherit from this class (visual inheritance), so the designer shows the same look and leaves it alone.
/// </summary>
public class ChromeForm : KryptonForm
{
    public ChromeForm()
    {
        // The plainer 2008 title bar.
        TitleStyle = KryptonFormTitleStyle.Classic;

        // The window frame uses the same surface as the panel, so it does not show as a darker "padding" strip.
        if (StateCommon != null) StateCommon.BackStyle = PaletteBackStyle.PanelClient;

        // Not resizable. With the minimize button on, the maximize button is shown greyed out, like in the 2008 windows.
        MaximizeBox = false;
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        WindowCorners.Round(Handle); // round corners on Windows 11 (ignored on older Windows)
    }
}
