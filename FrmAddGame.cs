using Microsoft.Data.Sqlite;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Vivy
{
    public partial class FrmAddGame : Form
    {
        private string currentLogin;
        private string selectedTheme;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        public FrmAddGame(string login, string theme)
        {
            InitializeComponent();
            currentLogin = login;
            selectedTheme = theme;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        private void InitializeComponent()
        {
            this.Text = "Neues Spiel hinzufügen";
            this.Size = new Size(450, 400);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblTitle = new Label
            {
                Text = "Neues Brettspiel hinzufügen",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 126, 249),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            Label lblName = new Label
            {
                Text = "Spielname:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(20, 70)
            };

            TextBox txtGameName = new TextBox
            {
                Name = "txtGameName",
                Font = new Font("Segoe UI", 10),
                Size = new Size(400, 30),
                Location = new Point(20, 95)
            };

            Label lblRules = new Label
            {
                Text = "Spielregeln:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(20, 135)
            };

            RichTextBox rtbRules = new RichTextBox
            {
                Name = "rtbRules",
                Font = new Font("Segoe UI", 10),
                Size = new Size(400, 150),
                Location = new Point(20, 160)
            };

            Button btnAdd = new Button
            {
                Text = "Hinzufügen",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(120, 35),
                Location = new Point(200, 325),
                BackColor = Color.FromArgb(0, 126, 249),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += BtnAdd_Click;

            Button btnCancel = new Button
            {
                Text = "Abbrechen",
                Font = new Font("Segoe UI", 10),
                Size = new Size(120, 35),
                Location = new Point(330, 325),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] {
                lblTitle, lblName, txtGameName, lblRules, rtbRules, btnAdd, btnCancel
            });

            // Theme anwenden
            if (selectedTheme == "White")
            {
                this.BackColor = Color.WhiteSmoke;
                lblName.ForeColor = Color.Black;
                lblRules.ForeColor = Color.Black;
            }
            else
            {
                this.BackColor = Color.FromArgb(46, 51, 73);
                lblName.ForeColor = Color.White;
                lblRules.ForeColor = Color.White;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var txtGameName = this.Controls.Find("txtGameName", true)[0] as TextBox;
            var rtbRules = this.Controls.Find("rtbRules", true)[0] as RichTextBox;

            if (string.IsNullOrWhiteSpace(txtGameName?.Text))
            {
                MessageBox.Show("Bitte geben Sie einen Spielnamen ein!", "Vivy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(rtbRules?.Text))
            {
                MessageBox.Show("Bitte geben Sie die Spielregeln ein!", "Vivy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string connectionString = "Data Source=vivy.db";
                using var connection = new SqliteConnection(connectionString);
                connection.Open();

                // Hole UserId
                using var userCmd = new SqliteCommand("SELECT Id FROM Users WHERE Login = @login", connection);
                userCmd.Parameters.AddWithValue("@login", currentLogin);
                var userId = Convert.ToInt32(userCmd.ExecuteScalar());

                // Füge Spiel hinzu
                string insertCmd = @"INSERT INTO BoardGames (Name, Rules, UserId, CreatedAt) 
                                    VALUES (@name, @rules, @userId, @createdAt)";
                using var cmd = new SqliteCommand(insertCmd, connection);
                cmd.Parameters.AddWithValue("@name", txtGameName.Text.Trim());
                cmd.Parameters.AddWithValue("@rules", rtbRules.Text.Trim());
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@createdAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();

                MessageBox.Show("Spiel erfolgreich hinzugefügt!", "Vivy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Hinzufügen des Spiels: {ex.Message}", "Vivy", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}