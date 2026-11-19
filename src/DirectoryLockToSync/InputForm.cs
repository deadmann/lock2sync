using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace DirectoryLockToSync;

/// <summary>"Confirm" window: asks for the new password a second time. Same layout as the 2008 KryptonInput form.</summary>
public sealed class InputForm : ChromeForm
{
    private readonly KryptonTextBox _confirmBox = new KryptonTextBox();
    private readonly KryptonButton _checkButton = new KryptonButton();

    public InputForm()
    {
        SuspendLayout();
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ControlBox = false;
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Confirm";

        _confirmBox.Location = new Point(59, 36);
        _confirmBox.Size = new Size(211, 22);
        _confirmBox.PasswordChar = '*';
        _confirmBox.TabIndex = 2;

        _checkButton.Text = "Check";
        _checkButton.Location = new Point(3, 64);
        _checkButton.Size = new Size(267, 25);
        _checkButton.TabIndex = 3;
        _checkButton.Click += (sender, e) => Close();

        var panel = new KryptonPanel { Dock = DockStyle.Fill };
        panel.Controls.Add(_checkButton);
        panel.Controls.Add(_confirmBox);
        panel.Controls.Add(new KryptonLabel { Text = "Confirm", Location = new Point(3, 36), Size = new Size(50, 19), TabIndex = 1 });
        panel.Controls.Add(new KryptonLabel
        {
            Text = "For verify your password, enter that on here ,again .",
            Location = new Point(3, 3),
            Size = new Size(267, 19),
            TabIndex = 0
        });
        Controls.Add(panel);
        ClientSize = new Size(274, 93);
        AcceptButton = _checkButton; // Enter closes it, like the original

        ResumeLayout(false);
        PerformLayout();
    }

    /// <summary>What the user typed.</summary>
    public string Confirmation => _confirmBox.Text;
}
