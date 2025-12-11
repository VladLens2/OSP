using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WinForms;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.AI;
using Microsoft.VisualBasic.Logging;
using OllamaSharp;
using SkiaSharp;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
namespace Vivy
{
    public partial class FrmMain : Form
    {
        private List<DateTime> messageTimestamps = new List<DateTime>();

        private string currentLogin;
        private Color activeButtonColor;
        private Color inactiveButtonColor;

        // Усередині класу FrmMain
        private Color sideButtonTextColor = Color.FromArgb(0, 126, 249); // за промовчанням для темної теми
        private Color panelElementTextColor = Color.White;                // за замовчуванням для елементів панелей
        private Color userNameTextColor = Color.FromArgb(0, 126, 149);   // за промовчуванням для імені користувача

        // Для бічних кнопок
        private Color sideButtonTextColorDark = Color.FromArgb(0, 126, 249);
        private Color sideButtonTextColorLight = Color.Black;


        private Color panelElementTextColorDark = Color.White;
        private Color panelElementTextColorLight = Color.Black;


        private Color userNameTextColorDark = Color.FromArgb(0, 126, 149);
        private Color userNameTextColorLight = Color.Black;

        // Громадські властивості зміни з коду
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

        // Імпорт функції для створення області з заокругленими кутами для форми
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
        private static CultureInfo GetCultureFromLanguage(string language)
        {
            return language switch
            {
                "English" => new CultureInfo("en"),
                "Deutsch" => new CultureInfo("de"),
                "Українська" => new CultureInfo("uk"),
                _ => new CultureInfo("uk")
            };
        }


        public FrmMain(string login)
        {
            InitializeComponent(); // Спочатку ініціалізація компонентів

            currentLogin = login;

            AddWindowControlButtons();

            // Застосовуємо заокруглення кутів до вікна
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            // Встановлюємо положення та розмір панелі-індикатора для кнопки Dashboard
            Pnlscroll.Height = BtnDashboard.Height;
            Pnlscroll.Top = BtnDashboard.Top;
            Pnlscroll.Left = BtnDashboard.Left;
            BtnDashboard.BackColor = Color.FromArgb(46, 51, 73);

            // Apply initial colors
            SideButtonTextColor = Color.FromArgb(0, 126, 249);
            PanelElementTextColor = Color.White;
            UserNameTextColor = Color.FromArgb(0, 126, 149);


            RestoreCustomUI();




            textBoxInput.KeyDown += textBoxInput_KeyDown;
        }
        private Dictionary<string, List<(string sender, string text, DateTime sentAt)>> chatHistory = new();
        private string currentChatTitle = "";

        // Подія завантаження форми
        private void FrmMain_Load(object sender, EventArgs e)
        {


            

            LoadAndApplyUserSettings();

            // Заокруглюємо кути панелі вводу
            RoundPanelCorners(panelInput, 10);

            // Встановлюємо текст для LinkLabel 
            linkLabel1.Text =
                "• CrossLang — багатомовний перекладач з ІІ\n" +
                "• StreamMind — генерація сценаріїв для YouTube\n" +
                "• ZenNote — мінімалістичний трекер звичок";

            // Очищаємо старі посилання (на всякий випадок)
            linkLabel1.Links.Clear();

            // Додаємо посилання до відповідних сервісів
            linkLabel1.Links.Add(2, 9, "https://crosslang.com");
            linkLabel1.Links.Add(44, 11, "https://streammind.com");
            linkLabel1.Links.Add(92, 8, "https://zennote.com");

            // Налаштовуємо кольори посилань
            linkLabel1.LinkColor = Color.LightGray;
            linkLabel1.ActiveLinkColor = Color.Black;
            linkLabel1.VisitedLinkColor = Color.LightGray;
            linkLabel1.LinkBehavior = LinkBehavior.HoverUnderline;

            // Додаємо посилання для підтримки
            linkSupportCard.Links.Clear();
            linkSupportCard.Links.Add(0, linkSupportCard.Text.Length, "https://send.monobank.ua/jar/4441114498935962");
            Usder.Text = currentLogin;
            LoadUserAvatar();

            toolTip1.SetToolTip(cbNotifications, "Надсилати сповіщення про нові функції або повідомлення.");
            toolTip1.SetToolTip(cbSpeakResponses, "Озвучувати відповіді асистента голосом.");
            toolTip1.SetToolTip(cbSaveHistory, "Зберігати історію ваших чатів, поки ви не видалите її вручну.");



            RoundPanelCorners(panelAboutVivy, 15);
            RoundPanelCorners(panelProjects, 15);
            RoundPanelCorners(panelContact, 15);
            RoundPanelCorners(panelSupport, 15);
            RoundPanelCorners(panelaboutUs, 15);
            //  всі панелі, які мають бути закруглені

            UpdateAboutPanelsTheme();


           

            var darkBackground = SKColors.Transparent;
            var darkText = SKColors.White;

            var analyticsBackgroundColor = selectedTheme == "Світла"
    ? new SKColor(245, 245, 245) // світлий
    : new SKColor(30, 35, 60);   // Темний


            

        }

