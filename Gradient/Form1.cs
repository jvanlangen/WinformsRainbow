using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gradient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            DrawRainbow();
        }

        private void DrawRainbow()
        {
            // specify some colors.
            var colors = new[] { Color.Red, Color.Yellow, Color.Green, Color.Blue };


            var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            // draw the rainbow
            using (var g = Graphics.FromImage(bitmap))
            {
                // for each verticalline
                for (var x = 0; x < bitmap.Width; x++)
                {
                    // transform x (0..width) to 0..1
                    var value = x / (double)bitmap.Width;

                    // get the color corresponding to 0.5 of all colors.
                    var color = GetRainbowColor(value, colors);

                    // draw vertical line
                    using (var pen = new Pen(color))
                        g.DrawLine(pen, x, 0, x, bitmap.Height);
                }
            }

            pictureBox1.Image = bitmap;
        }

        public Color GetRainbowColor(double value, Color[] colors)
        {
            // calculate the count of transitions between colors.
            var colorRange = colors.Length - 1;

            // scale the value in order of the transition count.
            var newValue = value * colorRange;

            // truncate the value to get the starting index.
            var startIndex = (int)newValue;

            // the residual value to get the transition point between the two colors.
            var residualValue = newValue - startIndex;

            // reuse a modified version of the old HeatMapColor function.
            return LerpColor(residualValue, colors[startIndex], colors[startIndex + 1]);
        }


        public Color LerpColor(double value, Color startColor, Color endColor)
        {
            // reuse some code from the HeatMapColor

            int r = (int)((endColor.R * value) + (startColor.R * (1 - value)));
            int g = (int)((endColor.G * value) + (startColor.G * (1 - value)));
            int b = (int)((endColor.B * value) + (startColor.B * (1 - value)));

            return Color.FromArgb(r, g, b);
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            DrawRainbow();
        }
    }
}
