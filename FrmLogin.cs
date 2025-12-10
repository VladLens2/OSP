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

        // Метод для хешування пароля з використанням солі та PBKDF2 (SHA256)
        // Повертає кортеж: (хеш, сіль), обидва значення у форматі Base64
        private static (string hash, string salt) HashPassword(string password)
        {
            // Генеруємо випадкову сіль довжиною 16 байт
            byte[] saltBytes = System.Security.Cryptography.RandomNumberGenerator.GetBytes(16);
            // Створюємо PBKDF2 з 100 000 ітерацій та алгоритмом SHA256
            using var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(password, saltBytes, 100_000, System.Security.Cryptography.HashAlgorithmName.SHA256);
            // Отримуємо хеш довжиною 32 байти
            byte[] hashBytes = pbkdf2.GetBytes(32);
            // Повертаємо хеш і сіль у вигляді рядків Base64
            return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(saltBytes));
        }

        // Метод для перевірки пароля: хешує введений пароль із збереженою сіллю та порівнює з збереженим хешем
        private static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            // Перетворюємо сіль з Base64 у байти
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            // Хешуємо введений пароль із цією сіллю
            using var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(password, saltBytes, 100_000, System.Security.Cryptography.HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(32);
            // Порівнюємо отриманий хеш із збереженим
            return Convert.ToBase64String(hashBytes) == storedHash;
        }

        // Імпортуємо функцію для створення заокруглених кутів вікна
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
            // Застосовуємо заокруглення кутів до вікна
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        // Іконки для приховування/відображення пароля
        Bitmap bmpHide = Properties.Resources.hide;
        Bitmap bmpReveal = Properties.Resources.reveal;

        // Обробка натискання кнопки "Увійти"
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Рядок підключення до SQLite бази даних
            string connectionString = "Data Source=vivy.db";
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
            connection.Open();

            // Запит для отримання хеша та солі за логіном користувача
            string selectCmd = "SELECT PasswordHash, PasswordSalt FROM Users WHERE Login = @login";
            using var cmd = new Microsoft.Data.Sqlite.SqliteCommand(selectCmd, connection);
            cmd.Parameters.AddWithValue("@login", txtUsername.Text);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string hash = reader.GetString(0);
                string salt = reader.GetString(1);

                // Перевіряємо введений пароль
                if (VerifyPassword(txtPassword.Text, hash, salt))
                {
                    System.IO.File.WriteAllText("user_session.txt", txtUsername.Text);

                    // Якщо пароль вірний — закриваємо форму з успішним результатом
                    UserLogin = txtUsername.Text;
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                    return;
                }
            }

            // Якщо логін або пароль невірний — показуємо помилку
            MessageBox.Show("Невірний логін або пароль!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtPassword.Clear();
            txtPassword.Focus();
        }

        // Обробка виходу з програми по кліку на "Вихід"
        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Обробка виходу з програми по кліку на "Вихід" у реєстрації
        private void lblExitReg_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Перехід до форми входу (якщо вже зареєстрований)
        private void lblAlreadyReg_Click(object sender, EventArgs e)
        {
            pnlReg.Visible = false;
            pnlLog.Visible = true;
        }

        // Перехід до форми реєстрації (якщо не зареєстрований)
        private void lblNotRegistred_Click(object sender, EventArgs e)
        {
            pnlReg.Visible = true;
            pnlLog.Visible = false;
        }

        // Ініціалізація форми: показуємо панель входу, ховаємо реєстрацію, встановлюємо іконку
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            pnlReg.Visible = false;
            pnlLog.Visible = true;
            btnReveal.Image = bmpReveal;
        }

        // Кнопка "показати/приховати пароль"
        private void btnReveal_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '\0')
            {
                // Приховати пароль
                txtPassword.PasswordChar = '*';
                btnReveal.Image = bmpReveal;
            }
            else
            {
                // Показати пароль
                txtPassword.PasswordChar = '\0';
                btnReveal.Image = bmpHide;
            }
        }

        // Обробка реєстрації нового користувача
        private void btnReg_Click(object sender, EventArgs e)
        {
            // Перевіряємо, що всі поля заповнені
            if (string.IsNullOrWhiteSpace(tbxLogin.Text) ||
                string.IsNullOrWhiteSpace(tbxPassword.Text) ||
                string.IsNullOrWhiteSpace(tbxEmail.Text))
            {
                MessageBox.Show("Введіть логін, пароль та e-mail!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Перевіряємо коректність e-mail
            if (!tbxEmail.Text.Contains("@") || !tbxEmail.Text.Contains("."))
            {
                MessageBox.Show("Введіть коректний e-mail!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Хешуємо пароль і отримуємо сіль
            var (hash, salt) = HashPassword(tbxPassword.Text);

            // Відкриваємо з'єднання з базою даних
            string connectionString = "Data Source=vivy.db";
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);
            connection.Open();

            // Перевіряємо, чи існує користувач з таким логіном
            string checkCmd = "SELECT COUNT(*) FROM Users WHERE Login = @login";
            using (var check = new Microsoft.Data.Sqlite.SqliteCommand(checkCmd, connection))
            {
                check.Parameters.AddWithValue("@login", tbxLogin.Text);
                long exists = (long)check.ExecuteScalar();
                if (exists > 0)
                {
                    MessageBox.Show("Користувач з таким логіном вже існує!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Додаємо нового користувача до бази даних
            string insertCmd = "INSERT INTO Users (Login, PasswordHash, PasswordSalt, Email) VALUES (@login, @hash, @salt, @email)";
            using (var cmd = new Microsoft.Data.Sqlite.SqliteCommand(insertCmd, connection))
            {
                cmd.Parameters.AddWithValue("@login", tbxLogin.Text);
                cmd.Parameters.AddWithValue("@hash", hash);
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Parameters.AddWithValue("@email", tbxEmail.Text);
                cmd.ExecuteNonQuery();
            }

            // Успішна реєстрація — показуємо повідомлення і перемикаємося на форму входу
            MessageBox.Show("Реєстрація успішна! Тепер увійдіть.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);

            pnlReg.Visible = false;
            pnlLog.Visible = true;
        }

        private void pnlLog_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
