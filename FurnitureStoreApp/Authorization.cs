using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FurnitureStoreApp
{
    public partial class LoginForm : Form
    {
        private string connectionString = DatabaseConnection.ConnectionString;

        // ── CAPTCHA / блокировка ──────────────────────────────────────
        private int    _failCount    = 0;   // число неудачных попыток
        private string _captchaText  = "";  // текущее значение CAPTCHA
        private int    _blockLeft    = 0;   // секунд до разблокировки

        // Символы для CAPTCHA: исключены похожие (0/O, 1/I/l)
        private const string CaptchaChars =
            "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz23456789";

        public LoginForm()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
            txtUsername.MaxLength    = 50;
            txtPassword.MaxLength    = 50;
        }

        // ════════════════════════════════════════════════════════════════
        // ISSUE #5 — Генерация CAPTCHA
        // ════════════════════════════════════════════════════════════════
        private void GenerateCaptcha()
        {
            var rnd = new Random();

            // 4 случайных символа
            char[] chars = new char[4];
            for (int i = 0; i < 4; i++)
                chars[i] = CaptchaChars[rnd.Next(CaptchaChars.Length)];
            _captchaText = new string(chars);

            // Рисуем картинку 200×60
            var bmp = new Bitmap(200, 60);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode     = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // Светло-серый фон
                g.Clear(Color.FromArgb(245, 245, 245));

                // Шумовые линии (10 штук)
                for (int i = 0; i < 10; i++)
                {
                    var pen = new Pen(
                        Color.FromArgb(rnd.Next(150, 210),
                                       rnd.Next(150, 210),
                                       rnd.Next(150, 210)), 1);
                    g.DrawLine(pen,
                        rnd.Next(0, 200), rnd.Next(0, 60),
                        rnd.Next(0, 200), rnd.Next(0, 60));
                    pen.Dispose();
                }

                // Шумовые точки (40 штук)
                for (int i = 0; i < 40; i++)
                {
                    var dotBrush = new SolidBrush(
                        Color.FromArgb(rnd.Next(120, 200),
                                       rnd.Next(120, 200),
                                       rnd.Next(120, 200)));
                    g.FillEllipse(dotBrush,
                        rnd.Next(0, 198), rnd.Next(0, 58), 2, 2);
                    dotBrush.Dispose();
                }

                // Рисуем каждый символ
                // Символы НЕ в одну линию — случайный Y-сдвиг ±12px
                int[] baseY  = { 10, 16, 8, 14 };    // разные базовые Y
                int[] baseX  = { 12, 58, 104, 150 }; // равномерно по X
                Color[] symbolColors = {
                    Color.FromArgb(30, 30, 150),
                    Color.FromArgb(150, 30, 30),
                    Color.FromArgb(30, 120, 30),
                    Color.FromArgb(100, 30, 130)
                };

                for (int i = 0; i < 4; i++)
                {
                    var font   = new Font("Arial", 22 + rnd.Next(-2, 3),
                                          FontStyle.Bold);
                    var brush  = new SolidBrush(symbolColors[i]);

                    // Трансформация: лёгкий наклон символа
                    var state = g.Save();
                    float angle = rnd.Next(-18, 19);
                    int   cx    = baseX[i] + 16;
                    int   cy    = baseY[i] + 16;
                    g.TranslateTransform(cx, cy);
                    g.RotateTransform(angle);
                    g.DrawString(_captchaText[i].ToString(), font, brush,
                                 -16, -16);
                    g.Restore(state);

                    // Перечёркиваем символ горизонтальной линией
                    var strikePen = new Pen(
                        Color.FromArgb(rnd.Next(80, 160), 80, 80), 1.5f);
                    g.DrawLine(strikePen,
                        baseX[i] + 2,  cy,
                        baseX[i] + 30, cy);
                    strikePen.Dispose();

                    font.Dispose();
                    brush.Dispose();
                }

                // Волнистая линия поверх всего — усложняет авторасшифровку
                var wavePen = new Pen(Color.FromArgb(100, 80, 80, 180), 1.5f);
                var points  = new Point[10];
                for (int i = 0; i < 10; i++)
                    points[i] = new Point(i * 22, 30 + rnd.Next(-10, 10));
                g.DrawCurve(wavePen, points);
                wavePen.Dispose();
            }

            pbCaptcha.Image = bmp;
        }

        // ════════════════════════════════════════════════════════════════
        // ISSUE #6 — Логика входа с CAPTCHA
        // ════════════════════════════════════════════════════════════════
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Если идёт блокировка — ничего не делаем
            if (_blockLeft > 0) return;

            string login    = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                ShowError("Введите логин и пароль.");
                return;
            }

            // Если CAPTCHA уже показана — проверяем её ПЕРЕД проверкой пароля
            if (_failCount >= 1 && pbCaptcha.Visible)
            {
                if (!ValidateCaptcha()) return; // ValidateCaptcha сам покажет ошибку/блок
            }

            // ── Учётка admin из App.Config ─────────────────────────────
            string cfgLogin    = (ConfigurationManager.AppSettings["DefaultAdminLogin"]    ?? "").Trim();
            string cfgPassword = (ConfigurationManager.AppSettings["DefaultAdminPassword"] ?? "").Trim();

            if (string.Equals(login, cfgLogin, StringComparison.Ordinal)
                && string.Equals(password.Trim(), cfgPassword, StringComparison.Ordinal))
            {
                CurrentSession.UserId   = 0;
                CurrentSession.FullName = "Администратор БД";
                CurrentSession.Login    = login;
                CurrentSession.RoleId   = 0;
                CurrentSession.RoleName = "db_admin";
                new AdminImportMenuForm().Show();
                this.Hide();
                return;
            }

            // ── Обычная проверка по БД ─────────────────────────────────
            string passwordHash = PasswordHelper.HashPassword(password);

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT u.id, u.full_name, u.login, u.role_id, r.name AS role_name
                        FROM users u
                        LEFT JOIN roles r ON u.role_id = r.id
                        WHERE u.login = @login AND u.password = @password";

                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@login",    login);
                    cmd.Parameters.AddWithValue("@password", passwordHash);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Успех — сбрасываем счётчик и скрываем CAPTCHA
                            _failCount = 0;
                            HideCaptcha();

                            CurrentSession.UserId   = Convert.ToInt32(reader["id"]);
                            CurrentSession.FullName = reader["full_name"].ToString();
                            CurrentSession.Login    = reader["login"].ToString();
                            CurrentSession.RoleId   = Convert.ToInt32(reader["role_id"]);
                            CurrentSession.RoleName = reader["role_name"].ToString();
                            reader.Close();
                            OpenRoleMenu();
                        }
                        else
                        {
                            HandleFailedLogin();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Ошибка подключения к БД: " + ex.Message);
            }
        }

        /// <summary>Обрабатывает неудачную попытку входа.</summary>
        private void HandleFailedLogin()
        {
            _failCount++;

            if (_failCount == 1)
            {
                // Первая неудача — показываем CAPTCHA
                MessageBox.Show(
                    "Неверный логин или пароль.\n\nДля продолжения введите символы с картинки.",
                    "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ShowCaptcha();
            }
            else
            {
                // Последующие неудачи — блокировка (обрабатывается в ValidateCaptcha)
                MessageBox.Show(
                    "Неверный логин или пароль.",
                    "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GenerateCaptcha();     // обновляем CAPTCHA
                txtCaptcha.Clear();
                txtCaptcha.Focus();
            }

            ClearPasswordField();
        }

        // ════════════════════════════════════════════════════════════════
        // ISSUE #7 — Проверка CAPTCHA и блокировка на 10 секунд
        // ════════════════════════════════════════════════════════════════

        /// <summary>
        /// Проверяет введённую CAPTCHA.
        /// Возвращает true если верно, false — если нет (и запускает блокировку).
        /// </summary>
        private bool ValidateCaptcha()
        {
            string entered = txtCaptcha.Text.Trim();

            if (string.Compare(entered, _captchaText, StringComparison.OrdinalIgnoreCase) == 0)
            {
                // CAPTCHA верна
                lblError.Visible = false;
                return true;
            }

            // CAPTCHA неверна — блокировка 10 секунд
            _failCount++;
            StartBlock();
            return false;
        }

        private void StartBlock()
        {
            _blockLeft = 10;
            btnLogin.Enabled           = false;
            txtCaptcha.Enabled         = false;
            btnRefreshCaptcha.Enabled  = false;
            lblError.Text              = "Неверная CAPTCHA!";
            lblError.Visible           = true;
            lblBlock.Visible           = true;
            UpdateBlockLabel();
            GenerateCaptcha();
            txtCaptcha.Clear();
            tmrBlock.Start();
        }

        private void tmrBlock_Tick(object sender, EventArgs e)
        {
            _blockLeft--;
            UpdateBlockLabel();

            if (_blockLeft <= 0)
            {
                tmrBlock.Stop();
                btnLogin.Enabled          = true;
                txtCaptcha.Enabled        = true;
                btnRefreshCaptcha.Enabled = true;
                lblBlock.Visible          = false;
                lblError.Visible          = false;
                txtCaptcha.Focus();
            }
        }

        private void UpdateBlockLabel()
        {
            lblBlock.Text = "Повторная попытка через " + _blockLeft + " сек...";
        }

        // ── Вспомогательные методы управления CAPTCHA-панелью ─────────
        private void ShowCaptcha()
        {
            GenerateCaptcha();
            lblCaptchaPrompt.Visible  = true;
            pbCaptcha.Visible         = true;
            btnRefreshCaptcha.Visible = true;
            txtCaptcha.Visible        = true;
            lblError.Visible          = false;
            lblBlock.Visible          = false;
            txtCaptcha.Clear();
            txtCaptcha.Focus();
        }

        private void HideCaptcha()
        {
            lblCaptchaPrompt.Visible  = false;
            pbCaptcha.Visible         = false;
            btnRefreshCaptcha.Visible = false;
            txtCaptcha.Visible        = false;
            lblError.Visible          = false;
            lblBlock.Visible          = false;
        }

        private void btnRefreshCaptcha_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
            txtCaptcha.Clear();
            txtCaptcha.Focus();
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка авторизации",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ClearPasswordField()
        {
            txtPassword.Clear();
            txtPassword.Focus();
        }

        // ── Роутинг после успешного входа ─────────────────────────────
        private void OpenRoleMenu()
        {
            switch (CurrentSession.RoleId)
            {
                case 3: new AdminMenu().Show();            break;
                case 2: new ManagerMenu().Show();          break;
                case 1: new MerchandiserMenuForm().Show(); break;
                default:
                    MessageBox.Show("Неизвестная роль пользователя.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }
            // Issue #14 — запускаем слежение за бездействием
            InactivityWatcher.Start();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                    "Вы действительно хотите выйти из приложения?",
                    "Подтверждение выхода",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e) { }
    }
}
