using System;
using System.Configuration;
using System.Windows.Forms;

namespace FurnitureStoreApp
{
    /// <summary>
    /// Issue #13 — Следит за бездействием пользователя.
    /// Таймаут читается из App.Config (ключ InactivityTimeoutSeconds, по умолчанию 30).
    /// При истечении — закрывает все открытые формы и показывает LoginForm.
    /// </summary>
    public static class InactivityWatcher
    {
        private static Timer  _timer;
        private static int    _timeoutMs;
        private static bool   _active = false;

        public static void Start()
        {
            // Читаем таймаут из конфига
            int seconds = 30;
            string cfg  = ConfigurationManager.AppSettings["InactivityTimeoutSeconds"];
            if (!string.IsNullOrEmpty(cfg) && int.TryParse(cfg, out int parsed) && parsed > 0)
                seconds = parsed;

            _timeoutMs = seconds * 1000;

            if (_timer == null)
            {
                _timer          = new Timer();
                _timer.Tick    += OnTimeout;
            }

            _timer.Interval = _timeoutMs;
            _timer.Start();
            _active = true;

            // Подписываемся на глобальные события мыши и клавиатуры
            Application.AddMessageFilter(new ActivityFilter());
        }

        public static void Stop()
        {
            _active = false;
            _timer?.Stop();
        }

        /// <summary>Сбрасывает таймер — вызывается при любой активности.</summary>
        public static void Reset()
        {
            if (_active && _timer != null)
            {
                _timer.Stop();
                _timer.Start();
            }
        }

        private static void OnTimeout(object sender, EventArgs e)
        {
            _timer.Stop();
            _active = false;

            // Закрываем все формы кроме LoginForm
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Form f = Application.OpenForms[i];
                if (!(f is LoginForm))
                    f.Close();
            }

            // Показываем форму авторизации
            var login = new LoginForm();
            login.Show();
        }

        // ── Фильтр сообщений Windows — перехватывает мышь и клавиши ──
        private class ActivityFilter : IMessageFilter
        {
            // WM_MOUSEMOVE=0x0200, WM_LBUTTONDOWN=0x0201, WM_KEYDOWN=0x0100, WM_KEYUP=0x0101
            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg == 0x0200 || m.Msg == 0x0201 ||
                    m.Msg == 0x0100 || m.Msg == 0x0101)
                    InactivityWatcher.Reset();
                return false; // не блокируем сообщение
            }
        }
    }
}
