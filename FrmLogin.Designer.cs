namespace Vivy
{
    partial class FrmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            pcbLogo = new PictureBox();
            lblLogin = new Label();
            pcbLogin = new PictureBox();
            pnlLogin = new Panel();
            pnlPassword = new Panel();
            pcbPassword = new PictureBox();
            btnLogin = new Button();
            lblNotRegistred = new Label();
            lblExit = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            pnlReg = new Panel();
            tbxEmail = new TextBox();
            pnlEmail = new Panel();
            pcbEmail = new PictureBox();
            tbxPassword = new TextBox();
            tbxLogin = new TextBox();
            lblExitReg = new Label();
            lblAlreadyReg = new Label();
            btnReg = new Button();
            pnlPasswordReg = new Panel();
            pcbPasswordReg = new PictureBox();
            pnlLoginReg = new Panel();
            pcbLoginReg = new PictureBox();
            lblSignUp = new Label();
            pcbLogoReg = new PictureBox();
            pnlLog = new Panel();
            btnReveal = new Button();
            ((System.ComponentModel.ISupportInitialize)pcbLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pcbLogin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pcbPassword).BeginInit();
            pnlReg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pcbEmail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pcbPasswordReg).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pcbLoginReg).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pcbLogoReg).BeginInit();
            pnlLog.SuspendLayout();
            SuspendLayout();
            // 
            // pcbLogo
            // 
            pcbLogo.Image = (Image)resources.GetObject("pcbLogo.Image");
            pcbLogo.Location = new Point(110, 62);
            pcbLogo.Name = "pcbLogo";
            pcbLogo.Size = new Size(87, 71);
            pcbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pcbLogo.TabIndex = 0;
            pcbLogo.TabStop = false;
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.Font = new Font("Tahoma", 24F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblLogin.ForeColor = Color.FromArgb(0, 126, 249);
            lblLogin.Location = new Point(88, 156);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(133, 39);
            lblLogin.TabIndex = 1;
            lblLogin.Text = "LOG IN";
            // 
            // pcbLogin
            // 
            pcbLogin.Image = (Image)resources.GetObject("pcbLogin.Image");
            pcbLogin.Location = new Point(36, 203);
            pcbLogin.Name = "pcbLogin";
            pcbLogin.Size = new Size(40, 40);
            pcbLogin.SizeMode = PictureBoxSizeMode.Zoom;
            pcbLogin.TabIndex = 2;
            pcbLogin.TabStop = false;
            // 
            // pnlLogin
            // 
            pnlLogin.BackColor = Color.FromArgb(0, 126, 249);
            pnlLogin.Location = new Point(36, 248);
            pnlLogin.Name = "pnlLogin";
            pnlLogin.Size = new Size(236, 1);
            pnlLogin.TabIndex = 3;
            // 
            // pnlPassword
            // 
            pnlPassword.BackColor = Color.FromArgb(0, 126, 249);
            pnlPassword.Location = new Point(36, 310);
            pnlPassword.Name = "pnlPassword";
            pnlPassword.Size = new Size(236, 1);
            pnlPassword.TabIndex = 5;
            // 
            // pcbPassword
            // 
            pcbPassword.Image = (Image)resources.GetObject("pcbPassword.Image");
            pcbPassword.Location = new Point(36, 271);
            pcbPassword.Name = "pcbPassword";
            pcbPassword.Size = new Size(40, 40);
            pcbPassword.SizeMode = PictureBoxSizeMode.Zoom;
            pcbPassword.TabIndex = 4;
            pcbPassword.TabStop = false;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(0, 126, 249);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Bahnschrift", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnLogin.ForeColor = Color.FromArgb(46, 51, 73);
            btnLogin.Location = new Point(36, 338);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(236, 37);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "ВХІД";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblNotRegistred
            // 
            lblNotRegistred.AutoSize = true;
            lblNotRegistred.Font = new Font("Bahnschrift", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 204);
            lblNotRegistred.ForeColor = Color.FromArgb(0, 126, 249);
            lblNotRegistred.Location = new Point(97, 378);
            lblNotRegistred.Name = "lblNotRegistred";
            lblNotRegistred.Size = new Size(115, 16);
            lblNotRegistred.TabIndex = 7;
            lblNotRegistred.Text = "Не зареєстровані?";
            lblNotRegistred.Click += lblNotRegistred_Click;
            // 
            // lblExit
            // 
            lblExit.AutoSize = true;
            lblExit.Font = new Font("Bahnschrift", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 204);
            lblExit.ForeColor = Color.FromArgb(0, 126, 249);
            lblExit.Location = new Point(132, 434);
            lblExit.Name = "lblExit";
            lblExit.Size = new Size(43, 16);
            lblExit.TabIndex = 8;
            lblExit.Text = "Вийти";
            lblExit.Click += lblExit_Click;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.FromArgb(25, 28, 41);
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Bahnschrift", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            txtUsername.ForeColor = Color.FromArgb(0, 126, 249);
            txtUsername.Location = new Point(82, 218);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(190, 19);
            txtUsername.TabIndex = 9;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(25, 28, 41);
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Bahnschrift", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            txtPassword.ForeColor = Color.FromArgb(0, 126, 249);
            txtPassword.Location = new Point(88, 280);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(148, 19);
            txtPassword.TabIndex = 10;
            // 
            // pnlReg
            // 
            pnlReg.Controls.Add(tbxEmail);
            pnlReg.Controls.Add(pnlEmail);
            pnlReg.Controls.Add(pcbEmail);
            pnlReg.Controls.Add(tbxPassword);
            pnlReg.Controls.Add(tbxLogin);
            pnlReg.Controls.Add(lblExitReg);
            pnlReg.Controls.Add(lblAlreadyReg);
            pnlReg.Controls.Add(btnReg);
            pnlReg.Controls.Add(pnlPasswordReg);
            pnlReg.Controls.Add(pcbPasswordReg);
            pnlReg.Controls.Add(pnlLoginReg);
            pnlReg.Controls.Add(pcbLoginReg);
            pnlReg.Controls.Add(lblSignUp);
            pnlReg.Controls.Add(pcbLogoReg);
            pnlReg.Location = new Point(0, 0);
            pnlReg.Name = "pnlReg";
            pnlReg.Size = new Size(308, 486);
            pnlReg.TabIndex = 11;
            // 
            // tbxEmail
            // 
            tbxEmail.BackColor = Color.FromArgb(25, 28, 41);
            tbxEmail.BorderStyle = BorderStyle.None;
            tbxEmail.Font = new Font("Bahnschrift", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tbxEmail.ForeColor = Color.FromArgb(0, 126, 249);
            tbxEmail.Location = new Point(88, 334);
            tbxEmail.Name = "tbxEmail";
            tbxEmail.Size = new Size(184, 19);
            tbxEmail.TabIndex = 24;
            // 
            // pnlEmail
            // 
            pnlEmail.BackColor = Color.FromArgb(0, 126, 249);
            pnlEmail.Location = new Point(36, 364);
            pnlEmail.Name = "pnlEmail";
            pnlEmail.Size = new Size(236, 1);
            pnlEmail.TabIndex = 23;
            // 
            // pcbEmail
            // 
            pcbEmail.Image = (Image)resources.GetObject("pcbEmail.Image");
            pcbEmail.Location = new Point(36, 325);
            pcbEmail.Name = "pcbEmail";
            pcbEmail.Size = new Size(40, 40);
            pcbEmail.SizeMode = PictureBoxSizeMode.Zoom;
            pcbEmail.TabIndex = 22;
            pcbEmail.TabStop = false;
            // 
            // tbxPassword
            // 
            tbxPassword.BackColor = Color.FromArgb(25, 28, 41);
            tbxPassword.BorderStyle = BorderStyle.None;
            tbxPassword.Font = new Font("Bahnschrift", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tbxPassword.ForeColor = Color.FromArgb(0, 126, 249);
            tbxPassword.Location = new Point(88, 267);
            tbxPassword.Name = "tbxPassword";
            tbxPassword.Size = new Size(184, 19);
            tbxPassword.TabIndex = 21;
            // 
            // tbxLogin
            // 
            tbxLogin.BackColor = Color.FromArgb(25, 28, 41);
            tbxLogin.BorderStyle = BorderStyle.None;
            tbxLogin.Font = new Font("Bahnschrift", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tbxLogin.ForeColor = Color.FromArgb(0, 126, 249);
            tbxLogin.Location = new Point(82, 205);
            tbxLogin.Name = "tbxLogin";
            tbxLogin.Size = new Size(190, 19);
            tbxLogin.TabIndex = 20;
            // 
            // lblExitReg
            // 
            lblExitReg.AutoSize = true;
            lblExitReg.Font = new Font("Bahnschrift", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 204);
            lblExitReg.ForeColor = Color.FromArgb(0, 126, 249);
            lblExitReg.Location = new Point(132, 462);
            lblExitReg.Name = "lblExitReg";
            lblExitReg.Size = new Size(43, 16);
            lblExitReg.TabIndex = 19;
            lblExitReg.Text = "Вийти";
            lblExitReg.Click += lblExitReg_Click;
            // 
            // lblAlreadyReg
            // 
            lblAlreadyReg.AutoSize = true;
            lblAlreadyReg.Font = new Font("Bahnschrift", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 204);
            lblAlreadyReg.ForeColor = Color.FromArgb(0, 126, 249);
            lblAlreadyReg.Location = new Point(93, 437);
            lblAlreadyReg.Name = "lblAlreadyReg";
            lblAlreadyReg.Size = new Size(123, 16);
            lblAlreadyReg.TabIndex = 18;
            lblAlreadyReg.Text = "Вже зареєстровані?";
            lblAlreadyReg.Click += lblAlreadyReg_Click;
            // 
            // btnReg
            // 
            btnReg.BackColor = Color.FromArgb(0, 126, 249);
            btnReg.FlatStyle = FlatStyle.Flat;
            btnReg.Font = new Font("Bahnschrift", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnReg.ForeColor = Color.FromArgb(46, 51, 73);
            btnReg.Location = new Point(36, 397);
            btnReg.Name = "btnReg";
            btnReg.Size = new Size(236, 37);
            btnReg.TabIndex = 17;
            btnReg.Text = "ЗАРЕЄСТРУВАТИСЯ";
            btnReg.UseVisualStyleBackColor = false;
            btnReg.Click += btnReg_Click;
            // 
            // pnlPasswordReg
            // 
            pnlPasswordReg.BackColor = Color.FromArgb(0, 126, 249);
            pnlPasswordReg.Location = new Point(36, 297);
            pnlPasswordReg.Name = "pnlPasswordReg";
            pnlPasswordReg.Size = new Size(236, 1);
            pnlPasswordReg.TabIndex = 16;
            // 
            // pcbPasswordReg
            // 
            pcbPasswordReg.Image = (Image)resources.GetObject("pcbPasswordReg.Image");
            pcbPasswordReg.Location = new Point(36, 258);
            pcbPasswordReg.Name = "pcbPasswordReg";
            pcbPasswordReg.Size = new Size(40, 40);
            pcbPasswordReg.SizeMode = PictureBoxSizeMode.Zoom;
            pcbPasswordReg.TabIndex = 15;
            pcbPasswordReg.TabStop = false;
            // 
            // pnlLoginReg
            // 
            pnlLoginReg.BackColor = Color.FromArgb(0, 126, 249);
            pnlLoginReg.Location = new Point(36, 235);
            pnlLoginReg.Name = "pnlLoginReg";
            pnlLoginReg.Size = new Size(236, 1);
            pnlLoginReg.TabIndex = 14;
            // 
            // pcbLoginReg
            // 
            pcbLoginReg.Image = (Image)resources.GetObject("pcbLoginReg.Image");
            pcbLoginReg.Location = new Point(36, 190);
            pcbLoginReg.Name = "pcbLoginReg";
            pcbLoginReg.Size = new Size(40, 40);
            pcbLoginReg.SizeMode = PictureBoxSizeMode.Zoom;
            pcbLoginReg.TabIndex = 13;
            pcbLoginReg.TabStop = false;
            // 
            // lblSignUp
            // 
            lblSignUp.AutoSize = true;
            lblSignUp.Font = new Font("Tahoma", 24F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblSignUp.ForeColor = Color.FromArgb(0, 126, 249);
            lblSignUp.Location = new Point(78, 143);
            lblSignUp.Name = "lblSignUp";
            lblSignUp.Size = new Size(155, 39);
            lblSignUp.TabIndex = 12;
            lblSignUp.Text = "SIGN UP";
            // 
            // pcbLogoReg
            // 
            pcbLogoReg.Image = (Image)resources.GetObject("pcbLogoReg.Image");
            pcbLogoReg.Location = new Point(110, 49);
            pcbLogoReg.Name = "pcbLogoReg";
            pcbLogoReg.Size = new Size(87, 71);
            pcbLogoReg.SizeMode = PictureBoxSizeMode.Zoom;
            pcbLogoReg.TabIndex = 11;
            pcbLogoReg.TabStop = false;
            // 
            // pnlLog
            // 
            pnlLog.Controls.Add(btnReveal);
            pnlLog.Controls.Add(txtPassword);
            pnlLog.Controls.Add(txtUsername);
            pnlLog.Controls.Add(lblExit);
            pnlLog.Controls.Add(lblNotRegistred);
            pnlLog.Controls.Add(btnLogin);
            pnlLog.Controls.Add(pnlPassword);
            pnlLog.Controls.Add(pcbPassword);
            pnlLog.Controls.Add(pnlLogin);
            pnlLog.Controls.Add(pcbLogin);
            pnlLog.Controls.Add(lblLogin);
            pnlLog.Controls.Add(pcbLogo);
            pnlLog.Location = new Point(0, 0);
            pnlLog.Name = "pnlLog";
            pnlLog.Size = new Size(308, 486);
            pnlLog.TabIndex = 25;
            pnlLog.Paint += pnlLog_Paint;
            // 
            // btnReveal
            // 
            btnReveal.FlatAppearance.BorderSize = 0;
            btnReveal.FlatStyle = FlatStyle.Flat;
            btnReveal.Location = new Point(242, 278);
            btnReveal.Name = "btnReveal";
            btnReveal.Size = new Size(30, 30);
            btnReveal.TabIndex = 11;
            btnReveal.UseVisualStyleBackColor = false;
            btnReveal.Click += btnReveal_Click;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 28, 41);
            ClientSize = new Size(308, 486);
            Controls.Add(pnlLog);
            Controls.Add(pnlReg);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmMain";
            Load += FrmLogin_Load;
            ((System.ComponentModel.ISupportInitialize)pcbLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pcbLogin).EndInit();
            ((System.ComponentModel.ISupportInitialize)pcbPassword).EndInit();
            pnlReg.ResumeLayout(false);
            pnlReg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pcbEmail).EndInit();
            ((System.ComponentModel.ISupportInitialize)pcbPasswordReg).EndInit();
            ((System.ComponentModel.ISupportInitialize)pcbLoginReg).EndInit();
            ((System.ComponentModel.ISupportInitialize)pcbLogoReg).EndInit();
            pnlLog.ResumeLayout(false);
            pnlLog.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pcbLogo;
        private Label lblLogin;
        private PictureBox pcbLogin;
        private Panel pnlLogin;
        private Panel pnlPassword;
        private PictureBox pcbPassword;
        private Button btnLogin;
        private Label lblNotRegistred;
        private Label lblExit;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Panel pnlReg;
        private TextBox tbxPassword;
        private TextBox tbxLogin;
        private Label lblExitReg;
        private Label lblAlreadyReg;
        private Button btnReg;
        private Panel pnlPasswordReg;
        private PictureBox pcbPasswordReg;
        private Panel pnlLoginReg;
        private PictureBox pcbLoginReg;
        private Label lblSignUp;
        private PictureBox pcbLogoReg;
        private TextBox tbxEmail;
        private Panel pnlEmail;
        private PictureBox pcbEmail;
        private Panel pnlLog;
        private Button btnReveal;
    }
}