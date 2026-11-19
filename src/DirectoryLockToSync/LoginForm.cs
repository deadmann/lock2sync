using System;
using System.Windows.Forms;

namespace DirectoryLockToSync;

/// <summary>
/// The start-up login window (layout: LoginForm.Designer.cs, from the 2008 Login form: a plain borderless form with a
/// Krypton panel). Ok checks the user name and password; Exit closes the program.
/// </summary>
public sealed partial class LoginForm : Form
{
    private readonly AppSettings _settings;

    /// <summary>For the designer: nobody can log in with it.</summary>
    public LoginForm() : this(new AppSettings())
    {
    }

    public LoginForm(AppSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        InitializeComponent();
    }

    private void CheckPassword()
    {
        if (_settings.CheckLogin(txtGetUser.Text, txtGetPass.Text))
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        else
        {
            MessageBox.Show(this, "Your password is wrong ,please try again !!!", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void Ok_Click(object? sender, EventArgs e) => CheckPassword();

    /// <summary>The Exit button: the caller closes the program when it sees a result other than OK.</summary>
    private void Cancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    // Enter does the same as Ok, in either text box or on the form itself, like the original.
    private void txtGetUser_KeyPress(object? sender, KeyPressEventArgs e) => EnterChecks(e);

    private void txtGetPass_KeyPress(object? sender, KeyPressEventArgs e) => EnterChecks(e);

    private void LoginForm_KeyPress(object? sender, KeyPressEventArgs e) => EnterChecks(e);

    private void EnterChecks(KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Return) return;
        e.Handled = true;
        CheckPassword();
    }
}
