using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DirectoryLockToSync;

/// <summary>
/// Draws the background picture the four ways the Setting window offers (Tile, Center, Stretch, Zoom) into a bitmap
/// of the window's size, so it can be used as the main window's own background.
/// </summary>
internal static class SkinRenderer
{
    public static Bitmap Render(Image source, SkinImageMode mode, Size size)
    {
        var result = new Bitmap(Math.Max(1, size.Width), Math.Max(1, size.Height));
        using (var g = Graphics.FromImage(result))
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            switch (mode)
            {
                case SkinImageMode.Stretch:
                    g.DrawImage(source, 0, 0, result.Width, result.Height);
                    break;

                case SkinImageMode.Center:
                    g.DrawImage(source, (result.Width - source.Width) / 2, (result.Height - source.Height) / 2, source.Width, source.Height);
                    break;

                case SkinImageMode.Zoom:
                {
                    var scale = Math.Min((double)result.Width / source.Width, (double)result.Height / source.Height);
                    var width = (int)Math.Round(source.Width * scale);
                    var height = (int)Math.Round(source.Height * scale);
                    g.DrawImage(source, (result.Width - width) / 2, (result.Height - height) / 2, width, height);
                    break;
                }

                default: // Tile, from the top-left corner
                    for (var y = 0; y < result.Height; y += source.Height)
                    {
                        for (var x = 0; x < result.Width; x += source.Width)
                        {
                            g.DrawImage(source, x, y, source.Width, source.Height);
                        }
                    }

                    break;
            }
        }

        return result;
    }
}
