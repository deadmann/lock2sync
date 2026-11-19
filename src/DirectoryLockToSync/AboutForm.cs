using System;
using System.Reflection;
using Krypton.Toolkit;

namespace DirectoryLockToSync;

/// <summary>
/// The About window (layout: AboutForm.Designer.cs, logo and text: AboutForm.resx). It shows the product information
/// of the assembly, like the 2008 KryptonAbout form did.
/// </summary>
public sealed partial class AboutForm : KryptonForm
{
    public AboutForm()
    {
        InitializeComponent();

        var assembly = typeof(AboutForm).Assembly;
        Text = "About " + AttributeText<AssemblyTitleAttribute>(assembly, a => a.Title, "Directory Lock To Sync");
        labelProductName.Text = AttributeText<AssemblyProductAttribute>(assembly, a => a.Product, string.Empty);
        labelVersion.Text = "Version " + assembly.GetName().Version;
        labelCopyright.Text = AttributeText<AssemblyCopyrightAttribute>(assembly, a => a.Copyright, string.Empty);
        labelCompanyName.Text = AttributeText<AssemblyCompanyAttribute>(assembly, a => a.Company, string.Empty);
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        WindowCorners.Round(Handle);
    }

    private static string AttributeText<T>(Assembly assembly, Func<T, string> read, string fallback) where T : Attribute
    {
        var attribute = (T?)Attribute.GetCustomAttribute(assembly, typeof(T));
        var text = attribute == null ? string.Empty : read(attribute);
        return string.IsNullOrEmpty(text) ? fallback : text;
    }

    private void kryptonButton1_Click(object? sender, EventArgs e) => Close();
}
