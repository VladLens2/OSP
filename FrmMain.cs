using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WinForms;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic.Logging;
using SkiaSharp;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;
using OllamaSharp;
using Microsoft.Extensions.AI;

namespace Vivy
{
    public partial class FrmMain : Form
    {
        private List<DateTime> messageTimestamps = new List<DateTime>();

        private string currentLogin;
        private Color activeButtonColor;
        private Color inactiveButtonColor;


        private Color sideButtonTextColor = Color.FromArgb(0, 126, 249);
        private Color panelElementTextColor = Color.White;
        private Color userNameTextColor = Color.FromArgb(0, 126, 149);


        private Color sideButtonTextColorDark = Color.FromArgb(0, 126, 249);
        private Color sideButtonTextColorLight = Color.Black;


        private Color panelElementTextColorDark = Color.White;
        private Color panelElementTextColorLight = Color.Black;


        private Color userNameTextColorDark = Color.FromArgb(0, 126, 149);
        private Color userNameTextColorLight = Color.Black;


        public Color SideButtonTextColor
        {
            get => sideButtonTextColor;
            set { sideButtonTextColor = value; ApplyTheme(selectedTheme); }
        }
        public Color PanelElementTextColor
        {
            get => panelElementTextColor;
            set { panelElementTextColor = value; ApplyTheme(selectedTheme); }
        }
        public Color UserNameTextColor
        {
            get => userNameTextColor;
            set { userNameTextColor = value; ApplyTheme(selectedTheme); }
        }
        public Color SideButtonTextColorDark
        {
            get => sideButtonTextColorDark;
            set { sideButtonTextColorDark = value; ApplyTheme(selectedTheme); }
        }
        public Color SideButtonTextColorLight
        {
            get => sideButtonTextColorLight;
            set { sideButtonTextColorLight = value; ApplyTheme(selectedTheme); }
        }
        public Color PanelElementTextColorDark
        {
            get => panelElementTextColorDark;
            set { panelElementTextColorDark = value; ApplyTheme(selectedTheme); }
        }
        public Color PanelElementTextColorLight
        {
            get => panelElementTextColorLight;
            set { panelElementTextColorLight = value; ApplyTheme(selectedTheme); }
        }
        public Color UserNameTextColorDark
        {
            get => userNameTextColorDark;
            set { userNameTextColorDark = value; ApplyTheme(selectedTheme); }
        }
        public Color UserNameTextColorLight
        {
            get => userNameTextColorLight;
            set { userNameTextColorLight = value; ApplyTheme(selectedTheme); }
        }


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );



        public FrmMain(string login)
        {
            {
                InitializeComponent();

                currentLogin = login;

                AddWindowControlButtons();

                // Wendet abgerundete Ecken auf das Fenster an
                Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
                // Setzt Position und Größe des Indikator-Panels für die Dashboard-Schaltfläche
                Pnlscroll.Height = BtnDashboard.Height;
                Pnlscroll.Top = BtnDashboard.Top;
                Pnlscroll.Left = BtnDashboard.Left;
                BtnDashboard.BackColor = Color.FromArgb(46, 51, 73);

                SideButtonTextColor = Color.FromArgb(0, 126, 249);
                PanelElementTextColor = Color.White;
                UserNameTextColor = Color.FromArgb(0, 126, 149);



                RestoreCustomUI();
            }
        }
        private Dictionary<string, List<(string sender, string text, DateTime sentAt)>> chatHistory = new();
        private string currentChatTitle = "";

        private void FrmMain_Load(object sender, EventArgs e)
        {




            LoadAndApplyUserSettings();

            // Rundet die Ecken des Eingabe-Panels
            RoundPanelCorners(panelInput, 10);
            RoundPanelCorners(panelAboutVivy, 15);
            RoundPanelCorners(panelProjects, 15);
            RoundPanelCorners(panelContact, 15);
            RoundPanelCorners(panelSupport, 15);
            RoundPanelCorners(panelaboutUs, 15);


            UpdateAboutPanelsTheme();





            var darkBackground = SKColors.Transparent;
            var darkText = SKColors.White;

            var analyticsBackgroundColor = selectedTheme == "White"
            ? new SKColor(245, 245, 245) // hell
            : new SKColor(30, 35, 60);   // dunkel




        }

