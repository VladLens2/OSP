using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vivy
{
    public partial class FrmLogin : Form
    {

        public string UserLogin { get; private set; }

        // Methode zum Hashen des Passworts mit Salt und PBKDF2 (SHA256)
        private static (string hash, string salt) HashPassword(string password)
        {
            // Erzeugt eine zufällige Salt mit 16 Byte Länge
            byte[] saltBytes = System.Security.Cryptography.RandomNumberGenerator.GetBytes(16);
            // Erzeugt PBKDF2 mit 100.000 Iterationen und dem Algorithmus SHA256
            using var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(password, saltBytes, 100_000, System.Security.Cryptography.HashAlgorithmName.SHA256);
            // Erhält den Hash mit 32 Bytes Länge
            byte[] hashBytes = pbkdf2.GetBytes(32);
            // Gibt Hash und Salt als Base64-kodierte Strings zurück
            return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(saltBytes));
        }

        // Methode zur Überprüfung des Passworts: hasht das eingegebene Passwort mit dem gespeicherten Salt und vergleicht es mit dem gespeicherten Hash
        private static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            // Wandelt das Salt von Base64 in Bytes um
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            // Hashen des eingegebenen Passworts mit diesem Salt
            using var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(password, saltBytes, 100_000, System.Security.Cryptography.HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(32);
            // Vergleicht den erhaltenen Hash mit dem gespeicherten
            return Convert.ToBase64String(hashBytes) == storedHash;
        }

        // Importiert die Funktion zum Erstellen abgerundeter Fensterkanten
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

        public FrmLogin()
        {
            InitializeComponent();
            // Wendet abgerundete Ecken auf das Fenster an
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        // Icons zum Ausblenden/Anzeigen des Passworts
        Bitmap bmpHide = Properties.Resources.hide;
        Bitmap bmpReveal = Properties.Resources.reveal;

        // Ereignishandler für Klick auf die Schaltfläche "Anmelden"
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Verbindungszeichenfolge zur SQLite-Datenbank
            string connectionString = "Data Source=vivy.db";
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
            connection.Open();

            // Abfrage zum Abrufen von Hash und Salt für den Benutzerlogin
            string selectCmd = "SELECT PasswordHash, PasswordSalt FROM Users WHERE Login = @login";
            using var cmd = new Microsoft.Data.Sqlite.SqliteCommand(selectCmd, connection);
            cmd.Parameters.AddWithValue("@login", txtUsername.Text);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string hash = reader.GetString(0);
                string salt = reader.GetString(1);

                // Überprüfen des eingegebenen Passworts
                if (VerifyPassword(txtPassword.Text, hash, salt))
                {
                    System.IO.File.WriteAllText("user_session.txt", txtUsername.Text);

                    // Wenn das Passwort korrekt ist — Formular mit Erfolg schließen
                    UserLogin = txtUsername.Text;
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                    return;
                }
            }

            // Wenn Login oder Passwort falsch sind — Fehlermeldung anzeigen
            MessageBox.Show("Falscher Benutzername oder Passwort!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtPassword.Clear();
            txtPassword.Focus();
        }

        // Behandelt das Beenden der Anwendung beim Klick auf "Beenden"
        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Behandelt das Beenden der Anwendung beim Klick auf "Beenden" in der Registrierung
        private void lblExitReg_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Wechsel zum Anmeldeformular (falls bereits registriert)
        private void lblAlreadyReg_Click(object sender, EventArgs e)
        {
            pnlReg.Visible = false;
            pnlLog.Visible = true;
        }

        // Wechsel zum Registrierungsformular (falls noch nicht registriert)
        private void lblNotRegistred_Click(object sender, EventArgs e)
        {
            pnlReg.Visible = true;
            pnlLog.Visible = false;
        }

        // Initialisierung des Formulars: Anmeldepanel anzeigen, Registrierung verbergen, Icon setzen
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            pnlReg.Visible = false;
            pnlLog.Visible = true;
            btnReveal.Image = bmpReveal;
        }

        // Schaltfläche "Passwort anzeigen/ausblenden"
        private void btnReveal_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '\0')
            {
                // Passwort ausblenden
                txtPassword.PasswordChar = '*';
                btnReveal.Image = bmpReveal;
            }
            else
            {
                // Passwort anzeigen
                txtPassword.PasswordChar = '\0';
                btnReveal.Image = bmpHide;
            }
        }

        // Verarbeitung der Registrierung eines neuen Benutzers
        private void btnReg_Click(object sender, EventArgs e)
        {
            // Prüfen, ob alle Felder ausgefüllt sind
            if (string.IsNullOrWhiteSpace(tbxLogin.Text) ||
                string.IsNullOrWhiteSpace(tbxPassword.Text) ||
                string.IsNullOrWhiteSpace(tbxEmail.Text))
            { 
                MessageBox.Show("Geben Sie Ihren Benutzernamen, Passwort und E-Mail-Adresse ein!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prüfen der Gültigkeit der E-Mail
            if (!tbxEmail.Text.Contains("@") || !tbxEmail.Text.Contains("."))
            {
                MessageBox.Show("Geben Sie eine korrekte E-Mail-Adresse ein!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hashen des Passworts und Erzeugen des Salts
            var (hash, salt) = HashPassword(tbxPassword.Text);

            // Öffnen der Verbindung zur Datenbank
            string connectionString = "Data Source=vivy.db";
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
            connection.Open();

            // Prüfen, ob ein Benutzer mit diesem Login bereits existiert
            string checkCmd = "SELECT COUNT(*) FROM Users WHERE Login = @login";
            using (var check = new Microsoft.Data.Sqlite.SqliteCommand(checkCmd, connection))
            {
                check.Parameters.AddWithValue("@login", tbxLogin.Text);
                long exists = (long)check.ExecuteScalar();
                if (exists > 0)
                {
                    MessageBox.Show("Ein Benutzer mit diesem Benutzernamen existiert bereits!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Fügt einen neuen Benutzer in die Datenbank hinzu
            string insertCmd = "INSERT INTO Users (Login, PasswordHash, PasswordSalt, Email) VALUES (@login, @hash, @salt, @email)";
            using (var cmd = new Microsoft.Data.Sqlite.SqliteCommand(insertCmd, connection))
            {
                cmd.Parameters.AddWithValue("@login", tbxLogin.Text);
                cmd.Parameters.AddWithValue("@hash", hash);
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Parameters.AddWithValue("@email", tbxEmail.Text);
                cmd.ExecuteNonQuery();
            }

            // Erfolgreiche Registrierung — Meldung anzeigen und zum Anmeldeformular wechseln
            MessageBox.Show("Die Registrierung war erfolgreich! Melden Sie sich jetzt an.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);

            pnlReg.Visible = false;
            pnlLog.Visible = true;
        }


    }
}
