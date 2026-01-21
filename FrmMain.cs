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
using System;
using System.Management;

namespace Vivy
{
    public partial class FrmMain : Form
    {
        private List<DateTime> messageTimestamps = new List<DateTime>();

        private string currentLogin;
        private Color activeButtonColor;
        private Color inactiveButtonColor;

        // Neue Felder für Spielverwaltung
        private int currentGameId = -1;
        private string currentGameName = "";
        private string currentGameRules = "";

        // Поле для процесу Ollama
        private Process? ollamaProcess;

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
            InitializeComponent();

            currentLogin = login;

            // Event Handler für ListBox hinzufügen (jetzt für Spiele)
            listBoxHistory.SelectedIndexChanged += listBoxHistory_SelectedIndexChanged;

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

            // Запуск Ollama сервера
            StartOllamaServer();

            // Подписка на событие закрытия формы
            this.FormClosing += FrmMain_FormClosing;
        }

        // Запуск Ollama сервера
        private async void StartOllamaServer()
        {
            try
            {
                Debug.WriteLine("=== Start des Ollama Servers ===");
                
                // Überprüfen der Verfügbarkeit von ollama.exe
                if (!IsOllamaInstalled())
                {
                    MessageBox.Show(
                        "Ollama wurde im System nicht gefunden!\n\n" +
                        "Laden Sie Ollama herunter und installieren Sie es von:\n" +
                        "https://ollama.ai/download\n\n" +
                        "Starten Sie das Programm nach der Installation neu.",
                        "Ollama nicht installiert",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                Debug.WriteLine("Ollama gefunden, starte Prozess...");

                ollamaProcess = new Process();
                ollamaProcess.StartInfo.FileName = "ollama";
                ollamaProcess.StartInfo.Arguments = "serve";
                ollamaProcess.StartInfo.UseShellExecute = false;
                ollamaProcess.StartInfo.CreateNoWindow = true;
                ollamaProcess.StartInfo.RedirectStandardOutput = true;
                ollamaProcess.StartInfo.RedirectStandardError = true;
                ollamaProcess.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                // Abonnieren der Ausgabe für Diagnose
                ollamaProcess.OutputDataReceived += (s, e) => 
                {
                    if (!string.IsNullOrEmpty(e.Data))
                        Debug.WriteLine($"Ollama OUT: {e.Data}");
                };
                
                ollamaProcess.ErrorDataReceived += (s, e) => 
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        Debug.WriteLine($"Ollama ERR: {e.Data}");
                        
                        // Fehler "address already in use" ignorieren - bedeutet bereits gestartet
                        if (e.Data.Contains("address already in use") || e.Data.Contains("bind:"))
                        {
                            Debug.WriteLine("⚠ Ollama bereits im System gestartet");
                        }
                    }
                };

                bool started = ollamaProcess.Start();
                
                if (started)
                {
                    ollamaProcess.BeginOutputReadLine();
                    ollamaProcess.BeginErrorReadLine();
                    
                    Debug.WriteLine($"Ollama Prozess gestartet (PID: {ollamaProcess.Id})");
                    
                    // WICHTIG: Warten auf vollständige Initialisierung (10-15 Sekunden)
                    Debug.WriteLine("Warte auf vollständige Server-Initialisierung (dies kann 10-15 Sekunden dauern)...");
                    
                    // 15 Sekunden warten für Initialisierung
                    await Task.Delay(15000);
                    
                    // Überprüfen, ob der Prozess beendet wurde
                    if (ollamaProcess.HasExited)
                    {
                        Debug.WriteLine($"⚠ Ollama Prozess beendet mit Code: {ollamaProcess.ExitCode}");
                        
                        // Wenn Code 1 und "bind" Fehler - bedeutet bereits gestartet
                        if (ollamaProcess.ExitCode == 1)
                        {
                            Debug.WriteLine("Möglicherweise bereits durch anderen Prozess gestartet. Überprüfe...");
                            await Task.Delay(2000);
                            
                            // Finale Überprüfung mit erhöhtem Timeout
                            if (await IsOllamaRunningAsync(timeoutSeconds: 5))
                            {
                                Debug.WriteLine("✓ Ja, Ollama läuft!");
                                return;
                            }
                        }
                        
                        MessageBox.Show(
                            $"Ollama wurde gestartet, aber unerwartet beendet.\n\n" +
                            $"Exit-Code: {ollamaProcess.ExitCode}\n\n" +
                            $"Versuchen Sie, Ollama manuell zu starten mit:\n" +
                            $"ollama serve\n\n" +
                            $"und überprüfen Sie die Fehlermeldungen.",
                            "Fehler",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }
                    
                    Debug.WriteLine("Führe finale Überprüfung durch...");
                    
                    // Finale Überprüfung mit großem Timeout (die einzige Überprüfung!)
                    if (await IsOllamaRunningAsync(timeoutSeconds: 10))
                    {
                        Debug.WriteLine("✓ Ollama Server erfolgreich gestartet und antwortet!");
                        return;
                    }
                    
                    // Wenn nach allem Server nicht antwortet
                    Debug.WriteLine("⚠ Ollama gestartet, aber Server antwortet nicht nach 25 Sekunden");
                    
                    MessageBox.Show(
                        "Ollama wurde gestartet, aber der Server antwortet nicht.\n\n" +
                        "Mögliche Ursachen:\n" +
                        "1. Ollama lädt ein großes Modell\n" +
                        "2. Port 11434 wird von anderem Programm verwendet\n" +
                        "3. Firewall blockiert die Verbindung\n\n" +
                        "Versuchen Sie:\n" +
                        "- Noch 1-2 Minuten warten\n" +
                        "- Windows Task Manager überprüfen\n" +
                        "- Oder 'ollama serve' manuell im Terminal starten",
                        "Warnung",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else
                {
                    Debug.WriteLine("✗ Ollama Prozess konnte nicht gestartet werden");
                }
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Debug.WriteLine($"Win32Exception: {ex.Message}");
                Debug.WriteLine($"ErrorCode: {ex.ErrorCode}");
                Debug.WriteLine($"NativeErrorCode: {ex.NativeErrorCode}");
                
                MessageBox.Show(
                    $"Ollama konnte nicht gefunden oder gestartet werden.\n\n" +
                    $"Fehler: {ex.Message}\n\n" +
                    $"Lösung:\n" +
                    $"1. Überprüfen Sie, ob Ollama installiert ist\n" +
                    $"2. Starten Sie den Computer nach Installation neu\n" +
                    $"3. Starten Sie cmd und prüfen: ollama --version\n" +
                    $"4. Falls Befehl nicht funktioniert, Ollama neu installieren",
                    "Startfehler",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.GetType().Name}");
                Debug.WriteLine($"Message: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                
                MessageBox.Show(
                    $"Unerwarteter Fehler beim Starten von Ollama:\n\n{ex.Message}",
                    "Fehler",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Überprüfung der Ollama Installation
        private bool IsOllamaInstalled()
        {
            try
            {
                var testProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "ollama",
                        Arguments = "--version",
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    }
                };
                
                testProcess.Start();
                string output = testProcess.StandardOutput.ReadToEnd();
                testProcess.WaitForExit(2000);
                
                Debug.WriteLine($"Ollama Versions-Check: {output}");
                return testProcess.ExitCode == 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"IsOllamaInstalled Exception: {ex.Message}");
                return false;
            }
        }

        // Überprüfung, ob Ollama läuft (mit konfigurierbarem Timeout)
        private async Task<bool> IsOllamaRunningAsync(int timeoutSeconds = 10)
        {
            try
            {
                using var httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
                var response = await httpClient.GetAsync("http://localhost:11434/api/tags");
                
                Debug.WriteLine($"HTTP Check: StatusCode={response.StatusCode}");
                return response.IsSuccessStatusCode;
            }
            catch (TaskCanceledException)
            {
                Debug.WriteLine($"HTTP Check: Timeout nach {timeoutSeconds} Sekunden");
                return false;
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine($"HTTP Check: Verbindungsfehler - {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"HTTP Check fehlgeschlagen: {ex.Message}");
                return false;
            }
        }

        // Stoppen des Ollama Servers
        private void StopOllamaServer()
        {
            try
            {
                if (ollamaProcess != null && !ollamaProcess.HasExited)
                {
                    Debug.WriteLine("Stoppe Ollama...");
                    
                    // Получаем ID родительского процесса
                    int processId = ollamaProcess.Id;
                    
                    // Закрываем все связанные процессы ollama
                    KillProcessAndChildren(processId);
                    
                    ollamaProcess.Dispose();
                    ollamaProcess = null;

                    Debug.WriteLine("Ollama Server gestoppt");
                }
                else
                {
                    // Если процесс не отслеживается, ищем все процессы ollama
                    Debug.WriteLine("Suche nach verbleibenden Ollama-Prozessen...");
                    KillAllOllamaProcesses();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fehler beim Stoppen von Ollama: {ex.Message}");
            }
        }

        // Убивает процесс и все его дочерние процессы
        private void KillProcessAndChildren(int pid)
        {
            try
            {
                // Используем ManagementObjectSearcher для поиска дочерних процессов
                var searcher = new System.Management.ManagementObjectSearcher(
                    $"SELECT * FROM Win32_Process WHERE ParentProcessId={pid}");
                
                var collection = searcher.Get();
                
                // Рекурсивно закрываем дочерние процессы
                foreach (var item in collection)
                {
                    int childProcessId = Convert.ToInt32(item["ProcessId"]);
                    KillProcessAndChildren(childProcessId);
                }
                
                // Закрываем сам процесс
                try
                {
                    Process proc = Process.GetProcessById(pid);
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                        proc.WaitForExit(2000);
                        Debug.WriteLine($"Prozess {pid} beendet");
                    }
                }
                catch (ArgumentException)
                {
                    // Процесс уже завершен
                    Debug.WriteLine($"Prozess {pid} bereits beendet");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fehler beim Beenden von Prozess {pid}: {ex.Message}");
            }
        }

        // Закрывает все процессы ollama в системе
        private void KillAllOllamaProcesses()
        {
            try
            {
                var ollamaProcesses = Process.GetProcessesByName("ollama");
                
                foreach (var proc in ollamaProcesses)
                {
                    try
                    {
                        Debug.WriteLine($"Beende Ollama-Prozess: PID={proc.Id}");
                        proc.Kill();
                        proc.WaitForExit(2000);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Fehler beim Beenden von Ollama-Prozess {proc.Id}: {ex.Message}");
                    }
                    finally
                    {
                        proc.Dispose();
                    }
                }
                
                Debug.WriteLine($"Insgesamt {ollamaProcesses.Length} Ollama-Prozesse beendet");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fehler beim Suchen von Ollama-Prozessen: {ex.Message}");
            }
        }

        // Event-Handler für Formular schließen
        private void FrmMain_FormClosing(object? sender, FormClosingEventArgs e)
        {
            StopOllamaServer();
        }

        private Dictionary<string, List<(string sender, string text, DateTime sentAt)>> chatHistory = new();
        private string currentChatTitle = "";

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Setze den Benutzernamen
            Usder.Text = currentLogin;
            LoadAndApplyUserSettings();
            LoadUserGamesFromDatabase(); // Geändert: Lade Spiele statt Chats

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
            try
            {
                string userMessage = textBoxInput.Text.Trim();
                if (string.IsNullOrEmpty(userMessage)) return;

                // Prüfe ob ein Spiel ausgewählt ist
                if (currentGameId == -1)
                {
                    MessageBox.Show("Bitte wählen Sie zuerst ein Spiel aus!", "Vivy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(currentChatTitle))
                {
                    currentChatTitle = $"Chat {DateTime.Now:dd.MM.yyyy HH:mm}";
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

                // Speichere Benutzernachricht in DB (mit aktueller UserId)
                SaveMessageToDatabase(currentChatTitle, "User", userMessage, sentAt);

                // Vivy-Label hinzufügen
                richTextBox1.SelectionColor = Color.MediumPurple;
                richTextBox1.AppendText("Vivy: ");
                richTextBox1.SelectionColor = mainTextColor;

                // Zeichen-für-Zeichen-Anzeige mit Streaming
                IChatClient chatClient = new OllamaApiClient(new Uri("http://localhost:11434/"), "phi3:mini");

                // System-Prompt mit Spielregeln
                string systemPrompt = $@"Du bist ein Experte für das Brettspiel '{currentGameName}'. 
Du antwortest NUR auf Fragen zu diesem Spiel basierend auf folgenden Regeln:

{currentGameRules}

Wenn eine Frage NICHTS mit diesem Brettspiel zu tun hat, antworte höflich: 
'Entschuldigung, ich bin auf das Spiel {currentGameName} spezialisiert. Bitte stelle mir eine Frage zu diesem Spiel.'";

                List<ChatMessage> chatHistoryForAI = new()
                {
                    new ChatMessage(ChatRole.System, systemPrompt),
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

                        // Nur DoEvents, kein Scrollen während des Streamings
                        Application.DoEvents();
                    }
                }

                richTextBox1.AppendText("\n\n");

                // Erst nach Abschluss des Streamings scrollen
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.ScrollToCaret();

                // Antwort im Chat-Verlauf speichern
                DateTime responseTime = DateTime.Now;
                string gptResponse = fullResponse.ToString();
                chatHistory[currentChatTitle].Add(("Vivy", gptResponse, responseTime));
                messageTimestamps.Add(responseTime);

                // Speichere Vivy-Antwort in DB mit SenderId = 1 (KI-Benutzer)
                SaveMessageToDatabase(currentChatTitle, "Vivy", gptResponse, responseTime, customSenderId: 1);

                // Text-to-Speech falls aktiviert
                if (cbSpeakResponses.Checked)
                {
                    synthesizer.SpeakAsync(gptResponse);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler: {ex.Message}", "Vivy", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                try
                {
                    // WICHTIG: Werte VOR dem Neuaufbau speichern
                    bool speakEnabled = cbSpeakResponses.Checked;
                    
                    // KRITISCH: Aktuelles Spiel speichern!
                    int savedGameId = currentGameId;
                    string savedGameName = currentGameName;
                    string savedGameRules = currentGameRules;
                    string? selectedGameInList = listBoxHistory.SelectedItem?.ToString();

                    // 1. ZUERST in Datenbank speichern (mit InterfaceLanguage!)
                    string connectionString = "Data Source=vivy.db";
                    using var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
                    connection.Open();

                    string updateCmd = @"
                        UPDATE Users SET 
                        Theme = @theme,  
                        SpeakResponsesEnabled = @speak, 
                        Model = @model,
                        InterfaceLanguage = @language
                        WHERE Login = @login";
                    using var cmd = new Microsoft.Data.Sqlite.SqliteCommand(updateCmd, connection);
                    cmd.Parameters.AddWithValue("@theme", theme);
                    cmd.Parameters.AddWithValue("@speak", speakEnabled ? 1 : 0);
                    cmd.Parameters.AddWithValue("@model", model);
                    cmd.Parameters.AddWithValue("@language", interfaceLanguage);
                    cmd.Parameters.AddWithValue("@login", currentLogin);
                    
                    int rowsAffected = cmd.ExecuteNonQuery();
                    
                    Debug.WriteLine($"Einstellungen gespeichert: Theme={theme}, Speak={speakEnabled}, Model={model}, Language={interfaceLanguage}, RowsAffected={rowsAffected}");
                    
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show(
                            "Benutzer nicht in der Datenbank gefunden!",
                            "Fehler",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }

                    // 2. Jetzt UI neu aufbauen
                    ApplyTheme(theme);
                    
                    this.Controls.Clear();
                    InitializeComponent();
                    
                    // WICHTIG: Event Handler WIEDER hinzufügen!
                    listBoxHistory.SelectedIndexChanged += listBoxHistory_SelectedIndexChanged;
                    
                    RestoreCustomUI();
                    
                    // 3. Benutzerdaten wiederherstellen
                    Usder.Text = currentLogin;
                    LoadUserAvatar();
                    
                    // 4. WICHTIG: Spiele neu laden!
                    LoadUserGamesFromDatabase();
                    
                    // 5. KRITISCH: Gespeichertes Spiel wiederherstellen!
                    currentGameId = savedGameId;
                    currentGameName = savedGameName;
                    currentGameRules = savedGameRules;
                    
                    Debug.WriteLine($"Wiederherstellung: GameID={currentGameId}, GameName={currentGameName}");
                    
                    // 6. Spiel in ListBox auswählen
                    if (!string.IsNullOrEmpty(selectedGameInList))
                    {
                        for (int i = 0; i < listBoxHistory.Items.Count; i++)
                        {
                            if (listBoxHistory.Items[i].ToString() == selectedGameInList)
                            {
                                listBoxHistory.SelectedIndex = i;
                                Debug.WriteLine($"Spiel ausgewählt: {selectedGameInList} (Index={i})");
                                break;
                            }
                        }
                        
                        // Nachrichten neu laden
                        if (currentGameId != -1)
                        {
                            LoadGameMessagesFromDatabase(currentGameId);
                        }
                    }
                    
                    // 7. Gespeicherte Werte in ComboBoxen setzen
                    cbTheme.SelectedItem = theme;
                    cbModel.SelectedItem = model;
                    cbLanguage.SelectedItem = interfaceLanguage;
                    cbSpeakResponses.Checked = speakEnabled;
                    
                    // 8. Theme anwenden
                    ApplyTheme(theme);
                    
                    // 9. Ollama Server neu starten
                    StartOllamaServer();
                    this.FormClosing += FrmMain_FormClosing;

                    MessageBox.Show(
                        "Einstellungen erfolgreich gespeichert!",
                        "Erfolg",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Fehler beim Speichern: {ex.Message}");
                    Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                    
                    MessageBox.Show(
                        $"Fehler beim Speichern der Einstellungen:\n\n{ex.Message}",
                        "Fehler",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            else
            {
                MessageBox.Show(
                    "Bitte füllen Sie alle Felder aus!",
                    "Warnung",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
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

            string selectCmd = "SELECT Theme, SpeakResponsesEnabled, Model, InterfaceLanguage FROM Users WHERE Login = @login";
            using var cmd = new Microsoft.Data.Sqlite.SqliteCommand(selectCmd, connection);
            cmd.Parameters.AddWithValue("@login", currentLogin);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string theme = reader.IsDBNull(0) ? "Dark" : reader.GetString(0);
                bool speak = !reader.IsDBNull(1) && reader.GetInt32(1) == 1;
                string model = reader.IsDBNull(2) ? "gpt-3.5-turbo" : reader.GetString(2);
                string language = reader.IsDBNull(3) ? "English" : reader.GetString(3);

                cbTheme.SelectedItem = theme;
                cbSpeakResponses.Checked = speak;
                cbModel.SelectedItem = model;
                cbLanguage.SelectedItem = language;

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

        // Speichern der Nachrichten in die Datenbank 
        private void SaveMessageToDatabase(string chatTitle, string sender, string text, DateTime sentAt, int? customSenderId = null)
        {
            string connectionString = "Data Source=vivy.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            // Hole SenderId - verwende customSenderId falls angegeben, sonst aktueller User
            int senderId;
            if (customSenderId.HasValue)
            {
                senderId = customSenderId.Value;
            }
            else
            {
                senderId = GetUserIdByLogin(currentLogin);
            }

            // Speichere Nachricht mit GameID statt ChatId
            string insertCmd = @"INSERT INTO Messages (GameID, SenderId, Text, SentAt) 
                                VALUES (@gameId, @senderId, @text, @sentAt)";
            using var cmd = new SqliteCommand(insertCmd, connection);
            cmd.Parameters.AddWithValue("@gameId", currentGameId); // Verwende currentGameId direkt
            cmd.Parameters.AddWithValue("@senderId", senderId);
            cmd.Parameters.AddWithValue("@text", text);
            cmd.Parameters.AddWithValue("@sentAt", sentAt.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
            cmd.ExecuteNonQuery();
        }

        // NEUE METHODE: Spiel aus Datenbank laden
        private void LoadGameFromDatabase(string gameName)
        {
            try
            {
                string connectionString = "Data Source=vivy.db";
                using var connection = new SqliteConnection(connectionString);
                connection.Open();

                int userId = GetUserIdByLogin(currentLogin);

                string selectCmd = "SELECT Id, Name, Rules FROM BoardGames WHERE Name = @name AND UserId = @userId";
                using var cmd = new SqliteCommand(selectCmd, connection);
                cmd.Parameters.AddWithValue("@name", gameName);
                cmd.Parameters.AddWithValue("@userId", userId);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    currentGameId = reader.GetInt32(0);
                    currentGameName = reader.GetString(1);
                    currentGameRules = reader.GetString(2);

                    // Leere den Chat-Verlauf
                    currentChatTitle = string.Empty;
                    chatHistory.Clear();
                    
                    Debug.WriteLine($"Spiel geladen: ID={currentGameId}, Name={currentGameName}");
            
                    // WICHTIG: Reader MUSS geschlossen werden, bevor LoadGameMessagesFromDatabase aufgerufen wird!
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden des Spiels: {ex.Message}", "Vivy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine($"LoadGameFromDatabase Error: {ex.Message}");
            }
        }

        // NEUE METHODE: Laden der Nachrichten für ein bestimmtes Spiel
        private void LoadGameMessagesFromDatabase(int gameId)
        {
            try
            {
                string connectionString = "Data Source=vivy.db";
                using var connection = new SqliteConnection(connectionString);
                connection.Open();

                // Lade alle Nachrichten für dieses Spiel
                string selectCmd = @"SELECT m.Text, m.SentAt, m.SenderId 
                            FROM Messages m
                            WHERE m.GameID = @gameId
                            ORDER BY m.SentAt ASC";

                using var cmd = new SqliteCommand(selectCmd, connection);
                cmd.Parameters.AddWithValue("@gameId", gameId);

                richTextBox1.Clear();
                messageTimestamps.Clear();

                Color mainTextColor = selectedTheme.Trim().StartsWith("White", StringComparison.OrdinalIgnoreCase)
                    ? Color.Black
                    : Color.White;

                // WICHTIG: Zähle die Nachrichten
                int messageCount = 0;
        
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string text = reader.GetString(0);
                    DateTime sentAt = DateTime.Parse(reader.GetString(1));
                    int senderId = reader.GetInt32(2);

                    messageTimestamps.Add(sentAt);
                    messageCount++;

                    // SenderId 1 ist KI (Vivy), alle anderen sind Benutzer
                    string sender = senderId == 1 ? "Vivy" : "User";

                    // Anzeige in RichTextBox
                    if (sender == "User")
                    {
                        richTextBox1.SelectionColor = Color.DeepSkyBlue;
                        richTextBox1.AppendText("Sie: ");
                    }
                    else
                    {
                        richTextBox1.SelectionColor = Color.MediumPurple;
                        richTextBox1.AppendText("Vivy: ");
                    }

                    richTextBox1.SelectionColor = mainTextColor;
                    richTextBox1.AppendText(text + "\n\n");
                }
        
                Debug.WriteLine($"Nachrichten geladen: {messageCount} Nachrichten für GameID={gameId}");
        
                // Wenn keine Nachrichten vorhanden sind, zeige Willkommensnachricht
                if (messageCount == 0)
                {
                    richTextBox1.SelectionColor = Color.MediumPurple;
                    richTextBox1.AppendText("Vivy: ");
                    richTextBox1.SelectionColor = mainTextColor;
                    richTextBox1.AppendText($"Hallo! Ich helfe dir gerne bei Fragen zu '{currentGameName}'. Was möchtest du wissen?\n\n");
                }

                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden der Nachrichten: {ex.Message}", "Vivy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine($"LoadGameMessagesFromDatabase Error: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }

        // GEÄNDERTE METHODE: Neues Spiel hinzufügen (früher btnNewChat_Click)
        private void btnNewChat_Click(object sender, EventArgs e)
        {
            using (var addGameForm = new FrmAddGame(currentLogin, selectedTheme))
            {
                if (addGameForm.ShowDialog() == DialogResult.OK)
                {
                    // Lade Spielliste neu
                    LoadUserGamesFromDatabase();
                }
            }
        }

        // GEÄNDERTE METHODE: Spiel löschen (früher btnDeleteChat_Click)
        private void btnDeleteChat_Click(object sender, EventArgs e)
        {
            // Prüfen ob ein Spiel ausgewählt ist
            if (listBoxHistory.SelectedItem == null)
            {
                MessageBox.Show(
                    "Bitte wählen Sie ein Spiel aus, das Sie löschen möchten.",
                    "Vivy",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            string gameToDelete = listBoxHistory.SelectedItem.ToString();

            // Bestätigung vom Benutzer einholen
            var result = MessageBox.Show(
                $"Möchten Sie das Spiel '{gameToDelete}' wirklich löschen? Diese Aktion kann nicht rückgängig gemacht werden.",
                "Spiel löschen",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.No)
                return;

            try
            {
                string connectionString = "Data Source=vivy.db";
                using var connection = new SqliteConnection(connectionString);
                connection.Open();

                int userId = GetUserIdByLogin(currentLogin);

                // Lösche das Spiel
                string deleteGameCmd = "DELETE FROM BoardGames WHERE Name = @name AND UserId = @userId";
                using var deleteCmd = new SqliteCommand(deleteGameCmd, connection);
                deleteCmd.Parameters.AddWithValue("@name", gameToDelete);
                deleteCmd.Parameters.AddWithValue("@userId", userId);
                deleteCmd.ExecuteNonQuery();

                // Entferne aus der ListBox
                listBoxHistory.Items.Remove(gameToDelete);

                // Wenn das gelöschte Spiel das aktuelle Spiel war, leere die Anzeige
                if (currentGameName == gameToDelete)
                {
                    currentGameId = -1;
                    currentGameName = string.Empty;
                    currentGameRules = string.Empty;
                    currentChatTitle = string.Empty;
                    richTextBox1.Clear();
                    textBoxInput.Clear();
                    messageTimestamps.Clear();
                }

                MessageBox.Show(
                    "Spiel erfolgreich gelöscht!",
                    "Vivy",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Fehler beim Löschen des Spiels: {ex.Message}",
                    "Vivy",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void listBoxHistory_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (listBoxHistory.SelectedItem is string selectedGame)
            {
                LoadGameFromDatabase(selectedGame);
                if (currentGameId != -1)
                {
                    LoadGameMessagesFromDatabase(currentGameId);
                }
            }
        }

        // Lädt alle Spiele des aktuellen Benutzers aus der Datenbank und zeigt sie in der ListBox an
        private void LoadUserGamesFromDatabase()
        {
            try
            {
                string connectionString = "Data Source=vivy.db";
                using var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
                connection.Open();

                int userId = GetUserIdByLogin(currentLogin);

                string selectCmd = "SELECT Name FROM BoardGames WHERE UserId = @userId ORDER BY Name ASC";
                using var cmd = new Microsoft.Data.Sqlite.SqliteCommand(selectCmd, connection);
                cmd.Parameters.AddWithValue("@userId", userId);

                using var reader = cmd.ExecuteReader();
                listBoxHistory.Items.Clear();
                while (reader.Read())
                {
                    string gameName = reader.GetString(0);
                    listBoxHistory.Items.Add(gameName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden der Spiele: {ex.Message}", "Vivy", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
