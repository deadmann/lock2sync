using System.Drawing;
using System.IO;

namespace DirectoryLockToSync;

/// <summary>
/// Loads a picture into memory so the file is not kept open. The 2008 version used <c>new Bitmap(path)</c>, which locks the
/// file for as long as the picture lives; replacing the background picture then failed ("Can't show image in main form").
/// </summary>
internal static class SkinImageLoader
{
    /// <summary>Returns an independent copy of the picture; throws if the file is not a picture.</summary>
    public static Image Load(string path)
    {
        var bytes = File.ReadAllBytes(path);
        using (var stream = new MemoryStream(bytes))
        using (var original = Image.FromStream(stream))
        {
            return new Bitmap(original);
        }
    }
}