        // Verarbeitung des Klicks auf verschiedene Menü-Schaltflächen zum Umschalten der Panels
        private void BtnDashboard_Click_1(object sender, EventArgs e)
        {
            Pnlscroll.Height = BtnDashboard.Height;
            Pnlscroll.Top = BtnDashboard.Top;
            Pnlscroll.Left = BtnDashboard.Left;
            BtnDashboard.BackColor = activeButtonColor;

            panelVivy.Visible = true;
            panelAbout.Visible = false;
            panelSettings.Visible = false;
        }

        private void BtnDashboard_Leave(object sender, EventArgs e)
        {
            BtnDashboard.BackColor = inactiveButtonColor;
        }



        private void btnContactUs_Click(object sender, EventArgs e)
        {
            Pnlscroll.Height = btnContactUs.Height;
            Pnlscroll.Top = btnContactUs.Top;
            btnContactUs.BackColor = activeButtonColor;

            panelVivy.Visible = false;
            panelAbout.Visible = true;
            panelSettings.Visible = false;
        }

        private void btnsettings_Click(object sender, EventArgs e)
        {
            Pnlscroll.Height = btnsettings.Height;
            Pnlscroll.Top = btnsettings.Top;
            btnsettings.BackColor = activeButtonColor;

            panelVivy.Visible = false;
            panelAbout.Visible = false;
            panelSettings.Visible = true;
        }

        private void btnContactUs_Leave(object sender, EventArgs e)
        {
            btnContactUs.BackColor = inactiveButtonColor;
        }
        private void btnsettings_Leave(object sender, EventArgs e)
        {
            btnsettings.BackColor = inactiveButtonColor;
        }

        // Methode zum Abrunden der Ecken eines Panels
        private void RoundPanelCorners(Panel panel, int radius)
        {
            Rectangle bounds = new Rectangle(0, 0, panel.Width, panel.Height);
            GraphicsPath path = new GraphicsPath();
            int r = radius * 2;
            path.AddArc(bounds.X, bounds.Y, r, r, 180, 90);
            path.AddArc(bounds.Right - r, bounds.Y, r, r, 270, 90);
            path.AddArc(bounds.Right - r, bounds.Bottom - r, r, r, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - r, r, r, 90, 90);
            path.CloseAllFigures();
            panel.Region = new Region(path);
        }



        



        private async void btnSend_Click(object sender, EventArgs e)
        {
            string userMessage = textBoxInput.Text.Trim();
            if (string.IsNullOrEmpty(userMessage)) return;

            if (string.IsNullOrEmpty(currentChatTitle))
            {
                listBoxHistory.Items.Add(currentChatTitle);
                if (!chatHistory.ContainsKey(currentChatTitle))
                {
                    chatHistory[currentChatTitle] = new List<(string sender, string text, DateTime sentAt)>();
                }
            }

            Color mainTextColor = selectedTheme.Trim().StartsWith("White", StringComparison.OrdinalIgnoreCase)
                ? Color.Black
                : Color.White;

            // Benutzernachricht anzeigen
            richTextBox1.SelectionColor = Color.DeepSkyBlue;
            richTextBox1.AppendText("Sie: ");
            richTextBox1.SelectionColor = mainTextColor;
            richTextBox1.AppendText(userMessage + "\n\n");

            textBoxInput.Clear();

            DateTime sentAt = DateTime.Now;
            chatHistory[currentChatTitle].Add(("User", userMessage, sentAt));
            messageTimestamps.Add(sentAt);

            // Vivy-Label hinzufügen
            richTextBox1.SelectionColor = Color.MediumPurple;
            richTextBox1.AppendText("Vivy: ");
            richTextBox1.SelectionColor = mainTextColor;

            // Zeichen-für-Zeichen-Anzeige mit Streaming
            IChatClient chatClient = new OllamaApiClient(new Uri("http://localhost:11434/"), "phi3:mini");

            List<ChatMessage> chatHistoryForAI = new()
            {
                new ChatMessage(ChatRole.User, userMessage)
            };

            StringBuilder fullResponse = new StringBuilder();

            await foreach (ChatResponseUpdate item in chatClient.GetStreamingResponseAsync(chatHistoryForAI))
            {
                if (!string.IsNullOrEmpty(item.Text))
                {
                    richTextBox1.SelectionColor = mainTextColor;
                    richTextBox1.AppendText(item.Text);
                    fullResponse.Append(item.Text);

                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    richTextBox1.ScrollToCaret();

                    // UI-Update erzwingen für flüssige Anzeige
                    Application.DoEvents();
                }
            }

            richTextBox1.AppendText("\n\n");

            // Antwort im Chat-Verlauf speichern
            DateTime responseTime = DateTime.Now;
            string gptResponse = fullResponse.ToString();
            chatHistory[currentChatTitle].Add(("Vivy", gptResponse, responseTime));
            messageTimestamps.Add(responseTime);

            // Text-to-Speech falls aktiviert
            if (cbSpeakResponses.Checked)
            {
                synthesizer.SpeakAsync(gptResponse);
            }

            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }





