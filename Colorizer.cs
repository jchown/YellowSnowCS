using System.Drawing;

namespace YellowSnow
{
    class Colorizer
    {
        public static Color GetColor(int level)
        {
            Color from = Color.FromArgb(255, 255, 255);
            Color to = Color.FromArgb(255, 255, 0);

//            Color from = Color.FromArgb(40, 40, 40);
//          Color to = Color.MediumPurple;

            float dR = to.R - from.R;
            float dG = to.G - from.G;
            float dB = to.B - from.B;

            float l = level / 255.0f;
            float r = dR * l + from.R;
            float g = dG * l + from.G;
            float b = dB * l + from.B;

            return Color.FromArgb((byte)r, (byte)g, (byte)b);
        }
    }
}