        // Обробка натискання на різні кнопки меню для перемикання панелей
        private void BtnDashboard_Click_1(object sender, EventArgs e)
        {
            Pnlscroll.Height = BtnDashboard.Height;
            Pnlscroll.Top = BtnDashboard.Top;
            Pnlscroll.Left = BtnDashboard.Left;
            BtnDashboard.BackColor = activeButtonColor;

            panelVivy.Visible = true;
            panelAnalytics.Visible = false;
            panelCalendar.Visible = false;
            panelAbout.Visible = false;
            panelSettings.Visible = false;
        }

        private void BtnDashboard_Leave(object sender, EventArgs e)
        {
            BtnDashboard.BackColor = inactiveButtonColor;
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            Pnlscroll.Height = btnAnalytics.Height;
            Pnlscroll.Top = btnAnalytics.Top;
            btnAnalytics.BackColor = activeButtonColor;

            panelVivy.Visible = false;
            panelAnalytics.Visible = true;
            panelCalendar.Visible = false;
            panelAbout.Visible = false;
            panelSettings.Visible = false;
        }

        private void btnAnalytics_Leave(object sender, EventArgs e)
        {
            btnAnalytics.BackColor = inactiveButtonColor;
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            Pnlscroll.Height = btnCalendar.Height;
            Pnlscroll.Top = btnCalendar.Top;
            btnCalendar.BackColor = activeButtonColor;

            panelVivy.Visible = false;
            panelAnalytics.Visible = false;
            panelCalendar.Visible = true;
            panelAbout.Visible = false;
            panelSettings.Visible = false;
        }

        private void btnContactUs_Click(object sender, EventArgs e)
        {
            Pnlscroll.Height = btnContactUs.Height;
            Pnlscroll.Top = btnContactUs.Top;
            btnContactUs.BackColor = activeButtonColor;

            panelVivy.Visible = false;
            panelAnalytics.Visible = false;
            panelCalendar.Visible = false;
            panelAbout.Visible = true;
            panelSettings.Visible = false;
        }

        private void btnsettings_Click(object sender, EventArgs e)
        {
            Pnlscroll.Height = btnsettings.Height;
            Pnlscroll.Top = btnsettings.Top;
            btnsettings.BackColor = activeButtonColor;

            panelVivy.Visible = false;
            panelAnalytics.Visible = false;
            panelCalendar.Visible = false;
            panelAbout.Visible = false;
            panelSettings.Visible = true;
        }

        // Відновлення стандартного кольору кнопок при втраті фокусу
        private void btnCalendar_Leave(object sender, EventArgs e)
        {
            btnCalendar.BackColor = inactiveButtonColor;
        }
        private void btnContactUs_Leave(object sender, EventArgs e)
        {
            btnContactUs.BackColor = inactiveButtonColor;
        }
        private void btnsettings_Leave(object sender, EventArgs e)
        {
            btnsettings.BackColor = inactiveButtonColor;
        }

        // Метод для заокруглення кутів панелі
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

