using System;
using Krypton.Toolkit;

namespace DirectoryLockToSync;

/// <summary>"Password Confirm" window: asks for the new password a second time. Layout: InputForm.Designer.cs.</summary>
public sealed partial class InputForm : KryptonForm
{
    public InputForm()
    {
        InitializeComponent();
    }

    /// <summary>What the user typed.</summary>
    public string Confirmation => txtVerify.Text;

    // Enter does the same (the form's AcceptButton is this button), like the original.
    private void btnOk_Click(object? sender, EventArgs e) => Close();
}
