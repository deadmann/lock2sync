using System;
using System.Runtime.InteropServices;

namespace DirectoryLockToSync;

/// <summary>
/// Asks Windows 11 to round the corners of a window, like the Office 2007 windows of the 2008 version were. On older
/// Windows the request is simply ignored.
/// </summary>
internal static class WindowCorners
{
    private const int DwmwaWindowCornerPreference = 33;
    private const int DwmwcpRound = 2;

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int value, int size);

    public static void Round(IntPtr windowHandle)
    {
        try
        {
            var preference = DwmwcpRound;
            DwmSetWindowAttribute(windowHandle, DwmwaWindowCornerPreference, ref preference, sizeof(int));
        }
        catch (DllNotFoundException)
        {
            // no desktop composition on this system: keep square corners
        }
        catch (EntryPointNotFoundException)
        {
        }
    }
}
