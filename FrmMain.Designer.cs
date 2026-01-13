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
            panelaboutUs = new Panel();
            panelSupport = new Panel();
            panelProjects = new Panel();
            panelAboutVivy = new Panel();
            pictureBox2 = new PictureBox();
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
            resources.ApplyResources(panelContact, "panelContact");
            panelContact.Name = "panelContact";
            // 
            // panelaboutUs
            // 
            panelaboutUs.BackColor = Color.Transparent;
            resources.ApplyResources(panelaboutUs, "panelaboutUs");
            panelaboutUs.Name = "panelaboutUs";
            // 
            // panelSupport
            // 
            panelSupport.BackColor = Color.Transparent;
            panelSupport.BackgroundImage = Properties.Resources.BackgroundBlack;
            resources.ApplyResources(panelSupport, "panelSupport");
            panelSupport.Name = "panelSupport";
            // 
            // panelProjects
            // 
            panelProjects.BackColor = Color.Transparent;
            panelProjects.BackgroundImage = Properties.Resources.BackgroundBlack;
            resources.ApplyResources(panelProjects, "panelProjects");
            panelProjects.Name = "panelProjects";
            // 
            // panelAboutVivy
            // 
            panelAboutVivy.BackColor = Color.Transparent;
            panelAboutVivy.BackgroundImage = Properties.Resources.BackgroundBlack;
            panelAboutVivy.Controls.Add(pictureBox2);
            resources.ApplyResources(panelAboutVivy, "panelAboutVivy");
            panelAboutVivy.Name = "panelAboutVivy";
            // 
            // pictureBox2
            // 
            resources.ApplyResources(pictureBox2, "pictureBox2");
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Name = "pictureBox2";
            pictureBox2.TabStop = false;
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
            cbModel.Items.AddRange(new object[] { resources.GetString("cbModel.Items"), resources.GetString("cbModel.Items1"), resources.GetString("cbModel.Items2"), resources.GetString("cbModel.Items3") });
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
            // 
            // btnNewChat
            // 
            btnNewChat.BackColor = Color.FromArgb(24, 30, 54);
            btnNewChat.ForeColor = Color.White;
            resources.ApplyResources(btnNewChat, "btnNewChat");
            btnNewChat.Name = "btnNewChat";
            btnNewChat.UseVisualStyleBackColor = false;
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
            Controls.Add(panelVivy);
            Controls.Add(panelSettings);
            Controls.Add(panelAbout);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmMain";
            Load += FrmMain_Load;
            pnlNaw.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picUserAvatar).EndInit();
            panelAbout.ResumeLayout(false);
            panelAboutVivy.ResumeLayout(false);
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
        private Panel panelaboutUs;
        private Panel panelSupport;
        private Panel panelProjects;
        private Panel panelAboutVivy;
        private PictureBox pictureBox2;
    }
}
