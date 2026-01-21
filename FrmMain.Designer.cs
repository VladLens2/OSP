namespace Vivy
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            pnlNaw = new Panel();
            Pnlscroll = new Panel();
            btnsettings = new Button();
            btnContactUs = new Button();
            BtnDashboard = new Button();
            panel2 = new Panel();
            label1 = new Label();
            Usder = new Label();
            picUserAvatar = new PictureBox();
            panelAbout = new Panel();
            panelContact = new Panel();
            llGithub = new LinkLabel();
            label9 = new Label();
            label10 = new Label();
            panelaboutUs = new Panel();
            label6 = new Label();
            label5 = new Label();
            panelSupport = new Panel();
            label8 = new Label();
            label7 = new Label();
            panelProjects = new Panel();
            llOllama = new LinkLabel();
            label14 = new Label();
            label12 = new Label();
            label4 = new Label();
            panelAboutVivy = new Panel();
            lblAboutTitle = new Label();
            pictureBox2 = new PictureBox();
            lblAboutText = new Label();
            label3 = new Label();
            label2 = new Label();
            panelSettings = new Panel();
            btnLogout = new Button();
            cbModel = new ComboBox();
            label11 = new Label();
            cbSpeakResponses = new CheckBox();
            btnSaveSettings = new Button();
            lblTheme = new Label();
            cbTheme = new ComboBox();
            cbLanguage = new ComboBox();
            lblLanguage = new Label();
            lblSettingsTitle = new Label();
            panelInput = new Panel();
            btnSend = new Button();
            textBoxInput = new TextBox();
            panelVivy = new Panel();
            label15 = new Label();
            btnClearChat = new Button();
            btnNewChat = new Button();
            label13 = new Label();
            labelvivy = new Label();
            panelHistory = new Panel();
            listBoxHistory = new ListBox();
            richTextBox1 = new RichTextBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            toolTip1 = new ToolTip(components);
            toolTip2 = new ToolTip(components);
            toolTip3 = new ToolTip(components);
            pnlNaw.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picUserAvatar).BeginInit();
            panelAbout.SuspendLayout();
            panelContact.SuspendLayout();
            panelaboutUs.SuspendLayout();
            panelSupport.SuspendLayout();
            panelProjects.SuspendLayout();
            panelAboutVivy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panelSettings.SuspendLayout();
            panelInput.SuspendLayout();
            panelVivy.SuspendLayout();
            panelHistory.SuspendLayout();
            SuspendLayout();
            // 
            // pnlNaw
            // 
            pnlNaw.BackColor = Color.FromArgb(24, 30, 54);
            pnlNaw.Controls.Add(Pnlscroll);
            pnlNaw.Controls.Add(btnsettings);
            pnlNaw.Controls.Add(btnContactUs);
            pnlNaw.Controls.Add(BtnDashboard);
            pnlNaw.Controls.Add(panel2);
            resources.ApplyResources(pnlNaw, "pnlNaw");
            pnlNaw.Name = "pnlNaw";
            // 
            // Pnlscroll
            // 
            Pnlscroll.BackColor = Color.FromArgb(0, 126, 249);
            resources.ApplyResources(Pnlscroll, "Pnlscroll");
            Pnlscroll.Name = "Pnlscroll";
            // 
            // btnsettings
            // 
            btnsettings.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(btnsettings, "btnsettings");
            btnsettings.ForeColor = Color.FromArgb(0, 126, 249);
            btnsettings.Name = "btnsettings";
            btnsettings.UseVisualStyleBackColor = true;
            btnsettings.Click += btnsettings_Click;
            btnsettings.Leave += btnsettings_Leave;
            // 
            // btnContactUs
            // 
            resources.ApplyResources(btnContactUs, "btnContactUs");
            btnContactUs.FlatAppearance.BorderSize = 0;
            btnContactUs.ForeColor = Color.FromArgb(0, 126, 249);
            btnContactUs.Name = "btnContactUs";
            btnContactUs.UseVisualStyleBackColor = true;
            btnContactUs.Click += btnContactUs_Click;
            btnContactUs.Leave += btnContactUs_Leave;
            // 
            // BtnDashboard
            // 
            resources.ApplyResources(BtnDashboard, "BtnDashboard");
            BtnDashboard.FlatAppearance.BorderSize = 0;
            BtnDashboard.ForeColor = Color.FromArgb(0, 126, 249);
            BtnDashboard.Name = "BtnDashboard";
            BtnDashboard.UseVisualStyleBackColor = true;
            BtnDashboard.Click += BtnDashboard_Click_1;
            BtnDashboard.Leave += BtnDashboard_Leave;
            // 
            // panel2
            // 
            panel2.Controls.Add(label1);
            panel2.Controls.Add(Usder);
            panel2.Controls.Add(picUserAvatar);
            resources.ApplyResources(panel2, "panel2");
            panel2.Name = "panel2";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.ForeColor = Color.FromArgb(158, 161, 178);
            label1.Name = "label1";
            // 
            // Usder
            // 
            resources.ApplyResources(Usder, "Usder");
            Usder.ForeColor = Color.FromArgb(0, 126, 149);
            Usder.Name = "Usder";
            // 
            // picUserAvatar
            // 
            resources.ApplyResources(picUserAvatar, "picUserAvatar");
            picUserAvatar.Name = "picUserAvatar";
            picUserAvatar.TabStop = false;
            picUserAvatar.Click += picUserAvatar_Click;
            // 
            // panelAbout
            // 
            panelAbout.Controls.Add(panelContact);
            panelAbout.Controls.Add(panelaboutUs);
            panelAbout.Controls.Add(panelSupport);
            panelAbout.Controls.Add(panelProjects);
            panelAbout.Controls.Add(panelAboutVivy);
            resources.ApplyResources(panelAbout, "panelAbout");
            panelAbout.Name = "panelAbout";
            // 
            // panelContact
            // 
            panelContact.BackColor = Color.Transparent;
            panelContact.BackgroundImage = Properties.Resources.BackgroundBlack;
            panelContact.Controls.Add(llGithub);
            panelContact.Controls.Add(label9);
            panelContact.Controls.Add(label10);
            resources.ApplyResources(panelContact, "panelContact");
            panelContact.Name = "panelContact";
            // 
            // llGithub
            // 
            llGithub.ActiveLinkColor = Color.White;
            llGithub.BackColor = Color.Transparent;
            resources.ApplyResources(llGithub, "llGithub");
            llGithub.LinkBehavior = LinkBehavior.HoverUnderline;
            llGithub.LinkColor = Color.LightGray;
            llGithub.Name = "llGithub";
            llGithub.TabStop = true;
            llGithub.LinkClicked += llGithub_LinkClicked;
            // 
            // label9
            // 
            label9.BackColor = Color.Transparent;
            resources.ApplyResources(label9, "label9");
            label9.ForeColor = SystemColors.ButtonHighlight;
            label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            label10.BackColor = Color.Transparent;
            label10.ForeColor = Color.White;
            label10.Name = "label10";
            // 
            // panelaboutUs
            // 
            panelaboutUs.BackColor = Color.Transparent;
            resources.ApplyResources(panelaboutUs, "panelaboutUs");
            panelaboutUs.Controls.Add(label6);
            panelaboutUs.Controls.Add(label5);
            panelaboutUs.Name = "panelaboutUs";
            // 
            // label6
            // 
            label6.BackColor = Color.Transparent;
            resources.ApplyResources(label6, "label6");
            label6.ForeColor = SystemColors.ButtonHighlight;
            label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.BackColor = Color.Transparent;
            label5.ForeColor = Color.White;
            label5.Name = "label5";
            // 
            // panelSupport
            // 
            panelSupport.BackColor = Color.Transparent;
            panelSupport.BackgroundImage = Properties.Resources.BackgroundBlack;
            panelSupport.Controls.Add(label8);
            panelSupport.Controls.Add(label7);
            resources.ApplyResources(panelSupport, "panelSupport");
            panelSupport.Name = "panelSupport";
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.BackColor = Color.Transparent;
            label8.ForeColor = Color.White;
            label8.Name = "label8";
            // 
            // label7
            // 
            label7.BackColor = Color.Transparent;
            resources.ApplyResources(label7, "label7");
            label7.ForeColor = SystemColors.ButtonFace;
            label7.Name = "label7";
            // 
            // panelProjects
            // 
            panelProjects.BackColor = Color.Transparent;
            panelProjects.BackgroundImage = Properties.Resources.BackgroundBlack;
            panelProjects.Controls.Add(llOllama);
            panelProjects.Controls.Add(label14);
            panelProjects.Controls.Add(label12);
            panelProjects.Controls.Add(label4);
            resources.ApplyResources(panelProjects, "panelProjects");
            panelProjects.Name = "panelProjects";
            // 
            // llOllama
            // 
            llOllama.ActiveLinkColor = Color.White;
            resources.ApplyResources(llOllama, "llOllama");
            llOllama.BackColor = Color.Transparent;
            llOllama.ForeColor = Color.LightGray;
            llOllama.LinkBehavior = LinkBehavior.HoverUnderline;
            llOllama.LinkColor = Color.LightGray;
            llOllama.Name = "llOllama";
            llOllama.TabStop = true;
            llOllama.LinkClicked += llOllama_LinkClicked;
            // 
            // label14
            // 
            label14.BackColor = Color.Transparent;
            resources.ApplyResources(label14, "label14");
            label14.ForeColor = SystemColors.ButtonHighlight;
            label14.Name = "label14";
            // 
            // label12
            // 
            label12.BackColor = Color.Transparent;
            resources.ApplyResources(label12, "label12");
            label12.ForeColor = SystemColors.ButtonHighlight;
            label12.Name = "label12";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.BackColor = Color.Transparent;
            label4.ForeColor = Color.White;
            label4.Name = "label4";
            label4.UseCompatibleTextRendering = true;
            // 
            // panelAboutVivy
            // 
            panelAboutVivy.BackColor = Color.Transparent;
            panelAboutVivy.BackgroundImage = Properties.Resources.BackgroundBlack;
            panelAboutVivy.Controls.Add(lblAboutTitle);
            panelAboutVivy.Controls.Add(pictureBox2);
            panelAboutVivy.Controls.Add(lblAboutText);
            panelAboutVivy.Controls.Add(label3);
            panelAboutVivy.Controls.Add(label2);
            resources.ApplyResources(panelAboutVivy, "panelAboutVivy");
            panelAboutVivy.Name = "panelAboutVivy";
            // 
            // lblAboutTitle
            // 
            resources.ApplyResources(lblAboutTitle, "lblAboutTitle");
            lblAboutTitle.BackColor = Color.Transparent;
            lblAboutTitle.ForeColor = Color.White;
            lblAboutTitle.Name = "lblAboutTitle";
            // 
            // pictureBox2
            // 
            resources.ApplyResources(pictureBox2, "pictureBox2");
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Name = "pictureBox2";
            pictureBox2.TabStop = false;
            // 
            // lblAboutText
            // 
            lblAboutText.BackColor = Color.Transparent;
            resources.ApplyResources(lblAboutText, "lblAboutText");
            lblAboutText.ForeColor = Color.LightGray;
            lblAboutText.Name = "lblAboutText";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.BackColor = Color.Transparent;
            label3.ForeColor = Color.Gray;
            label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.BackColor = Color.Transparent;
            label2.ForeColor = Color.LightGray;
            label2.Name = "label2";
            // 
            // panelSettings
            // 
            panelSettings.BackColor = Color.FromArgb(46, 51, 72);
            panelSettings.Controls.Add(btnLogout);
            panelSettings.Controls.Add(cbModel);
            panelSettings.Controls.Add(label11);
            panelSettings.Controls.Add(cbSpeakResponses);
            panelSettings.Controls.Add(btnSaveSettings);
            panelSettings.Controls.Add(lblTheme);
            panelSettings.Controls.Add(cbTheme);
            panelSettings.Controls.Add(cbLanguage);
            panelSettings.Controls.Add(lblLanguage);
            panelSettings.Controls.Add(lblSettingsTitle);
            resources.ApplyResources(panelSettings, "panelSettings");
            panelSettings.Name = "panelSettings";
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(24, 30, 54);
            btnLogout.ForeColor = Color.White;
            resources.ApplyResources(btnLogout, "btnLogout");
            btnLogout.Name = "btnLogout";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // cbModel
            // 
            cbModel.DropDownStyle = ComboBoxStyle.DropDownList;
            cbModel.FormattingEnabled = true;
            cbModel.Items.AddRange(new object[] { resources.GetString("cbModel.Items"), resources.GetString("cbModel.Items1") });
            resources.ApplyResources(cbModel, "cbModel");
            cbModel.Name = "cbModel";
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            label11.ForeColor = Color.White;
            label11.Name = "label11";
            // 
            // cbSpeakResponses
            // 
            cbSpeakResponses.Checked = true;
            cbSpeakResponses.CheckState = CheckState.Checked;
            resources.ApplyResources(cbSpeakResponses, "cbSpeakResponses");
            cbSpeakResponses.ForeColor = Color.White;
            cbSpeakResponses.Name = "cbSpeakResponses";
            cbSpeakResponses.UseVisualStyleBackColor = true;
            // 
            // btnSaveSettings
            // 
            btnSaveSettings.BackColor = Color.FromArgb(24, 30, 54);
            btnSaveSettings.ForeColor = Color.White;
            resources.ApplyResources(btnSaveSettings, "btnSaveSettings");
            btnSaveSettings.Name = "btnSaveSettings";
            btnSaveSettings.UseVisualStyleBackColor = false;
            btnSaveSettings.Click += btnSaveSettings_Click;
            // 
            // lblTheme
            // 
            resources.ApplyResources(lblTheme, "lblTheme");
            lblTheme.ForeColor = Color.White;
            lblTheme.Name = "lblTheme";
            // 
            // cbTheme
            // 
            cbTheme.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTheme.FormattingEnabled = true;
            cbTheme.Items.AddRange(new object[] { resources.GetString("cbTheme.Items"), resources.GetString("cbTheme.Items1") });
            resources.ApplyResources(cbTheme, "cbTheme");
            cbTheme.Name = "cbTheme";
            // 
            // cbLanguage
            // 
            cbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLanguage.FormattingEnabled = true;
            cbLanguage.Items.AddRange(new object[] { resources.GetString("cbLanguage.Items"), resources.GetString("cbLanguage.Items1"), resources.GetString("cbLanguage.Items2") });
            resources.ApplyResources(cbLanguage, "cbLanguage");
            cbLanguage.Name = "cbLanguage";
            // 
            // lblLanguage
            // 
            resources.ApplyResources(lblLanguage, "lblLanguage");
            lblLanguage.ForeColor = Color.White;
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Tag = " ";
            // 
            // lblSettingsTitle
            // 
            resources.ApplyResources(lblSettingsTitle, "lblSettingsTitle");
            lblSettingsTitle.ForeColor = Color.White;
            lblSettingsTitle.Name = "lblSettingsTitle";
            // 
            // panelInput
            // 
            panelInput.BackColor = Color.FromArgb(40, 40, 40);
            panelInput.Controls.Add(btnSend);
            panelInput.Controls.Add(textBoxInput);
            resources.ApplyResources(panelInput, "panelInput");
            panelInput.Name = "panelInput";
            // 
            // btnSend
            // 
            btnSend.BackColor = Color.FromArgb(60, 60, 60);
            resources.ApplyResources(btnSend, "btnSend");
            btnSend.ForeColor = Color.White;
            btnSend.Name = "btnSend";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // textBoxInput
            // 
            textBoxInput.BackColor = SystemColors.WindowFrame;
            textBoxInput.BorderStyle = BorderStyle.None;
            resources.ApplyResources(textBoxInput, "textBoxInput");
            textBoxInput.ForeColor = Color.White;
            textBoxInput.Name = "textBoxInput";
            // 
            // panelVivy
            // 
            panelVivy.Controls.Add(label15);
            panelVivy.Controls.Add(btnClearChat);
            panelVivy.Controls.Add(btnNewChat);
            panelVivy.Controls.Add(label13);
            panelVivy.Controls.Add(labelvivy);
            panelVivy.Controls.Add(panelHistory);
            panelVivy.Controls.Add(panelInput);
            panelVivy.Controls.Add(richTextBox1);
            resources.ApplyResources(panelVivy, "panelVivy");
            panelVivy.Name = "panelVivy";
            // 
            // label15
            // 
            resources.ApplyResources(label15, "label15");
            label15.ForeColor = Color.White;
            label15.Name = "label15";
            // 
            // btnClearChat
            // 
            btnClearChat.BackColor = Color.FromArgb(24, 30, 54);
            btnClearChat.ForeColor = Color.White;
            resources.ApplyResources(btnClearChat, "btnClearChat");
            btnClearChat.Name = "btnClearChat";
            btnClearChat.UseVisualStyleBackColor = false;
            btnClearChat.Click += btnDeleteChat_Click;
            // 
            // btnNewChat
            // 
            btnNewChat.BackColor = Color.FromArgb(24, 30, 54);
            btnNewChat.ForeColor = Color.White;
            resources.ApplyResources(btnNewChat, "btnNewChat");
            btnNewChat.Name = "btnNewChat";
            btnNewChat.UseVisualStyleBackColor = false;
            btnNewChat.Click += btnNewChat_Click;
            // 
            // label13
            // 
            resources.ApplyResources(label13, "label13");
            label13.ForeColor = Color.White;
            label13.Name = "label13";
            // 
            // labelvivy
            // 
            resources.ApplyResources(labelvivy, "labelvivy");
            labelvivy.ForeColor = Color.White;
            labelvivy.Name = "labelvivy";
            // 
            // panelHistory
            // 
            panelHistory.Controls.Add(listBoxHistory);
            resources.ApplyResources(panelHistory, "panelHistory");
            panelHistory.Name = "panelHistory";
            // 
            // listBoxHistory
            // 
            listBoxHistory.BackColor = Color.FromArgb(46, 51, 73);
            resources.ApplyResources(listBoxHistory, "listBoxHistory");
            listBoxHistory.ForeColor = Color.White;
            listBoxHistory.FormattingEnabled = true;
            listBoxHistory.Name = "listBoxHistory";
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = Color.FromArgb(46, 51, 73);
            richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            resources.ApplyResources(richTextBox1, "richTextBox1");
            richTextBox1.ForeColor = Color.White;
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            // 
            // FrmMain
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(46, 51, 73);
            Controls.Add(pnlNaw);
            Controls.Add(panelSettings);
            Controls.Add(panelAbout);
            Controls.Add(panelVivy);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmMain";
            Load += FrmMain_Load;
            pnlNaw.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picUserAvatar).EndInit();
            panelAbout.ResumeLayout(false);
            panelContact.ResumeLayout(false);
            panelContact.PerformLayout();
            panelaboutUs.ResumeLayout(false);
            panelaboutUs.PerformLayout();
            panelSupport.ResumeLayout(false);
            panelSupport.PerformLayout();
            panelProjects.ResumeLayout(false);
            panelProjects.PerformLayout();
            panelAboutVivy.ResumeLayout(false);
            panelAboutVivy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panelSettings.ResumeLayout(false);
            panelSettings.PerformLayout();
            panelInput.ResumeLayout(false);
            panelInput.PerformLayout();
            panelVivy.ResumeLayout(false);
            panelVivy.PerformLayout();
            panelHistory.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion


        private Panel pnlNaw;
        private Panel panel2;
        private PictureBox picUserAvatar;
        private Label Usder;
        private Label label1;
        private Button BtnDashboard;
        private Button btnsettings;
        private Button btnContactUs;
        private Panel Pnlscroll;
        private Panel panelAbout;
        private Panel panelVivy;
        private Panel panelSettings;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Panel panelInput;
        private TextBox textBoxInput;
        private Button btnSend;
        private RichTextBox richTextBox1;
        private Panel panelHistory;
        private ListBox listBoxHistory;
        private Label lblLanguage;
        private Label lblSettingsTitle;
        private ComboBox cbLanguage;
        private Label lblTheme;
        private ComboBox cbTheme;
        private Button btnSaveSettings;
        private CheckBox cbSpeakResponses;
        private ToolTip toolTip1;
        private ToolTip toolTip2;
        private ToolTip toolTip3;
        private ComboBox cbModel;
        private Label label11;
        private Button btnLogout;
        private Label label13;
        private Label labelvivy;
        private Button btnClearChat;
        private Button btnNewChat;
        private Label label15;
        private Panel panelContact;
        private LinkLabel llGithub;
        private Label label9;
        private Label label10;
        private Panel panelaboutUs;
        private Label label6;
        private Label label5;
        private Panel panelSupport;
        private Label label8;
        private Label label7;
        private Panel panelProjects;
        private Label label4;
        private LinkLabel llOllama;
        private Panel panelAboutVivy;
        private Label lblAboutTitle;
        private PictureBox pictureBox2;
        private Label lblAboutText;
        private Label label3;
        private Label label2;
        private Label label12;
        private Label label14;
    }
}
