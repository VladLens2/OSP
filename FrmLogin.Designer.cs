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
            pcbLogo.Location = new Point(126, 83);
            pcbLogo.Margin = new Padding(3, 4, 3, 4);
            pcbLogo.Name = "pcbLogo";
            pcbLogo.Size = new Size(99, 95);
            pcbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pcbLogo.TabIndex = 0;
            pcbLogo.TabStop = false;
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.Font = new Font("Tahoma", 24F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblLogin.ForeColor = Color.FromArgb(0, 126, 249);
            lblLogin.Location = new Point(63, 201);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(248, 48);
            lblLogin.TabIndex = 1;
            lblLogin.Text = "ANMELDEN";
            // 
            // pcbLogin
            // 
            pcbLogin.Image = (Image)resources.GetObject("pcbLogin.Image");
            pcbLogin.Location = new Point(41, 271);
            pcbLogin.Margin = new Padding(3, 4, 3, 4);
            pcbLogin.Name = "pcbLogin";
            pcbLogin.Size = new Size(46, 53);
            pcbLogin.SizeMode = PictureBoxSizeMode.Zoom;
            pcbLogin.TabIndex = 2;
            pcbLogin.TabStop = false;
            // 
            // pnlLogin
            // 
            pnlLogin.BackColor = Color.FromArgb(0, 126, 249);
            pnlLogin.Location = new Point(41, 331);
            pnlLogin.Margin = new Padding(3, 4, 3, 4);
            pnlLogin.Name = "pnlLogin";
            pnlLogin.Size = new Size(270, 1);
            pnlLogin.TabIndex = 3;
            // 
            // pnlPassword
            // 
            pnlPassword.BackColor = Color.FromArgb(0, 126, 249);
            pnlPassword.Location = new Point(41, 413);
            pnlPassword.Margin = new Padding(3, 4, 3, 4);
            pnlPassword.Name = "pnlPassword";
            pnlPassword.Size = new Size(270, 1);
            pnlPassword.TabIndex = 5;
            // 
            // pcbPassword
            // 
            pcbPassword.Image = (Image)resources.GetObject("pcbPassword.Image");
            pcbPassword.Location = new Point(41, 361);
            pcbPassword.Margin = new Padding(3, 4, 3, 4);
            pcbPassword.Name = "pcbPassword";
            pcbPassword.Size = new Size(46, 53);
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
            btnLogin.Location = new Point(41, 451);
            btnLogin.Margin = new Padding(3, 4, 3, 4);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(270, 49);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Anmelden";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblNotRegistred
            // 
            lblNotRegistred.AutoSize = true;
            lblNotRegistred.Font = new Font("Bahnschrift", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 204);
            lblNotRegistred.ForeColor = Color.FromArgb(0, 126, 249);
            lblNotRegistred.Location = new Point(111, 504);
            lblNotRegistred.Name = "lblNotRegistred";
            lblNotRegistred.Size = new Size(138, 21);
            lblNotRegistred.TabIndex = 7;
            lblNotRegistred.Text = "Nicht registriert?";
            lblNotRegistred.Click += lblNotRegistred_Click;
            // 
            // lblExit
            // 
            lblExit.AutoSize = true;
            lblExit.Font = new Font("Bahnschrift", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 204);
            lblExit.ForeColor = Color.FromArgb(0, 126, 249);
            lblExit.Location = new Point(128, 582);
            lblExit.Name = "lblExit";
            lblExit.Size = new Size(82, 21);
            lblExit.TabIndex = 8;
            lblExit.Text = "Schließen";
            lblExit.Click += lblExit_Click;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.FromArgb(25, 28, 41);
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Bahnschrift", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            txtUsername.ForeColor = Color.FromArgb(0, 126, 249);
            txtUsername.Location = new Point(94, 291);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(217, 23);
            txtUsername.TabIndex = 9;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(25, 28, 41);
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Bahnschrift", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            txtPassword.ForeColor = Color.FromArgb(0, 126, 249);
            txtPassword.Location = new Point(101, 373);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(169, 23);
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
            pnlReg.Margin = new Padding(3, 4, 3, 4);
            pnlReg.Name = "pnlReg";
            pnlReg.Size = new Size(352, 648);
            pnlReg.TabIndex = 11;
            // 
            // tbxEmail
            // 
            tbxEmail.BackColor = Color.FromArgb(25, 28, 41);
            tbxEmail.BorderStyle = BorderStyle.None;
            tbxEmail.Font = new Font("Bahnschrift", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tbxEmail.ForeColor = Color.FromArgb(0, 126, 249);
            tbxEmail.Location = new Point(101, 445);
            tbxEmail.Margin = new Padding(3, 4, 3, 4);
            tbxEmail.Name = "tbxEmail";
            tbxEmail.Size = new Size(210, 23);
            tbxEmail.TabIndex = 24;
            // 
            // pnlEmail
            // 
            pnlEmail.BackColor = Color.FromArgb(0, 126, 249);
            pnlEmail.Location = new Point(41, 485);
            pnlEmail.Margin = new Padding(3, 4, 3, 4);
            pnlEmail.Name = "pnlEmail";
            pnlEmail.Size = new Size(270, 1);
            pnlEmail.TabIndex = 23;
            // 
            // pcbEmail
            // 
            pcbEmail.Image = (Image)resources.GetObject("pcbEmail.Image");
            pcbEmail.Location = new Point(41, 433);
            pcbEmail.Margin = new Padding(3, 4, 3, 4);
            pcbEmail.Name = "pcbEmail";
            pcbEmail.Size = new Size(46, 53);
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
            tbxPassword.Location = new Point(101, 356);
            tbxPassword.Margin = new Padding(3, 4, 3, 4);
            tbxPassword.Name = "tbxPassword";
            tbxPassword.Size = new Size(210, 23);
            tbxPassword.TabIndex = 21;
            // 
            // tbxLogin
            // 
            tbxLogin.BackColor = Color.FromArgb(25, 28, 41);
            tbxLogin.BorderStyle = BorderStyle.None;
            tbxLogin.Font = new Font("Bahnschrift", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tbxLogin.ForeColor = Color.FromArgb(0, 126, 249);
            tbxLogin.Location = new Point(94, 273);
            tbxLogin.Margin = new Padding(3, 4, 3, 4);
            tbxLogin.Name = "tbxLogin";
            tbxLogin.Size = new Size(217, 23);
            tbxLogin.TabIndex = 20;
            // 
            // lblExitReg
            // 
            lblExitReg.AutoSize = true;
            lblExitReg.Font = new Font("Bahnschrift", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 204);
            lblExitReg.ForeColor = Color.FromArgb(0, 126, 249);
            lblExitReg.Location = new Point(143, 618);
            lblExitReg.Name = "lblExitReg";
            lblExitReg.Size = new Size(82, 21);
            lblExitReg.TabIndex = 19;
            lblExitReg.Text = "Schließen";
            lblExitReg.Click += lblExitReg_Click;
            // 
            // lblAlreadyReg
            // 
            lblAlreadyReg.AutoSize = true;
            lblAlreadyReg.Font = new Font("Bahnschrift", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 204);
            lblAlreadyReg.ForeColor = Color.FromArgb(0, 126, 249);
            lblAlreadyReg.Location = new Point(106, 583);
            lblAlreadyReg.Name = "lblAlreadyReg";
            lblAlreadyReg.Size = new Size(144, 21);
            lblAlreadyReg.TabIndex = 18;
            lblAlreadyReg.Text = "Schon registriert?";
            lblAlreadyReg.Click += lblAlreadyReg_Click;
            // 
            // btnReg
            // 
            btnReg.BackColor = Color.FromArgb(0, 126, 249);
            btnReg.FlatStyle = FlatStyle.Flat;
            btnReg.Font = new Font("Bahnschrift", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnReg.ForeColor = Color.FromArgb(46, 51, 73);
            btnReg.Location = new Point(41, 529);
            btnReg.Margin = new Padding(3, 4, 3, 4);
            btnReg.Name = "btnReg";
            btnReg.Size = new Size(270, 49);
            btnReg.TabIndex = 17;
            btnReg.Text = "REGISTRIEREN";
            btnReg.UseVisualStyleBackColor = false;
            btnReg.Click += btnReg_Click;
            // 
            // pnlPasswordReg
            // 
            pnlPasswordReg.BackColor = Color.FromArgb(0, 126, 249);
            pnlPasswordReg.Location = new Point(41, 396);
            pnlPasswordReg.Margin = new Padding(3, 4, 3, 4);
            pnlPasswordReg.Name = "pnlPasswordReg";
            pnlPasswordReg.Size = new Size(270, 1);
            pnlPasswordReg.TabIndex = 16;
            // 
            // pcbPasswordReg
            // 
            pcbPasswordReg.Image = (Image)resources.GetObject("pcbPasswordReg.Image");
            pcbPasswordReg.Location = new Point(41, 344);
            pcbPasswordReg.Margin = new Padding(3, 4, 3, 4);
            pcbPasswordReg.Name = "pcbPasswordReg";
            pcbPasswordReg.Size = new Size(46, 53);
            pcbPasswordReg.SizeMode = PictureBoxSizeMode.Zoom;
            pcbPasswordReg.TabIndex = 15;
            pcbPasswordReg.TabStop = false;
            // 
            // pnlLoginReg
            // 
            pnlLoginReg.BackColor = Color.FromArgb(0, 126, 249);
            pnlLoginReg.Location = new Point(41, 313);
            pnlLoginReg.Margin = new Padding(3, 4, 3, 4);
            pnlLoginReg.Name = "pnlLoginReg";
            pnlLoginReg.Size = new Size(270, 1);
            pnlLoginReg.TabIndex = 14;
            // 
            // pcbLoginReg
            // 
            pcbLoginReg.Image = (Image)resources.GetObject("pcbLoginReg.Image");
            pcbLoginReg.Location = new Point(41, 253);
            pcbLoginReg.Margin = new Padding(3, 4, 3, 4);
            pcbLoginReg.Name = "pcbLoginReg";
            pcbLoginReg.Size = new Size(46, 53);
            pcbLoginReg.SizeMode = PictureBoxSizeMode.Zoom;
            pcbLoginReg.TabIndex = 13;
            pcbLoginReg.TabStop = false;
            // 
            // lblSignUp
            // 
            lblSignUp.AutoSize = true;
            lblSignUp.Font = new Font("Tahoma", 24F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblSignUp.ForeColor = Color.FromArgb(0, 126, 249);
            lblSignUp.Location = new Point(12, 182);
            lblSignUp.Name = "lblSignUp";
            lblSignUp.Size = new Size(331, 48);
            lblSignUp.TabIndex = 12;
            lblSignUp.Text = "REGISTRIEREN";
            // 
            // pcbLogoReg
            // 
            pcbLogoReg.Image = (Image)resources.GetObject("pcbLogoReg.Image");
            pcbLogoReg.Location = new Point(126, 65);
            pcbLogoReg.Margin = new Padding(3, 4, 3, 4);
            pcbLogoReg.Name = "pcbLogoReg";
            pcbLogoReg.Size = new Size(99, 95);
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
            pnlLog.Margin = new Padding(3, 4, 3, 4);
            pnlLog.Name = "pnlLog";
            pnlLog.Size = new Size(352, 648);
            pnlLog.TabIndex = 25;
            // 
            // btnReveal
            // 
            btnReveal.FlatAppearance.BorderSize = 0;
            btnReveal.FlatStyle = FlatStyle.Flat;
            btnReveal.Location = new Point(277, 371);
            btnReveal.Margin = new Padding(3, 4, 3, 4);
            btnReveal.Name = "btnReveal";
            btnReveal.Size = new Size(34, 40);
            btnReveal.TabIndex = 11;
            btnReveal.UseVisualStyleBackColor = false;
            btnReveal.Click += btnReveal_Click;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 28, 41);
            ClientSize = new Size(352, 648);
            Controls.Add(pnlReg);
            Controls.Add(pnlLog);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
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