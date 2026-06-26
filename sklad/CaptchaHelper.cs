using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace sklad
{
    public static class CaptchaHelper
    {
        private static Random rand = new Random();

        /// <summary>
        /// Генерирует случайный код (буквы + цифры)
        /// </summary>
        public static string GenerateCode(int length = 5)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ0123456789";
            char[] code = new char[length];
            for (int i = 0; i < length; i++)
                code[i] = chars[rand.Next(chars.Length)];
            return new string(code);
        }

        /// <summary>
        /// Рисует полное изображение с кодом (шум, линии, искажение)
        /// </summary>
        public static Bitmap DrawFullCaptcha(string code, int width = 300, int height = 120)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightGray);
                // шумовые точки
                for (int i = 0; i < 200; i++)
                {
                    int x = rand.Next(width);
                    int y = rand.Next(height);
                    bmp.SetPixel(x, y, Color.Gray);
                }
                // случайные линии
                using (Pen pen = new Pen(Color.Blue, 1.5f))
                {
                    for (int i = 0; i < 3; i++)
                        g.DrawLine(pen, rand.Next(width), rand.Next(height), rand.Next(width), rand.Next(height));
                }
                // текст с искажением
                Font font = new Font("Arial", 32, FontStyle.Bold);
                PointF point = new PointF(30, 30);
                // небольшой наклон
                using (Matrix matrix = new Matrix())
                {
                    matrix.Shear(0.1f, 0.1f);
                    g.Transform = matrix;
                    g.DrawString(code, font, Brushes.DarkRed, point);
                }
            }
            return bmp;
        }

        /// <summary>
        /// Разрезает изображение на 4 горизонтальные полосы и перемешивает их.
        /// Возвращает массив из 4 картинок и правильный порядок (массив индексов).
        /// </summary>
        public static (Bitmap[] parts, int[] correctOrder) GetShuffledParts(string code, int width = 300, int height = 120)
        {
            Bitmap full = DrawFullCaptcha(code, width, height);
            int partHeight = height / 4;  // 30px при высоте 120
            Bitmap[] parts = new Bitmap[4];
            for (int i = 0; i < 4; i++)
            {
                Rectangle rect = new Rectangle(0, i * partHeight, width, partHeight);
                parts[i] = full.Clone(rect, full.PixelFormat);
            }
            // правильный порядок: 0,1,2,3
            int[] correct = { 0, 1, 2, 3 };
            // перемешиваем массив частей и запоминаем новый порядок
            var shuffled = parts.Select((part, idx) => new { part, idx }).OrderBy(x => rand.Next()).ToArray();
            Bitmap[] shuffledParts = shuffled.Select(x => x.part).ToArray();
            int[] currentOrder = shuffled.Select(x => x.idx).ToArray();
            return (shuffledParts, currentOrder);
        }
    }
}