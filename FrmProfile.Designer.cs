using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Vivy
{
    partial class FrmProfile
    {
        private System.ComponentModel.IContainer components = null;
        private PictureBox picAvatar;
        private Button btnChangeAvatar;
        private TextBox txtLogin;
        private TextBox txtEmail;
        private TextBox txtNewPassword;
        private Label lblEmail;
        private Label lblNewPassword;
        private Button btnSave;
        private Label lblLogin;
        private Panel panelProfile;

        // Для скругления углов формы
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void InitializeComponent()
        {
            panelProfile = new Panel();
            button1 = new Button();
            picAvatar = new PictureBox();
            btnChangeAvatar = new Button();
            lblLogin = new Label();
            txtLogin = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblNewPassword = new Label();
            txtNewPassword = new TextBox();
            btnSave = new Button();
            panelProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            SuspendLayout();
            // 
            // panelProfile
            // 
            panelProfile.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelProfile.BackColor = Color.FromArgb(37, 42, 64);
            panelProfile.Controls.Add(button1);
            panelProfile.Controls.Add(picAvatar);
            panelProfile.Controls.Add(btnChangeAvatar);
            panelProfile.Controls.Add(lblLogin);
            panelProfile.Controls.Add(txtLogin);
            panelProfile.Controls.Add(lblEmail);
            panelProfile.Controls.Add(txtEmail);
            panelProfile.Controls.Add(lblNewPassword);
            panelProfile.Controls.Add(txtNewPassword);
            panelProfile.Controls.Add(btnSave);
            panelProfile.Location = new Point(11, 13);
            panelProfile.Margin = new Padding(3, 4, 3, 4);
            panelProfile.Name = "panelProfile";
            panelProfile.Size = new Size(526, 267);
            panelProfile.TabIndex = 0;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(46, 51, 73);
            button1.DialogResult = DialogResult.Abort;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.FromArgb(0, 126, 249);
            button1.Location = new Point(497, 0);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(29, 33);
            button1.TabIndex = 9;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = false;
            // 
            // picAvatar
            // 
            picAvatar.BackColor = Color.Transparent;
            picAvatar.BorderStyle = BorderStyle.FixedSingle;
            picAvatar.Location = new Point(34, 40);
            picAvatar.Margin = new Padding(3, 4, 3, 4);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(114, 133);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.TabIndex = 0;
            picAvatar.TabStop = false;
            // 
            // btnChangeAvatar
            // 
            btnChangeAvatar.BackColor = Color.FromArgb(0, 126, 249);
            btnChangeAvatar.FlatAppearance.BorderSize = 0;
            btnChangeAvatar.FlatStyle = FlatStyle.Flat;
            btnChangeAvatar.Font = new Font("Bahnschrift", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnChangeAvatar.ForeColor = Color.FromArgb(46, 51, 73);
            btnChangeAvatar.Location = new Point(34, 187);
            btnChangeAvatar.Margin = new Padding(3, 4, 3, 4);
            btnChangeAvatar.Name = "btnChangeAvatar";
            btnChangeAvatar.Size = new Size(114, 40);
            btnChangeAvatar.TabIndex = 1;
            btnChangeAvatar.Text = "Ändern";
            btnChangeAvatar.UseVisualStyleBackColor = false;
            btnChangeAvatar.Click += btnChangeAvatar_Click;
            // 
            // lblLogin
            // 
            lblLogin.Font = new Font("Segoe UI", 10F);
            lblLogin.ForeColor = Color.White;
            lblLogin.Location = new Point(173, 43);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(114, 31);
            lblLogin.TabIndex = 2;
            lblLogin.Text = "Name:";
            // 
            // txtLogin
            // 
            txtLogin.BackColor = Color.FromArgb(46, 51, 73);
            txtLogin.BorderStyle = BorderStyle.FixedSingle;
            txtLogin.Font = new Font("Segoe UI", 10F);
            txtLogin.ForeColor = Color.White;
            txtLogin.Location = new Point(297, 40);
            txtLogin.Margin = new Padding(3, 4, 3, 4);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(194, 30);
            txtLogin.TabIndex = 3;
            // 
            // lblEmail
            // 
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.ForeColor = Color.White;
            lblEmail.Location = new Point(173, 96);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(114, 31);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "E-mail:";
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.FromArgb(46, 51, 73);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.ForeColor = Color.White;
            txtEmail.Location = new Point(297, 93);
            txtEmail.Margin = new Padding(3, 4, 3, 4);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(194, 30);
            txtEmail.TabIndex = 5;
            // 
            // lblNewPassword
            // 
            lblNewPassword.Font = new Font("Segoe UI", 10F);
            lblNewPassword.ForeColor = Color.White;
            lblNewPassword.Location = new Point(173, 149);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(118, 31);
            lblNewPassword.TabIndex = 6;
            lblNewPassword.Text = "Neues Passwort:";
            // 
            // txtNewPassword
            // 
            txtNewPassword.BackColor = Color.FromArgb(46, 51, 73);
            txtNewPassword.BorderStyle = BorderStyle.FixedSingle;
            txtNewPassword.Font = new Font("Segoe UI", 10F);
            txtNewPassword.ForeColor = Color.White;
            txtNewPassword.Location = new Point(297, 147);
            txtNewPassword.Margin = new Padding(3, 4, 3, 4);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.PasswordChar = '*';
            txtNewPassword.Size = new Size(194, 30);
            txtNewPassword.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(0, 126, 249);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Bahnschrift", 14.25F, FontStyle.Bold);
            btnSave.ForeColor = Color.FromArgb(46, 51, 73);
            btnSave.Location = new Point(297, 200);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(137, 47);
            btnSave.TabIndex = 8;
            btnSave.Text = "Speichern";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // FrmProfile
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(24, 30, 54);
            ClientSize = new Size(549, 293);
            Controls.Add(panelProfile);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "FrmProfile";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Профіль користувача";
            panelProfile.ResumeLayout(false);
            panelProfile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            ResumeLayout(false);
        }
        private Button button1;
    }
}