        // Обробка кліку по посиланню (відкриває у браузері)
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Link?.LinkData is string url && !string.IsNullOrEmpty(url))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
        }

        private void linkSupportCard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Link?.LinkData is string url && !string.IsNullOrEmpty(url))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
        }

        // Асинхронний метод для отримання відповіді від GPT API
        private async Task<string> GetGPTResponse(string userMessage)
        {
            IChatClient chatClient =
    new OllamaApiClient(new Uri("http://localhost:11434/"), "gpt-oss:20b");

            // Start the conversation with context for the AI model
            List<ChatMessage> chatHistory = new();

            while (true)
            {
                // Get user prompt and add to chat history
                Console.WriteLine("Your prompt:");
                var userPrompt = userMessage;
                chatHistory.Add(new ChatMessage(ChatRole.User, userPrompt));

                // Stream the AI response and add to chat history
                Console.WriteLine("AI Response:");
                var response = "";
                await foreach (ChatResponseUpdate item in
                    chatClient.GetStreamingResponseAsync(chatHistory))
                {
                    Console.Write(item.Text);
                    response += item.Text;
                }
                chatHistory.Add(new ChatMessage(ChatRole.Assistant, response));
                return response;
            }
        }




        // Обробка натискання кнопки "Відправити" (Send)
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

            string gptResponse = await GetGPTResponse(userMessage);
            DateTime sentAt = DateTime.Now;
            chatHistory[currentChatTitle].Add(("Vivy", gptResponse, sentAt));
            messageTimestamps.Add(sentAt);


            Color mainTextColor = selectedTheme.Trim().StartsWith("Світла", StringComparison.OrdinalIgnoreCase)
                ? Color.Black
                : Color.White;

            richTextBox1.SelectionColor = Color.DeepSkyBlue;
            richTextBox1.AppendText("Ви: ");
            richTextBox1.SelectionColor = mainTextColor;
            richTextBox1.AppendText(userMessage + "\n\n");

            textBoxInput.Clear();

            var now = DateTime.Now;
            chatHistory[currentChatTitle].Add(("Vivy", gptResponse, now));
            SaveSingleMessageToDb(currentChatTitle, "Vivy", gptResponse, now);

            messageTimestamps.Add(now);

            richTextBox1.SelectionColor = Color.MediumPurple;
            richTextBox1.AppendText("Vivy: ");
            richTextBox1.SelectionColor = mainTextColor;
            richTextBox1.AppendText(gptResponse + "\n\n");

            if (cbSpeakResponses.Checked)
            {
                synthesizer.SpeakAsync(gptResponse);
            }

            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();




            SaveChatHistoryToDb();

        }

        private void SaveSingleMessageToDb(string chatTitle, string sender, string text, DateTime sentAt)
        {
            int userId = GetUserIdByLogin(currentLogin);
            if (userId == -1) return;

            string connectionString = "Data Source=vivy.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            // Знайти ID чату
            string selectChatId = "SELECT Id FROM Chats WHERE Title = @title AND (User1Id = @userId OR User2Id = @userId)";
            using var cmdChat = new SqliteCommand(selectChatId, connection);
            cmdChat.Parameters.AddWithValue("@title", chatTitle);
            cmdChat.Parameters.AddWithValue("@userId", userId);

            object result = cmdChat.ExecuteScalar();
            int chatId;

            if (result == null)
            {
                // Чат не знайдено - створюємо
                string insertChat = "INSERT INTO Chats (User1Id, User2Id, Title) VALUES (@u1, @u2, @title); SELECT last_insert_rowid();";
                using var insertCmd = new SqliteCommand(insertChat, connection);
                insertCmd.Parameters.AddWithValue("@u1", userId);
                insertCmd.Parameters.AddWithValue("@u2", userId);
                insertCmd.Parameters.AddWithValue("@title", chatTitle);
                chatId = Convert.ToInt32(insertCmd.ExecuteScalar());
            }
            else
            {
                chatId = Convert.ToInt32(result);
            }

            int senderId = GetUserIdByLogin(sender);
            if (senderId == -1) senderId = userId;

            // Додаємо повідомлення
            string insertMsg = "INSERT INTO Messages (ChatId, SenderId, Text, SentAt) VALUES (@chatId, @senderId, @text, @sentAt)";
            using var cmdMsg = new SqliteCommand(insertMsg, connection);
            cmdMsg.Parameters.AddWithValue("@chatId", chatId);
            cmdMsg.Parameters.AddWithValue("@senderId", senderId);
            cmdMsg.Parameters.AddWithValue("@text", text);
            cmdMsg.Parameters.AddWithValue("@sentAt", sentAt);
            cmdMsg.ExecuteNonQuery();
        }



        private void listBoxHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxHistory.SelectedItem == null) return;
            var selected = listBoxHistory.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selected)) return;
            currentChatTitle = selected;

            if (!chatHistory.ContainsKey(currentChatTitle))
            {
                chatHistory[currentChatTitle] = new List<(string sender, string text, DateTime sentAt)>();

            }

            RedrawChatHistory();
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
                // Використовуємо стандартний аватар із ресурсів
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


                // Застосовуємо тему
                ApplyTheme(theme);

                // Застосовуємо мову інтерфейсу
                string langCode = interfaceLanguage switch
                {
                    "Українська" => "uk",
                    "English" => "en",
                    "Deutsch" => "de",
                    _ => "uk"
                };
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(langCode);

                // Перестворюємо елементи керування для застосування мови
                var selectedTheme = cbTheme.SelectedItem;
                var selectedModel = cbModel.SelectedItem;
                var selectedNotifications = cbNotifications.Checked;
                var selectedSpeak = cbSpeakResponses.Checked;
                var selectedHistory = cbSaveHistory.Checked;

                this.Controls.Clear();
                InitializeComponent();
                RestoreCustomUI();
                textBoxInput.KeyDown += textBoxInput_KeyDown;
                Usder.Text = currentLogin;
                LoadUserAvatar();
                ApplyTheme(selectedTheme?.ToString() ?? string.Empty);

                cbTheme.SelectedItem = selectedTheme;
                cbModel.SelectedItem = selectedModel;
                cbNotifications.Checked = selectedNotifications;
                cbSpeakResponses.Checked = selectedSpeak;
                cbSaveHistory.Checked = selectedHistory;
                cbLanguage.SelectedItem = interfaceLanguage;


                Usder.Text = currentLogin;
                LoadUserAvatar();

                ApplyTheme(theme);

                // Зберігаємо налаштування в БД
                string connectionString = "Data Source=vivy.db";
                using var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
                connection.Open();

                string updateCmd = @"
            UPDATE Users SET 
                Theme = @theme, 
                NotificationsEnabled = @notifications, 
                SpeakResponsesEnabled = @speak, 
                SaveHistoryEnabled = @history,
                Model = @model,
                InterfaceLanguage = @interfaceLanguage
            WHERE Login = @login";
                using var cmd = new Microsoft.Data.Sqlite.SqliteCommand(updateCmd, connection);
                cmd.Parameters.AddWithValue("@theme", theme);
                cmd.Parameters.AddWithValue("@notifications", cbNotifications.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@speak", cbSpeakResponses.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@history", cbSaveHistory.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@model", model);
                cmd.Parameters.AddWithValue("@interfaceLanguage", interfaceLanguage);
                cmd.Parameters.AddWithValue("@login", currentLogin);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Налаштування збережено!", "Vivy", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadChatHistoryFromDb();
        }


        private void AddWindowControlButtons()
        {
            // Створення кнопки "Згорнути"
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

            // Створення кнопки "закрити"
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

            // Додаємо кнопки на форму (будуть поверх усіх панелей)
            this.Controls.Add(btnMinimize);
            this.Controls.Add(btnClose);
            btnMinimize.BringToFront();
            btnClose.BringToFront();

            ChangeLocalisation();
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

        private string selectedTheme = "Темна"; // За замовчуванням

        private void ApplyTheme(string theme)
        {
            selectedTheme = theme;
            Color backColor, foreColor, buttonBack;

            // Вибір кольорів для поточної теми
            Color sideButtonColor = theme == "Світла" ? sideButtonTextColorLight : sideButtonTextColorDark;
            Color panelElementColor = theme == "Світла" ? panelElementTextColorLight : panelElementTextColorDark;
            Color userNameColor = theme == "Світла" ? userNameTextColorLight : userNameTextColorDark;

            if (theme == "Світла")
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

            panel2.BackColor = theme == "Світла" ? Color.LightGray : Color.FromArgb(24, 30, 54);
            pnlNaw.BackColor = theme == "Світла" ? Color.LightGray : Color.FromArgb(24, 30, 54);

            if (!string.IsNullOrEmpty(currentChatTitle) && chatHistory.ContainsKey(currentChatTitle))
                RedrawChatHistory();

            UpdateAboutPanelsTheme();
            ApplyAnalyticsTheme(theme);
        }

        private void ApplyThemeToControl(Control ctrl, Color backColor, Color foreColor, Color buttonBack, Color sideButtonColor, Color panelElementColor, Color userNameColor)
        {
            if (ctrl is Panel panel)
            {
                panel.BackColor = backColor;
            }
            else if (ctrl is Label label)
            {
                // Ім'я користувача (Usder)
                if (label.Name == "Usder")
                    label.ForeColor = userNameColor;
                else
                    label.ForeColor = panelElementColor;
            }
            else if (ctrl is Button btn)
            {
                btn.BackColor = buttonBack;
                // Бічні кнопки
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
                // Тільки фон, без ForeColor!
                if (rtb == richTextBox1 && panelVivy.Controls.Contains(rtb))
                {
                    if (selectedTheme == "Світла")
                        rtb.BackColor = Color.White;
                    else
                        rtb.BackColor = Color.FromArgb(46, 51, 73);
                    // Не змінюємо rtb.ForeColor!
                }
                else
                {
                    rtb.BackColor = backColor;
                    // Не змінюємо rtb.ForeColor!
                }
            }
            else if (ctrl is ListBox lb)
            {
                // Для listBoxHistory в panelHistory
                if (lb == listBoxHistory && panelHistory.Controls.Contains(lb))
                {
                    if (selectedTheme == "Світла")
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

            // Рекурсивно всім дочірніх контролів
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

            string selectCmd = "SELECT Theme, NotificationsEnabled, SpeakResponsesEnabled, SaveHistoryEnabled, Model, InterfaceLanguage FROM Users WHERE Login = @login";
            using var cmd = new Microsoft.Data.Sqlite.SqliteCommand(selectCmd, connection);
            cmd.Parameters.AddWithValue("@login", currentLogin);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string theme = reader.IsDBNull(0) ? "Темна" : reader.GetString(0);
                bool notifications = !reader.IsDBNull(1) && reader.GetInt32(1) == 1;
                bool speak = !reader.IsDBNull(2) && reader.GetInt32(2) == 1;
                bool saveHistory = !reader.IsDBNull(3) && reader.GetInt32(3) == 1;
                string model = reader.IsDBNull(4) ? "gpt-3.5-turbo" : reader.GetString(4);
                string interfaceLanguage = reader.IsDBNull(5) ? "Українська" : reader.GetString(5);

                cbTheme.SelectedItem = theme;
                cbNotifications.Checked = notifications;
                cbSpeakResponses.Checked = speak;
                cbSaveHistory.Checked = saveHistory;
                cbModel.SelectedItem = model;

                cbLanguage.SelectedItem = interfaceLanguage switch
                {
                    "uk" or "uk-UA" => "Українська",
                    "en" => "English",
                    "de" => "Deutsch",
                    _ => "Українська"
                };

                var culture = GetCultureFromLanguage(interfaceLanguage);
                Thread.CurrentThread.CurrentUICulture = culture;
                Thread.CurrentThread.CurrentCulture = culture;

                ApplyTheme(theme);
            }
        }


        private void RedrawChatHistory()
        {
            if (string.IsNullOrEmpty(currentChatTitle) || !chatHistory.ContainsKey(currentChatTitle))
                return;

            richTextBox1.Clear();
            Color mainTextColor = selectedTheme.Trim().StartsWith("Світла", StringComparison.OrdinalIgnoreCase)
                ? Color.Black
                : Color.White;

            var messages = chatHistory[currentChatTitle];
            for (int i = 0; i < messages.Count; i++)
            {
                var (senderName, message, sentAt) = messages[i];
                if (i % 2 == 1) // кожне друге повідомлення - Vivy
                {
                    richTextBox1.SelectionColor = Color.MediumPurple;
                    richTextBox1.AppendText("Vivy: ");
                }
                else // кожне перше - користувач
                {
                    richTextBox1.SelectionColor = Color.DeepSkyBlue;
                    richTextBox1.AppendText($"{currentLogin}: ");
                }
                richTextBox1.SelectionColor = mainTextColor;
                richTextBox1.AppendText(message + "\n\n");
            }
        }

        private void ChangeLocalisation()
        {
            if (cbLanguage.SelectedItem is not string selectedCulture || string.IsNullOrWhiteSpace(selectedCulture))
                return;

            var culture = GetCultureFromLanguage(selectedCulture);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;


            this.Controls.Clear();
            InitializeComponent();
            RestoreCustomUI();
            textBoxInput.KeyDown += textBoxInput_KeyDown;

            Usder.Text = currentLogin;
            LoadUserAvatar();

            ApplyTheme(selectedTheme);

            // зберігає вибір відразу
            string connectionString = "Data Source=vivy.db";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var cmd = new SqliteCommand("UPDATE Users SET InterfaceLanguage = @lang WHERE Login = @login", connection);
            cmd.Parameters.AddWithValue("@lang", culture.Name);
            cmd.Parameters.AddWithValue("@login", currentLogin);
            //cmd.ExecuteNonQuery();


        }

        private void btnNewChat_Click(object sender, EventArgs e)
        {
            // Очистити поле та скинути заголовок чату
            textBoxInput.Clear();
            richTextBox1.Clear();
            listBoxHistory.ClearSelected();
            currentChatTitle = "";
        }

        private void btnClearChat_Click_1(object sender, EventArgs e)
        {
            if (listBoxHistory.SelectedItem != null)
            {
                string selectedChat = listBoxHistory.SelectedItem.ToString();

                // Видаляємо з бази даних
                int userId = GetUserIdByLogin(currentLogin);
                if (userId != -1)
                {
                    string connectionString = "Data Source=vivy.db";
                    using var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
                    connection.Open();

                    // Отримуємо Id чату за назвою та користувачем
                    string selectChatId = "SELECT Id FROM Chats WHERE Title = @title AND (User1Id = @userId OR User2Id = @userId)";
                    using var cmdSelect = new Microsoft.Data.Sqlite.SqliteCommand(selectChatId, connection);
                    cmdSelect.Parameters.AddWithValue("@title", selectedChat);
                    cmdSelect.Parameters.AddWithValue("@userId", userId);
                    var chatIdObj = cmdSelect.ExecuteScalar();

                    if (chatIdObj != null)
                    {
                        int chatId = Convert.ToInt32(chatIdObj);

                        // Видаляємо повідомлення цього чату
                        string deleteMessages = "DELETE FROM Messages WHERE ChatId = @chatId";
                        using (var cmdDelMsg = new Microsoft.Data.Sqlite.SqliteCommand(deleteMessages, connection))
                        {
                            cmdDelMsg.Parameters.AddWithValue("@chatId", chatId);
                            cmdDelMsg.ExecuteNonQuery();
                        }

                        // Видаляємо сам чат
                        string deleteChat = "DELETE FROM Chats WHERE Id = @chatId";
                        using (var cmdDelChat = new Microsoft.Data.Sqlite.SqliteCommand(deleteChat, connection))
                        {
                            cmdDelChat.Parameters.AddWithValue("@chatId", chatId);
                            cmdDelChat.ExecuteNonQuery();
                        }
                    }
                }

                // Видаляємо з історії
                if (chatHistory.ContainsKey(selectedChat))
                {
                    chatHistory.Remove(selectedChat);
                }

                // Видаляємо зі списку
                listBoxHistory.Items.Remove(selectedChat);

                // Очищаємо поле повідомлень
                richTextBox1.Clear();
                currentChatTitle = "";
            }
        }

       
        private List<Event> allEvents = new();







        


       

        


           
        
        

        private void UpdateAboutPanelsTheme()
        {
            // Кольори для світлої та темної теми
            Color checkBoxForeColor = selectedTheme == "Світла" ? Color.Black : Color.White;
            Color checkBoxBackColor = selectedTheme == "Світла" ? Color.WhiteSmoke : Color.FromArgb(46, 51, 73);

            // Панелі та фонові картинки
            if (selectedTheme == "Світла")
            {
                panelAboutVivy.BackgroundImage = Properties.Resources.BackgroundWhite;
                panelProjects.BackgroundImage = Properties.Resources.BackgroundWhite;
                panelaboutUs.BackgroundImage = Properties.Resources.BackgroundWhite;
                panelContact.BackgroundImage = Properties.Resources.BackgroundWhite;
                panelSupport.BackgroundImage = Properties.Resources.BackgroundWhite;
                linkLabel1.LinkColor = Color.Blue;
                linkLabel2.LinkColor = Color.Blue;
                linkSupportCard.LinkColor = Color.Blue;
            }
            else
            {
                panelAboutVivy.BackgroundImage = Properties.Resources.BackgroundBlack;
                panelProjects.BackgroundImage = Properties.Resources.BackgroundBlack;
                panelaboutUs.BackgroundImage = Properties.Resources.BackgroundBlack;
                panelContact.BackgroundImage = Properties.Resources.BackgroundBlack;
                panelSupport.BackgroundImage = Properties.Resources.BackgroundBlack;
                linkLabel1.LinkColor = Color.Blue;
                linkLabel2.LinkColor = Color.Blue;
                linkSupportCard.LinkColor = Color.Blue;
            }

            // Чекбокси - колір тексту та фону
            cbNotifications.ForeColor = checkBoxForeColor;
            cbNotifications.BackColor = checkBoxBackColor;
            cbSpeakResponses.ForeColor = checkBoxForeColor;
            cbSpeakResponses.BackColor = checkBoxBackColor;
            cbSaveHistory.ForeColor = checkBoxForeColor;
            cbSaveHistory.BackColor = checkBoxBackColor;
        }
       


        public class Event
        {
            public DateTime Date { get; set; }
            public string Text { get; set; }
            public bool IsDone { get; set; }

            public Event(DateTime date, string text, bool isDone = false)
            {
                Date = date;
                Text = text;
                IsDone = isDone;
            }
        }

        




        private void RestoreCustomUI()
        {
            linkLabel1.Links.Clear();
            linkLabel1.Links.Add(2, 9, "https://crosslang.com");
            linkLabel1.Links.Add(44, 11, "https://streammind.com");
            linkLabel1.Links.Add(92, 8, "https://zennote.com");

            // Відновити закругленість форми
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            // Додати кастомні кнопки керування вікном
            AddWindowControlButtons();

            // Закруглити панелі (повторно)
            RoundPanelCorners(panelInput, 10);
            RoundPanelCorners(panelAboutVivy, 15);
            RoundPanelCorners(panelProjects, 15);
            RoundPanelCorners(panelContact, 15);
            RoundPanelCorners(panelSupport, 15);
            RoundPanelCorners(panelaboutUs, 15);
            // Додайте сюди всі панелі, які мають бути закруглені
        }

        




        private void ApplyAnalyticsTheme(string theme)
        {
            Color analyticsBack, analyticsFore, analyticsButtonBack, analyticsTextBoxBack, analyticsTextBoxFore;
            Image analyticsBackgroundImage;
            analyticsBack = Color.Transparent;
            if (theme == "Світла")
            {
                analyticsFore = Color.Black;
                analyticsButtonBack = Color.WhiteSmoke;
                analyticsTextBoxBack = Color.White;
                analyticsTextBoxFore = Color.Black;
                analyticsBackgroundImage = Properties.Resources.BackgroundWhite;
            }
            else
            {
                analyticsFore = Color.White;
                analyticsButtonBack = Color.FromArgb(24, 30, 54);
                analyticsTextBoxBack = Color.FromArgb(46, 51, 73);
                analyticsTextBoxFore = Color.White;
                analyticsBackgroundImage = Properties.Resources.BackgroundBlack;
            }

            // Рекурсивно змінюємо фон лише у вкладених панелей
            void SetPanelBackgrounds(Control parent)
            {
                foreach (Control ctrl in parent.Controls)
                {
                    if (ctrl is Panel p)
                        p.BackgroundImage = analyticsBackgroundImage;
                    if (ctrl.HasChildren)
                        SetPanelBackgrounds(ctrl);
                }
            }
            SetPanelBackgrounds(panelAnalytics);

            // Рекурсивна функція для застосування теми до всіх контролів
            void ApplyToAllControls(Control parent)
            {
                foreach (Control ctrl in parent.Controls)
                {
                    if (ctrl is Panel || ctrl is GroupBox)
                        ctrl.BackColor = analyticsBack;
                    if (ctrl is Label l)
                        l.ForeColor = analyticsFore;
                    if (ctrl is Button b)
                    {
                        b.BackColor = analyticsButtonBack;
                        b.ForeColor = analyticsFore;
                    }
                    if (ctrl is TextBox t)
                    {
                        t.BackColor = analyticsTextBoxBack;
                        t.ForeColor = analyticsTextBoxFore;
                    }
                    if (ctrl is ListBox lb)
                    {
                        lb.BackColor = analyticsTextBoxBack;
                        lb.ForeColor = analyticsTextBoxFore;
                    }
                    if (ctrl is LiveChartsCore.SkiaSharpView.WinForms.CartesianChart chart)
                    {
                        chart.BackColor = analyticsBack;


                    }
                    if (ctrl.HasChildren)
                        ApplyToAllControls(ctrl);
                }
            }

            ApplyToAllControls(panelAnalytics);
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


        private void LoadCalendarEventsFromDb()
        {
            allEvents.Clear();
            int userId = GetUserIdByLogin(currentLogin);
            if (userId == -1) return;

            string connectionString = "Data Source=vivy.db";
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
            connection.Open();

            string selectCmd = "SELECT Date, Title, Description, IsDone FROM Events WHERE OwnerId = @userId";
            using var cmd = new Microsoft.Data.Sqlite.SqliteCommand(selectCmd, connection);
            cmd.Parameters.AddWithValue("@userId", userId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                DateTime date = reader.GetDateTime(0);
                string text = reader.GetString(1);

                bool isDone = !reader.IsDBNull(3) && reader.GetInt32(3) == 1;
                allEvents.Add(new Event(date, text, isDone));
            }
        }

        private void SaveCalendarEventsToDb()
        {
            int userId = GetUserIdByLogin(currentLogin);
            if (userId == -1) return;

            string connectionString = "Data Source=vivy.db";
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
            connection.Open();

            // Видаляємо старі події користувача
            string deleteCmd = "DELETE FROM Events WHERE OwnerId = @userId";
            using (var cmd = new Microsoft.Data.Sqlite.SqliteCommand(deleteCmd, connection))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.ExecuteNonQuery();
            }

            // Зберігаємо всі події
            foreach (var ev in allEvents)
            {
                string insertCmd = @"
            INSERT INTO Events (Title, Date, OwnerId, IsDone)
            VALUES (@title, @date, @ownerId, @isDone)";
                using var cmd = new Microsoft.Data.Sqlite.SqliteCommand(insertCmd, connection);
                cmd.Parameters.AddWithValue("@title", ev.Text);
                cmd.Parameters.AddWithValue("@date", ev.Date);
                cmd.Parameters.AddWithValue("@ownerId", userId);
                cmd.Parameters.AddWithValue("@isDone", ev.IsDone ? 1 : 0);
                cmd.ExecuteNonQuery();
            }
        }

        private void LoadChatHistoryFromDb()
        {
            chatHistory.Clear();
            listBoxHistory.Items.Clear();
            messageTimestamps.Clear();

            string connectionString = "Data Source=vivy.db";
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
            connection.Open();

            // Завантажуємо всі повідомлення поточного користувача з прив'язкою до чату
            string selectMessages = @"
    SELECT m.Text, u.Login, m.SentAt, c.Title
    FROM Messages m
    JOIN Users u ON m.SenderId = u.Id
    JOIN Chats c ON m.ChatId = c.Id
    WHERE u.Login = @login
    ORDER BY m.SentAt;
";

            using var cmd = new Microsoft.Data.Sqlite.SqliteCommand(selectMessages, connection);
            cmd.Parameters.AddWithValue("@login", currentLogin);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string text = reader.GetString(0);
                string sender = reader.GetString(1);
                DateTime timestamp = reader.GetDateTime(2);
                string title = reader.IsDBNull(3) ? "Без названия" : reader.GetString(3);

                string debugMessage = $"[DEBUG] Чат: '{title}' | Від: {sender} | Текст: {text} | Час: {timestamp}";

                // Лог в консоль и Output окна
                Console.WriteLine(debugMessage);
                Debug.WriteLine(debugMessage);

                // Додаємо до словника чату
                if (!chatHistory.ContainsKey(title))
                    chatHistory[title] = new List<(string sender, string text, DateTime sentAt)>();

                chatHistory[title].Add((sender, text, timestamp));
                messageTimestamps.Add(timestamp);

                // Додаємо до списку історії, якщо ще немає
                if (!listBoxHistory.Items.Contains(title))
                    listBoxHistory.Items.Add(title);
            }


        }



        private void SaveChatHistoryToDb()
        {
            int userId = GetUserIdByLogin(currentLogin);
            if (userId == -1) return;

            string connectionString = "Data Source=vivy.db";
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
            connection.Open();

            // Для простоти: видаляємо всі чати користувача, зберігаємо заново
            string selectChats = "SELECT Id FROM Chats WHERE User1Id = @userId OR User2Id = @userId";
            using (var cmd = new Microsoft.Data.Sqlite.SqliteCommand(selectChats, connection))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                using var reader = cmd.ExecuteReader();
                var chatIds = new List<int>();
                while (reader.Read()) chatIds.Add(reader.GetInt32(0));
                reader.Close();

                foreach (var chatId in chatIds)
                {
                    using var delMsg = new Microsoft.Data.Sqlite.SqliteCommand("DELETE FROM Messages WHERE ChatId = @chatId", connection);
                    delMsg.Parameters.AddWithValue("@chatId", chatId);
                    delMsg.ExecuteNonQuery();

                    using var delChat = new Microsoft.Data.Sqlite.SqliteCommand("DELETE FROM Chats WHERE Id = @chatId", connection);
                    delChat.Parameters.AddWithValue("@chatId", chatId);
                    delChat.ExecuteNonQuery();
                }
            }

            // Зберігаємо чати та повідомлення
            foreach (var chat in chatHistory)
            {
                // Вставляємо чат
                string insertChat = "INSERT INTO Chats (User1Id, User2Id, Title) VALUES (@u1, @u2, @title); SELECT last_insert_rowid();";
                using var cmdChat = new Microsoft.Data.Sqlite.SqliteCommand(insertChat, connection);
                cmdChat.Parameters.AddWithValue("@u1", userId);
                cmdChat.Parameters.AddWithValue("@u2", userId); // якщо користувач і Vivy, можна userId двічі
                cmdChat.Parameters.AddWithValue("@title", chat.Key);
                long chatId = (long)cmdChat.ExecuteScalar();

                // Вставляємо повідомлення
                foreach (var (sender, text, sentAt) in chat.Value)
                {
                    int senderId = GetUserIdByLogin(sender) != -1 ? GetUserIdByLogin(sender) : userId;
                    string insertMsg = "INSERT INTO Messages (ChatId, SenderId, Text, SentAt) VALUES (@chatId, @senderId, @text, @sentAt)";
                    using var cmdMsg = new Microsoft.Data.Sqlite.SqliteCommand(insertMsg, connection);
                    cmdMsg.Parameters.AddWithValue("@chatId", chatId);
                    cmdMsg.Parameters.AddWithValue("@senderId", senderId);
                    cmdMsg.Parameters.AddWithValue("@text", text);
                    cmdMsg.Parameters.AddWithValue("@sentAt", sentAt);

                    cmdMsg.ExecuteNonQuery();
                }
            }
        }


        private void textBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // щоб не додавався переклад рядка
                btnSend.PerformClick();    // імітуємо натискання кнопки "Надіслати"
            }
        }

       
       

        private void panelVivy_VisibleChanged(object sender, EventArgs e)
        {
            if (panelVivy.Visible)
                LoadChatHistoryFromDb();
        }

        private void btnUpdateAnalytics_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadChatHistoryFromDb();

        }

        

        
    }
}
