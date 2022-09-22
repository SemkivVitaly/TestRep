using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace OOOPovarenok.View
{
    public partial class Capcha : Form
    {
        public Capcha()
        {
            InitializeComponent();
        }
        string text;

        /// <summary>
        /// Метод для создания картинки
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        private Bitmap CreateImage(int Width, int Height)
        {
            Random rnd = new Random();

            Bitmap result = new Bitmap(Width, Height);

            int Xpos = 10;
            int Ypos = 10;

            Brush[] colors = {
             Brushes.Black,
             Brushes.Red,
             Brushes.Yellow,
             Brushes.White,
             Brushes.Pink };

            Pen[] colorpens = {
             Pens.Black,
             Pens.Red,
             Pens.Green,
             Pens.Yellow,
             Pens.White,
             Pens.Pink };

            FontStyle[] fontstyle = {
             FontStyle.Bold,
             FontStyle.Italic,
             FontStyle.Regular,
             FontStyle.Strikeout,
             FontStyle.Underline};

            Int16[] rotate = { 1, -1, 2, -2, 3, -3, 4, -4, 5, -5, 6, -6 };

            Graphics g = Graphics.FromImage((Image)result);

            g.Clear(Color.Gray);

            g.RotateTransform(rnd.Next(rotate.Length));

            text = String.Empty;
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];

            g.DrawString(text,
                         new Font("Arial", 25, fontstyle[rnd.Next(fontstyle.Length)]),
                         colors[rnd.Next(colors.Length)],
                         new PointF(Xpos, Ypos));
    
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)],
                       new Point(0, 0),
                       new Point(Width - 1, Height - 1));
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)],
                       new Point(0, Height - 1),
                       new Point(Width - 1, 0));

            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 30 == 0)
                        result.SetPixel(i, j, Color.White);

            return result;
        }

        /// <summary>
        /// Начальные настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Capcha_Load(object sender, EventArgs e)
        {
            pictureBoxMain.Image = this.CreateImage(pictureBoxMain.Width, pictureBoxMain.Height);
        }

        /// <summary>
        /// Генерация новой капчи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewCapcha_Click(object sender, EventArgs e)
        {
            pictureBoxMain.Image = this.CreateImage(pictureBoxMain.Width, pictureBoxMain.Height);

        }

        /// <summary>
        /// Проверка совпадения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCheck_Click(object sender, EventArgs e)
        {
            if (textBoxProverka.Text == this.text)
                MessageBox.Show("Верно!");
            else
                MessageBox.Show("Капча введена не верно!");
        }

        /// <summary>
        /// Возвращение к авторизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Autorisation().ShowDialog();
            this.Show();
        }
    }
}
