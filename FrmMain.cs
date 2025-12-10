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
        private Color userNameTextColor = Color.FromArgb(0, 126, 149);   // за промовчанням для імені користувача

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

            UpdateTopicChart();

            RestoreCustomUI();

            LoadChatHistoryFromDb();
            LoadCalendarEventsFromDb();


            textBoxInput.KeyDown += textBoxInput_KeyDown;
        }
        private Dictionary<string, List<(string sender, string text, DateTime sentAt)>> chatHistory = new();
        private string currentChatTitle = "";

        // Подія завантаження форми
        private void FrmMain_Load(object sender, EventArgs e)
        {


            chartTopics.DrawMarginFrame = new DrawMarginFrame
            {
                Stroke = null,
                Fill = new SolidColorPaint(new SKColor(30, 35, 60))
            };
            chartTopics.BackColor = Color.FromArgb(30, 35, 60);

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

            UpdateEventsForDate(monthCalendar1.SelectionStart.Date);

            RoundPanelCorners(panelAboutVivy, 15);
            RoundPanelCorners(panelProjects, 15);
            RoundPanelCorners(panelContact, 15);
            RoundPanelCorners(panelSupport, 15);
            RoundPanelCorners(panelaboutUs, 15);
            //  всі панелі, які мають бути закруглені

            UpdateAboutPanelsTheme();

            UpdateCalendarStats();

            
            cbEventFilter.SelectedIndex = 0;
            cbEventFilter.SelectedIndexChanged += (s, e) => ApplyEventFilter();

            var darkBackground = SKColors.Transparent;
            var darkText = SKColors.White;

            var analyticsBackgroundColor = selectedTheme == "Світла"
    ? new SKColor(245, 245, 245) // світлий
    : new SKColor(30, 35, 60);   // Темний


            chartTopics.XAxes = new Axis[]
            {
        new Axis
        {
            LabelsPaint = new SolidColorPaint(darkText),
            TextSize = 16
        }
            };

            chartTopics.YAxes = new Axis[]
            {
        new Axis
        {
            LabelsPaint = new SolidColorPaint(darkText),
            TextSize = 16
        }
            };

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
            string apiKey = "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";
            string apiUrl = "https://openrouter.ai/api/v1/chat/completions";

            var requestBody = new
            {
                model = "mistralai/mistral-7b-instruct", // можно заменить на другую модель
                messages = new[]
                {
            new { role = "user", content = userMessage }
        },
                temperature = 0.7,
                max_tokens = 500
            };

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                client.DefaultRequestHeaders.Add("HTTP-Referer", "https://yourapp.github.io"); // обязателен для OpenRouter
                client.DefaultRequestHeaders.Add("X-Title", "Vivy AI");

                var content = new StringContent(
                    JsonSerializer.Serialize(requestBody),
                    Encoding.UTF8,
                    "application/json"
                );

                try
                {
                    var response = await client.PostAsync(apiUrl, content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    using (JsonDocument doc = JsonDocument.Parse(responseBody))
                    {
                        var root = doc.RootElement;

                        // Проверка на наличие поля "message"
                        if (root.TryGetProperty("choices", out JsonElement choices) &&
                            choices[0].TryGetProperty("message", out JsonElement message) &&
                            message.TryGetProperty("content", out JsonElement contentValue))
                        {
                            return contentValue.GetString() ?? "Пустой ответ.";
                        }
                        else
                        {
                            return "Ошибка: ответ не содержит текст.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    return $"Ошибка: {ex.Message}";
                }
            }
        }




        // Обробка натискання кнопки "Відправити" (Send)
        private async void btnSend_Click(object sender, EventArgs e)
        {
            string userMessage = textBoxInput.Text.Trim();
            if (string.IsNullOrEmpty(userMessage)) return;

            if (string.IsNullOrEmpty(currentChatTitle))
            {
                string extractedTopic = ExtractChatTopic(userMessage);
                currentChatTitle = extractedTopic.Length > 30 ? extractedTopic.Substring(0, 30) + "..." : extractedTopic;

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

            UpdateAnalytics();
            UpdateTimeChart("days");


            // Обробляємо тему для аналітики окремо
            string classifiedTopic = await ClassifyMessageTopic(userMessage);
            if (!string.IsNullOrWhiteSpace(classifiedTopic))
            {
                if (!topicFrequency.ContainsKey(classifiedTopic))
                    topicFrequency[classifiedTopic] = 0;
                topicFrequency[classifiedTopic]++;
                UpdateTopicChart();

            }
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

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDate = e.Start.Date;
            UpdateEventsForDate(selectedDate);
        }
        private List<Event> allEvents = new();

        private void buttonAddEvent_Click(object sender, EventArgs e)
        {
            string eventText = textBoxNewEvent.Text.Trim();
            if (string.IsNullOrEmpty(eventText))
            {
                MessageBox.Show("Введіть текст події.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime selectedDate = datePickerEvent.Value.Date;
            DateTime selectedTime = timePickerEvent.Value;

            DateTime fullDateTime = new DateTime(
                selectedDate.Year, selectedDate.Month, selectedDate.Day,
                selectedTime.Hour, selectedTime.Minute, 0
            );

            allEvents.Add(new Event(fullDateTime, eventText));
            SaveCalendarEventsToDb();
            UpdateEventsForDate(monthCalendar1.SelectionStart.Date);
            ApplyEventFilter();

            UpdateCalendarStats();
            textBoxNewEvent.Clear();
        }

        private void UpdateEventsForDate(DateTime date)
        {
            listBoxEvents.Items.Clear();
            lblEventsTitle.Text = $"Події на: [{date:dd.MM.yyyy}]";

            var eventsForDate = allEvents
                .Where(ev => ev.Date.Date == date.Date)
                .OrderBy(ev => ev.Date.TimeOfDay)
                .ToList();

            if (eventsForDate.Count == 0)
            {
                listBoxEvents.Items.Add("Немає подій");
                listBoxEvents.Enabled = false;
            }
            else
            {
                listBoxEvents.Enabled = true;

                foreach (var ev in eventsForDate)
                {
                    string prefix = ev.IsDone ? "✅" : "";
                    listBoxEvents.Items.Add($"{prefix}{ev.Date:HH:mm} — {ev.Text}");
                }
            }


            UpdateCalendarStats();
        }


        


        private void btnDeleteEvent_Click_1(object sender, EventArgs e)
        {
            if (listBoxEvents.SelectedItem == null)
            {
                MessageBox.Show("Виберіть подію для видалення.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedItem = listBoxEvents.SelectedItem.ToString();
            DateTime selectedDate = monthCalendar1.SelectionStart.Date;

            var eventToRemove = allEvents
                .FirstOrDefault(ev => ev.Date.Date == selectedDate && $"{ev.Date:HH:mm} — {ev.Text}" == selectedItem);

            if (eventToRemove != default)
            {
                // Удаление из базы данных
                int userId = GetUserIdByLogin(currentLogin);
                if (userId != -1)
                {
                    string connectionString = "Data Source=vivy.db";
                    using (var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString))
                    {
                        connection.Open();
                        string deleteCmd = @"
                        DELETE FROM Events 
                        WHERE OwnerId = @userId AND Title = @title AND Date = @date";
                        using (var cmd = new Microsoft.Data.Sqlite.SqliteCommand(deleteCmd, connection))
                        {
                            cmd.Parameters.AddWithValue("@userId", userId);
                            cmd.Parameters.AddWithValue("@title", eventToRemove.Text);
                            cmd.Parameters.AddWithValue("@date", eventToRemove.Date);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // Удаляем локально из списка
                allEvents.Remove(eventToRemove);
                UpdateEventsForDate(selectedDate);
                ApplyEventFilter();
                UpdateCalendarStats();
            }
        }

        private void UpdateAnalytics()
        {
            if (chatHistory == null || chatHistory.Count == 0)
                return;

            int totalChats = chatHistory.Count;
            int totalMessages = 0;
            int totalGptResponsesLength = 0;
            int gptResponsesCount = 0;
            string longestChatTitle = "";
            int longestChatMessages = 0;

            // Найти самый длинный чат(заменил1)
            foreach (var chat in chatHistory)
            {
                totalMessages += chat.Value.Count;

                int currentChatMessageCount = chat.Value.Count;
                if (currentChatMessageCount > longestChatMessages)
                {
                    longestChatMessages = currentChatMessageCount;
                    longestChatTitle = chat.Key;
                }
            }

            // Подсчитать длину GPT-ответов ТОЛЬКО ПОСЛЕ того, как найден longestChatTitle
            if (!string.IsNullOrEmpty(longestChatTitle) && chatHistory.ContainsKey(longestChatTitle))
            {
                foreach (var (sender, message, sentAt) in chatHistory[longestChatTitle])
                {
                    if (sender == "Vivy")
                    {
                        totalGptResponsesLength += message.Length;
                        gptResponsesCount++;
                    }
                }
            }


            int avgLength = gptResponsesCount > 0 ? totalGptResponsesLength / gptResponsesCount : 0;

            // Оновлення UI
            lblChatsCount.Text = totalChats.ToString();
            lblMessagesCount.Text = totalMessages.ToString();
            lblAvgResponseLength.Text = $"{avgLength} символів";
            lblLongestChat.Text = longestChatTitle;
        }
        private void FrmSettings_Load(object sender, EventArgs e)
        {
            UpdateAnalytics();
        }
        private string ExtractChatTopic(string message)
        {
            // Видалимо розділові знаки і зайві прогалини
            string cleaned = Regex.Replace(message.ToLower(), @"[^\w\s]", "").Trim();

            // Розіб'ємо на слова
            var words = cleaned.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Візьмемо максимум 2-4 слова з кінця (вони часто найважливіші)
            var topicWords = words.Skip(Math.Max(0, words.Length - 4)).ToArray();

            return string.Join(" ", topicWords);
        }

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
        private void UpdateCalendarStats()
        {
            if (allEvents == null || allEvents.Count == 0)
            {
                lblTotalEvents2.Text = "0";
                lblPlannedEvents2.Text = "0";
                lblDoneEvents2.Text = "0";
                lblOverdueEvents2.Text = "0";
                lblNextEvent2.Text = "—";
                return;
            }

            int total = allEvents.Count;
            int completed = allEvents.Count(ev => ev.IsDone);
            int planned = allEvents.Count(ev => !ev.IsDone && ev.Date >= DateTime.Now);
            int overdue = allEvents.Count(ev => !ev.IsDone && ev.Date < DateTime.Now);

            var next = allEvents
                .Where(ev => !ev.IsDone && ev.Date >= DateTime.Now)
                .OrderBy(ev => ev.Date)
                .FirstOrDefault();

            lblTotalEvents2.Text = total.ToString();
            lblPlannedEvents2.Text = planned.ToString();
            lblDoneEvents2.Text = completed.ToString();
            lblOverdueEvents2.Text = overdue.ToString();
            lblNextEvent2.Text = next != default
                ? $"{next.Date:dd.MM.yyyy HH:mm} — {next.Text}"
                : "—";
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

        private void ApplyEventFilter()
        {
            string selectedFilter = cbEventFilter.SelectedItem?.ToString();
            listBoxAllEvents.Items.Clear();

            IEnumerable<Event> filtered = allEvents;

            switch (selectedFilter)
            {
                case "Усі події":
                    break;

                case "Заплановані":
                    filtered = allEvents.Where(ev => !ev.IsDone && ev.Date >= DateTime.Now);
                    break;

                case "Виконані":
                    filtered = allEvents.Where(ev => ev.IsDone);
                    break;

                case "Прострочені":
                    filtered = allEvents.Where(ev => !ev.IsDone && ev.Date < DateTime.Now);
                    break;
            }

            foreach (var ev in filtered.OrderBy(ev => ev.Date))
            {
                string status = ev.IsDone ? "✅" : ev.Date < DateTime.Now ? "⏰" : "";
                listBoxAllEvents.Items.Add($"{ev.Date:dd.MM.yyyy HH:mm} — {ev.Text} {status}");
            }
        }
        private void btnMarkAsDone_Click(object sender, EventArgs e)
        {
            if (listBoxAllEvents.SelectedItem == null) return;

            string selectedText = listBoxAllEvents.SelectedItem.ToString();

            var ev = allEvents.FirstOrDefault(ev =>
                selectedText.StartsWith($"{ev.Date:dd.MM.yyyy HH:mm} — {ev.Text}"));

            if (ev != null)
            {
                ev.IsDone = true;
                ApplyEventFilter();
                UpdateCalendarStats();
            }
        }

        private void UpdateCalendarAnalytics()
        {
            int total = allEvents.Count;
            int planned = allEvents.Count(ev => ev.Date >= DateTime.Now && !ev.IsDone);
            int done = allEvents.Count(ev => ev.IsDone);
            int overdue = allEvents.Count(ev => ev.Date < DateTime.Now && !ev.IsDone);

            lblTotalEvents.Text = total.ToString();
            lblPlannedEvents.Text = planned.ToString();
            lblDoneEvents.Text = done.ToString();
            lblOverdueEvents.Text = overdue.ToString();

            // Найближча подія
            var next = allEvents
                .Where(ev => ev.Date >= DateTime.Now && !ev.IsDone)
                .OrderBy(ev => ev.Date)
                .FirstOrDefault();

            lblNextEvent.Text = next != null
                ? $"{next.Date:dd.MM.yyyy HH:mm} — {next.Text}"
                : "—";
        }

        private Dictionary<string, int> topicFrequency = new();

        private async Task<string> ClassifyMessageTopic(string message)
        {
            string prompt = $"Определи основную тему этого сообщения (1-2 слова, без объяснений): \"{message}\"";

            string response = await GetGPTResponse(prompt);

            return response.Trim().Split('\n').FirstOrDefault()?.Trim() ?? "";
        }

        private void UpdateTopicChart()
        {
            // 👉 Временно заполняем темы вручную, если они пусты
            if (topicFrequency.Count == 0)
            {
                topicFrequency["Привітання"] = 5;
                topicFrequency["Погода"] = 3;
                topicFrequency["Код"] = 4;
                topicFrequency["Музика"] = 2;
                topicFrequency["Фільми"] = 1;
                topicFrequency["Навчання"] = 6;
            }

            var textColor = selectedTheme == "Світла" ? SKColors.Black : SKColors.White;

            var topTopics = topicFrequency
                .OrderByDescending(kvp => kvp.Value)
                .Take(6)
                .ToList();

            double total = topTopics.Sum(kvp => kvp.Value);

            pieChartTopics.Series = topTopics
                .Select(kvp => new PieSeries<double>
                {
                    Values = new[] { (double)kvp.Value },
                    Name = kvp.Key,
                    DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                    DataLabelsPaint = new SolidColorPaint(textColor),
                    DataLabelsSize = 14,
                    DataLabelsFormatter = point =>
                    {
                        double percent = total > 0 ? (point.Model / total) * 100 : 0;
                        return $"{point.Model} ({percent:F0}%)";
                    }

                })
                .ToList<ISeries>();
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

        private void UpdateTimeChart(string mode = "all")
        {
            List<string> labels;
            List<double> values;

            DateTime now = DateTime.Now;
            IEnumerable<DateTime> filtered = messageTimestamps;

            if (mode == "day")
            {
                filtered = messageTimestamps.Where(t => t.Date == now.Date);
                var hours = filtered
                    .GroupBy(t => t.Hour)
                    .OrderBy(g => g.Key)
                    .Select(g => new
                    {
                        Hour = $"{g.Key:00}:00",
                        Count = g.Count()
                    }).ToList();

                labels = hours.Select(h => h.Hour).ToList();
                values = hours.Select(h => (double)h.Count).ToList();
            }
            else if (mode == "week")
            {
                DateTime weekStart = now.Date.AddDays(-(int)now.DayOfWeek + 1); // Понеділок як початок тижня
                filtered = messageTimestamps.Where(t => t.Date >= weekStart && t.Date <= now.Date);

                var days = filtered
                    .GroupBy(t => t.Date.DayOfWeek)
                    .OrderBy(g => (int)g.Key)
                    .Select(g => new
                    {
                        Day = CultureInfo.GetCultureInfo("uk-UA").DateTimeFormat.GetDayName(g.Key),
                        Count = g.Count()
                    }).ToList();

                labels = days.Select(d => d.Day).ToList();
                values = days.Select(d => (double)d.Count).ToList();
            }
            else if (mode == "month")
            {
                filtered = messageTimestamps.Where(t => t.Date >= now.AddDays(-29).Date && t.Date <= now.Date);
                var dates = filtered
                    .GroupBy(t => t.Date)
                    .OrderBy(g => g.Key)
                    .Select(g => new
                    {
                        Date = g.Key.ToString("dd.MM"),
                        Count = g.Count()
                    }).ToList();

                labels = dates.Select(d => d.Date).ToList();
                values = dates.Select(d => (double)d.Count).ToList();
            }
            else // "all"
            {
                var dates = filtered
                    .GroupBy(t => t.Date)
                    .OrderBy(g => g.Key)
                    .Select(g => new
                    {
                        Date = g.Key.ToString("dd.MM"),
                        Count = g.Count()
                    }).ToList();

                labels = dates.Select(d => d.Date).ToList();
                values = dates.Select(d => (double)d.Count).ToList();
            }

            var columnSeries = new ColumnSeries<double>
            {
                Values = values,
                Name = "Активність",
                DataLabelsSize = 14,
                DataLabelsPaint = new SolidColorPaint(SKColors.White),
                DataLabelsFormatter = (point) => $"{point.Model}"

            };

            chartTopics.Series = new List<ISeries> { columnSeries };
            chartTopics.XAxes = new Axis[]
 {
    new Axis
    {
        Labels = labels.ToArray(),                    // Дати під кожним стовпцем
        LabelsPaint = new SolidColorPaint(SKColors.White),
        TextSize = 16,
        NamePaint = null,
        SeparatorsPaint = null,
        TicksPaint = null
    }
 };

            chartTopics.YAxes = new Axis[]
            {
        new Axis
        {
            LabelsPaint = new SolidColorPaint(SKColors.White),
            TextSize = 16
        }
            };
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
                        chartTopics.BackColor = Color.FromArgb(30, 35, 60);

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

            RebuildTopicFrequencyFromHistory();
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

        private void cbTimeViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRaw = cbTimeViewMode.SelectedItem?.ToString() ?? "За весь час";
            string selected = selectedRaw.TrimStart('•', ' ').Trim();

            string mode = selected switch
            {
                "За день" => "day",
                "За тиждень" => "week",
                "За місяць" => "month",
                "За весь час" => "all",
                _ => "all"
            };

            UpdateTimeChart(mode);
        }
        private async void RebuildTopicFrequencyFromHistory()
        {
            var tempFrequency = new Dictionary<string, int>();

            foreach (var chat in chatHistory.Values.ToList())
            {
                foreach (var (sender, message, sentAt) in chat.ToList())
                {
                    if (sender != currentLogin) continue;

                    string topic = await ClassifyMessageTopic(message);
                    if (string.IsNullOrWhiteSpace(topic)) continue;

                    if (!tempFrequency.ContainsKey(topic))
                        tempFrequency[topic] = 0;

                    tempFrequency[topic]++;
                }
            }

            topicFrequency = tempFrequency;
            UpdateTopicChart(); // Оновлюємо графік
        }

        private void panelVivy_VisibleChanged(object sender, EventArgs e)
        {
            if (panelVivy.Visible)
                LoadChatHistoryFromDb();
        }

        private void btnUpdateAnalytics_Click(object sender, EventArgs e)
        {
            UpdateAnalytics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadChatHistoryFromDb();
            RebuildTopicFrequencyFromHistory();
        }

        private void cbEventFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFilter = cbEventFilter.SelectedItem?.ToString();
            listBoxAllEvents.Items.Clear();

            IEnumerable<Event> filtered = allEvents;

            switch (selectedFilter)
            {
                case "Усі події":
                    break;

                case "Заплановані":
                    filtered = allEvents.Where(ev => !ev.IsDone && ev.Date >= DateTime.Now);
                    break;

                case "Виконані":
                    filtered = allEvents.Where(ev => ev.IsDone);
                    break;

                case "Прострочені":
                    filtered = allEvents.Where(ev => !ev.IsDone && ev.Date < DateTime.Now);
                    break;
            }

            foreach (var ev in filtered.OrderBy(ev => ev.Date))
            {
                string status = ev.IsDone ? "✅" : ev.Date < DateTime.Now ? "⏰" : "";
                listBoxAllEvents.Items.Add($"{ev.Date:dd.MM.yyyy HH:mm} — {ev.Text} {status}");
            }
        }

        
    }
}
