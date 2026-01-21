using Microsoft.Data.Sqlite;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Vivy
{
    public partial class FrmAddGame : Form
    {
        private string currentLogin;
        private string selectedTheme;
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

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
            
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            if (selectedTheme == "White")
            {
                this.BackColor = Color.WhiteSmoke;
                lblName.ForeColor = Color.Black;
                lblRules.ForeColor = Color.Black;
            }
            else
            {
                this.BackColor = Color.FromArgb(24, 30, 54);
                lblName.ForeColor = Color.White;
                lblRules.ForeColor = Color.White;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
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

                using var userCmd = new SqliteCommand("SELECT Id FROM Users WHERE Login = @login", connection);
                userCmd.Parameters.AddWithValue("@login", currentLogin);
                var userId = Convert.ToInt32(userCmd.ExecuteScalar());

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

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}