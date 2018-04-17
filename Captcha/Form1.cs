using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Captcha
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
            SetRandomizedCode(); // Создаём код для капчи из букв и цифр
            SetCaptcha(); // Рисуем капчу
            picCaptcha.Image = CaptchaImg; // Показываем капчу
        }

        private Bitmap _captcha = new Bitmap(351, 70); // Рисунок капчи (351x70)
        private String _code = String.Empty; // Код капчи
        private Random rand = new Random(); // Для создания кодов

        // Геттеры
        public Bitmap CaptchaImg { get { return _captcha; } }
        public String Code { get { return _code; } }

        /// <summary>
        /// Создание рисунка капчи
        /// </summary>
        private void SetCaptcha()
        {
            // Задаём стиль текста
            FontStyle style = FontStyle.Italic | FontStyle.Strikeout | FontStyle.Underline | FontStyle.Regular | FontStyle.Bold;
            Font font = new Font("Arial", 50f, style);

            // Создаём графический объект
            Graphics GFX = Graphics.FromImage(_captcha);

            // Задаём отрисовку со сглаживанием (AntiAlias)
            GFX.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // Заполняём весь объект цветом LightBlue
            GFX.FillRectangle(Brushes.LightBlue, 0, 0, _captcha.Width, _captcha.Height);
            
            // Создаём шум на фоне в виде квадратов 15x15, разного цвета
            for (int j = 0; j < 351; j += 15)
                for (int i = 0; i < 70; i += 15)
                {
                    Color color = Color.FromArgb(rand.Next());
                    SolidBrush brush = new SolidBrush(color);
                    GFX.FillRectangle(brush, j, i, 15, 15);
                }

            int b = 25; // Отвечает за положение символа на оси X
            int c = -5; // Отвечает за положение символа на оси Y
            Char[] _arrCode = _code.ToArray(); // Сгенерированный код разбиваем посимвольно
            
            // Рисуем символы на графическом объекте
            for (int i = 0; i < 7; i++)
            {
                // Создаём кисть случайного цвета
                Color color = Color.FromArgb(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256));
                SolidBrush brush = new SolidBrush(color);

                // Рисуем символ
                GFX.DrawString(_arrCode[i].ToString(), font, brush, new Point(b, c));

                b += 40; // Увеличиваем для того, чтобы отодвинуть следующий символ по оси X

                // Рандомизируем высоту символов по оси Y
                if (rand.Next(0, 2) == 1) c -= 5;
                else c += 5;
            }
        }

        /// <summary>
        /// Генерация кода для капчи
        /// </summary>
        private void SetRandomizedCode()
        {
            _code = String.Empty;

            for (int i = 0; i < 7; i++)
                // Для рандомизации последовательности чисел и букв
                if(rand.Next(0, 2) == 1) _code += rand.Next(0, 10).ToString(); // Любые числа в пределе от 0 до 10
                else _code += (char)(rand.Next(1040, 1104)); // 1040 - А — 1071 - Я // 1072 - а — 1103 - я
        }

        // При нажатии на pictureBox
        private void picCaptcha_Click(object sender, EventArgs e)
        {
            SetRandomizedCode(); // Создаём код для капчи из букв и цифр
            SetCaptcha(); // Рисуем капчу
            picCaptcha.Image = CaptchaImg; // Показываем капчу
        }

        // Проверить
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Code == txtAnswer.Text)
            {
                MessageBox.Show("Ответ верный!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAnswer.Clear();
                SetRandomizedCode(); // Создаём код для капчи из букв и цифр
                SetCaptcha(); // Рисуем капчу
                picCaptcha.Image = CaptchaImg; // Показываем капчу
            }
            else
            {
                MessageBox.Show("Ответ неверный!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAnswer.Clear();
                SetRandomizedCode(); // Создаём код для капчи из букв и цифр
                SetCaptcha(); // Рисуем капчу
                picCaptcha.Image = CaptchaImg; // Показываем капчу
            }
        }
    }
}