        private void picUserAvatar_Click(object sender, EventArgs e)
        {
            using (var profileForm = new FrmProfile(currentLogin, selectedTheme))
            {
                if (profileForm.ShowDialog() == DialogResult.OK)
                {
                    currentLogin = profileForm.NewLogin;
                    LoadUserAvatar();
                }
            }
        }



        private void LoadUserAvatar()
        {
            string connectionString = "Data Source=vivy.db";
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
            connection.Open();

            string selectCmd = "SELECT ProfileImage FROM Users WHERE Login = @login";
            using var cmd = new Microsoft.Data.Sqlite.SqliteCommand(selectCmd, connection);
            cmd.Parameters.AddWithValue("@login", currentLogin);

            var avatarPath = cmd.ExecuteScalar() as string;
            if (!string.IsNullOrEmpty(avatarPath) && System.IO.File.Exists(avatarPath))
            {
                using var ms = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(avatarPath));
                picUserAvatar.Image = Image.FromStream(ms);
            }
            else
            {
                // Verwende Standard-Avatar aus Ressourcen
                picUserAvatar.Image = Properties.Resources.DefaultAvatar;
            }

        }



        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            if (cbTheme.SelectedItem != null && cbModel.SelectedItem != null && cbLanguage.SelectedItem != null)
            {
                string? theme = cbTheme.SelectedItem?.ToString();
                string? model = cbModel.SelectedItem?.ToString();
                string? interfaceLanguage = cbLanguage.SelectedItem?.ToString();

                if (theme == null || model == null || interfaceLanguage == null)
                {
                    return;
                }


                ApplyTheme(theme);
                var selectedTheme = cbTheme.SelectedItem;
                var selectedModel = cbModel.SelectedItem;

                var selectedSpeak = cbSpeakResponses.Checked;


                this.Controls.Clear();
                InitializeComponent();
                RestoreCustomUI();
                Usder.Text = currentLogin;
                LoadUserAvatar();
                ApplyTheme(selectedTheme?.ToString() ?? string.Empty);

                cbTheme.SelectedItem = selectedTheme;
                cbModel.SelectedItem = selectedModel;

                cbSpeakResponses.Checked = selectedSpeak;

                cbLanguage.SelectedItem = interfaceLanguage;


                Usder.Text = currentLogin;
                LoadUserAvatar();

                ApplyTheme(theme);

                // Speichere die Einstellungen in der DB
                string connectionString = "Data Source=vivy.db";
                using var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
                connection.Open();

                string updateCmd = @"
                UPDATE Users SET 
                Theme = @theme,  
                SpeakResponsesEnabled = @speak, 
                Model = @model
                WHERE Login = @login";
                using var cmd = new Microsoft.Data.Sqlite.SqliteCommand(updateCmd, connection);
                cmd.Parameters.AddWithValue("@theme", theme);

                cmd.Parameters.AddWithValue("@speak", cbSpeakResponses.Checked ? 1 : 0);

                cmd.Parameters.AddWithValue("@model", model);
                cmd.Parameters.AddWithValue("@login", currentLogin);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Änderungen gespeichert!", "Vivy", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
        }


        private void AddWindowControlButtons()
        {
            // Erstellt die "Minimieren"-Schaltfläche
            Button btnMinimize = new Button
            {
                Text = "–",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(24, 30, 54),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(30, 30),
                Location = new Point(this.Width - 70, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.Click += (s, e) => this.WindowState = FormWindowState.Minimized;

            // Erstellt die "Schließen"-Schaltfläche
            Button btnClose = new Button
            {
                Text = "×",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(24, 30, 54),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(30, 30),
                Location = new Point(this.Width - 35, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            // Fügt die Schaltflächen dem Formular hinzu (werden über allen Panels liegen)
            this.Controls.Add(btnMinimize);
            this.Controls.Add(btnClose);
            btnMinimize.BringToFront();
            btnClose.BringToFront();

        }

        private SpeechSynthesizer synthesizer = new SpeechSynthesizer();



        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            var loginForm = new FrmLogin();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                currentLogin = loginForm.UserLogin;
                Usder.Text = currentLogin;
                LoadUserAvatar();
                LoadAndApplyUserSettings();
                this.Show();
            }
            else
            {
                Application.Exit();
            }
        }

        private string selectedTheme = "Dark";

        private void ApplyTheme(string theme)
        {
            selectedTheme = theme;
            Color backColor, foreColor, buttonBack;

            // Auswahl der Farben für das aktuelle Theme
            Color sideButtonColor = theme == "White" ? sideButtonTextColorLight : sideButtonTextColorDark;
            Color panelElementColor = theme == "White" ? panelElementTextColorLight : panelElementTextColorDark;
            Color userNameColor = theme == "White" ? userNameTextColorLight : userNameTextColorDark;

            if (theme == "White")
            {
                backColor = Color.WhiteSmoke;
                foreColor = Color.Black;
                buttonBack = Color.LightGray;
                activeButtonColor = Color.Gainsboro;
                inactiveButtonColor = Color.LightGray;
            }
            else
            {
                backColor = Color.FromArgb(46, 51, 73);
                foreColor = Color.White;
                buttonBack = Color.FromArgb(24, 30, 54);
                activeButtonColor = Color.FromArgb(46, 51, 73);
                inactiveButtonColor = Color.FromArgb(24, 30, 54);
            }

            this.BackColor = backColor;

            foreach (Control control in this.Controls)
            {
                ApplyThemeToControl(control, backColor, foreColor, buttonBack, sideButtonColor, panelElementColor, userNameColor);
            }

            panel2.BackColor = theme == "White" ? Color.LightGray : Color.FromArgb(24, 30, 54);
            pnlNaw.BackColor = theme == "White" ? Color.LightGray : Color.FromArgb(24, 30, 54);

            if (!string.IsNullOrEmpty(currentChatTitle) && chatHistory.ContainsKey(currentChatTitle))
                UpdateAboutPanelsTheme();

        }

        private void ApplyThemeToControl(Control ctrl, Color backColor, Color foreColor, Color buttonBack, Color sideButtonColor, Color panelElementColor, Color userNameColor)
        {
            if (ctrl is Panel panel)
            {
                panel.BackColor = backColor;
            }
            else if (ctrl is Label label)
            {
                // Benutzername (Usder)
                if (label.Name == "Usder")
                    label.ForeColor = userNameColor;
                else
                    label.ForeColor = panelElementColor;
            }
            else if (ctrl is Button btn)
            {
                btn.BackColor = buttonBack;
                // Seiten-Buttons
                if (pnlNaw.Controls.Contains(btn))
                    btn.ForeColor = sideButtonColor;
                else
                    btn.ForeColor = panelElementColor;
            }
            else if (ctrl is ComboBox cb)
            {
                cb.BackColor = buttonBack;
                cb.ForeColor = panelElementColor;
            }
            else if (ctrl is TextBox tb)
            {
                tb.BackColor = Color.White;
                tb.ForeColor = Color.Black;
            }
            else if (ctrl is RichTextBox rtb)
            {

                if (rtb == richTextBox1 && panelVivy.Controls.Contains(rtb))
                {
                    if (selectedTheme == "White")
                        rtb.BackColor = Color.White;
                    else
                        rtb.BackColor = Color.FromArgb(46, 51, 73);

                }
                else
                {
                    rtb.BackColor = backColor;

                }
            }
            else if (ctrl is ListBox lb)
            {

                if (lb == listBoxHistory && panelHistory.Controls.Contains(lb))
                {
                    if (selectedTheme == "White")
                    {
                        lb.BackColor = Color.White;
                        lb.ForeColor = Color.Black;
                    }
                    else
                    {
                        lb.BackColor = Color.FromArgb(46, 51, 73);
                        lb.ForeColor = Color.White;
                    }
                }
                else
                {
                    lb.BackColor = backColor;
                    lb.ForeColor = foreColor;
                }
            }

            foreach (Control child in ctrl.Controls)
            {
                ApplyThemeToControl(child, backColor, foreColor, buttonBack, sideButtonColor, panelElementColor, userNameColor);
            }
        }

        private void LoadAndApplyUserSettings()
        {
            string connectionString = "Data Source=vivy.db";
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
            connection.Open();

            string selectCmd = "SELECT Theme, SpeakResponsesEnabled, Model FROM Users WHERE Login = @login";
            using var cmd = new Microsoft.Data.Sqlite.SqliteCommand(selectCmd, connection);
            cmd.Parameters.AddWithValue("@login", currentLogin);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string theme = reader.IsDBNull(0) ? "Dark" : reader.GetString(0);
                bool speak = !reader.IsDBNull(1) && reader.GetInt32(2) == 1;
                string model = reader.IsDBNull(2) ? "gpt-3.5-turbo" : reader.GetString(2);

                cbTheme.SelectedItem = theme;

                cbSpeakResponses.Checked = speak;

                cbModel.SelectedItem = model;

                ApplyTheme(theme);
            }
        }


        private void UpdateAboutPanelsTheme()
        {
            // Farben für helles und dunkles Theme
            Color checkBoxForeColor = selectedTheme == "White" ? Color.Black : Color.White;
            Color checkBoxBackColor = selectedTheme == "White" ? Color.WhiteSmoke : Color.FromArgb(46, 51, 73);

            // Panels und Hintergrundbilder
            if (selectedTheme == "White")
            {
                panelAboutVivy.BackgroundImage = Properties.Resources.BackgroundWhite;
                panelProjects.BackgroundImage = Properties.Resources.BackgroundWhite;
                panelaboutUs.BackgroundImage = Properties.Resources.BackgroundWhite;
                panelContact.BackgroundImage = Properties.Resources.BackgroundWhite;
                panelSupport.BackgroundImage = Properties.Resources.BackgroundWhite;
            }
            else
            {
                panelAboutVivy.BackgroundImage = Properties.Resources.BackgroundBlack;
                panelProjects.BackgroundImage = Properties.Resources.BackgroundBlack;
                panelaboutUs.BackgroundImage = Properties.Resources.BackgroundBlack;
                panelContact.BackgroundImage = Properties.Resources.BackgroundBlack;
                panelSupport.BackgroundImage = Properties.Resources.BackgroundBlack;
            }


            cbSpeakResponses.ForeColor = checkBoxForeColor;
            cbSpeakResponses.BackColor = checkBoxBackColor;

        }


        private void RestoreCustomUI()
        {



            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            // Füge benutzerdefinierte Fenstersteuerungs-Schaltflächen hinzu
            AddWindowControlButtons();

            // Runde die Panels
            RoundPanelCorners(panelInput, 10);
            RoundPanelCorners(panelAboutVivy, 15);
            RoundPanelCorners(panelProjects, 15);
            RoundPanelCorners(panelContact, 15);
            RoundPanelCorners(panelSupport, 15);
            RoundPanelCorners(panelaboutUs, 15);

        }


        private int GetUserIdByLogin(string login)
        {
            string connectionString = "Data Source=vivy.db";
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
            connection.Open();

            using var cmd = new Microsoft.Data.Sqlite.SqliteCommand("SELECT Id FROM Users WHERE Login = @login", connection);
            cmd.Parameters.AddWithValue("@login", login);

            var result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : -1;
        }

    }
}
