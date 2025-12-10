using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace Vivy
{
    public partial class FrmProfile : Form
    {
        private string currentLogin;
        private string avatarPath;
        private string selectedTheme = "Темна"; // По умолчанию
        public string NewLogin => txtLogin.Text;


        public FrmProfile(string login, string theme = "Темна")
        {
            InitializeComponent();
            selectedTheme = theme;
            ApplyTheme(selectedTheme);
            currentLogin = login;
            LoadUserProfile();
        }

        // Завантаження даних користувача з БД
        private void LoadUserProfile()
        {
            txtLogin.Text = "";
            txtEmail.Text = "";
            avatarPath = null;
            picAvatar.Image = null;

            string connectionString = "Data Source=vivy.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string selectCmd = "SELECT Login, Email, ProfileImage FROM Users WHERE Login = @login";
            using var cmd = new SqliteCommand(selectCmd, connection);
            cmd.Parameters.AddWithValue("@login", currentLogin);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtLogin.Text = reader.GetString(0);
                txtEmail.Text = reader.GetString(1);
                avatarPath = reader.IsDBNull(2) ? null : reader.GetString(2);

                if (!string.IsNullOrEmpty(avatarPath) && File.Exists(avatarPath))
                {
                    picAvatar.Image = Image.FromFile(avatarPath);
                }
            }
        }


        // Зміна аватарки
        private void btnChangeAvatar_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Зображення (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                avatarPath = ofd.FileName;
                try
                {
                    using (var stream = new FileStream(avatarPath, FileMode.Open, FileAccess.Read))
                    {
                        picAvatar.Image = Image.FromStream(stream);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не вдалося завантажити зображення: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    picAvatar.Image = null;
                }


            }
        }

        // Збереження змін профілю
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Логін не може бути порожнім!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=vivy.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string updateCmd = "UPDATE Users SET Login = @login, Email = @Email, ProfileImage = @avatar WHERE Login = @currentLogin";
            using (var cmd = new Microsoft.Data.Sqlite.SqliteCommand(updateCmd, connection))
            {
                cmd.Parameters.AddWithValue("@login", txtLogin.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@avatar", avatarPath ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@currentLogin", currentLogin);
                cmd.ExecuteNonQuery();
            }

            if (!string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                var (hash, salt) = HashPassword(txtNewPassword.Text);
                string updatePassCmd = "UPDATE Users SET PasswordHash = @hash, PasswordSalt = @salt WHERE Login = @login";
                using (var cmd = new SqliteCommand(updatePassCmd, connection))
                {
                    cmd.Parameters.AddWithValue("@hash", hash);
                    cmd.Parameters.AddWithValue("@salt", salt);
                    cmd.Parameters.AddWithValue("@login", txtLogin.Text);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Профіль оновлено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            currentLogin = txtLogin.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Застосування теми
        private void ApplyTheme(string theme)
        {
            selectedTheme = theme;
            Color backColor, foreColor, buttonBack, textBoxBack, textBoxFore;

            if (theme == "Світла")
            {
                backColor = Color.WhiteSmoke;
                foreColor = Color.Black;
                buttonBack = Color.Gainsboro;
                textBoxBack = Color.White;
                textBoxFore = Color.Black;
            }
            else
            {
                backColor = Color.FromArgb(46, 51, 73);
                foreColor = Color.White;
                buttonBack = Color.FromArgb(24, 30, 54);
                textBoxBack = Color.FromArgb(46, 51, 73);
                textBoxFore = Color.White;
            }

            this.BackColor = backColor;

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Panel panel)
                    panel.BackColor = backColor;
                else if (ctrl is Label label)
                    label.ForeColor = foreColor;
                else if (ctrl is Button btn)
                {
                    btn.BackColor = buttonBack;
                    btn.ForeColor = foreColor;
                }
                else if (ctrl is TextBox tb)
                {
                    tb.BackColor = textBoxBack;
                    tb.ForeColor = textBoxFore;
                }
                else if (ctrl is PictureBox pb)
                {
                    pb.BackColor = backColor;
                }

                // Рекурсивно для вложенных контролов
                foreach (Control child in ctrl.Controls)
                {
                    if (child is Label l) l.ForeColor = foreColor;
                    if (child is Button b)
                    {
                        b.BackColor = buttonBack;
                        b.ForeColor = foreColor;
                    }
                    if (child is TextBox t)
                    {
                        t.BackColor = textBoxBack;
                        t.ForeColor = textBoxFore;
                    }
                    if (child is Panel p) p.BackColor = backColor;
                }
            }
        }

        // Хешування пароля (аналогічно FrmLogin)
        private static (string hash, string salt) HashPassword(string password)
        {
            byte[] saltBytes = System.Security.Cryptography.RandomNumberGenerator.GetBytes(16);
            using var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(password, saltBytes, 100_000, System.Security.Cryptography.HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(32);
            return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(saltBytes));
        }
    }
}
