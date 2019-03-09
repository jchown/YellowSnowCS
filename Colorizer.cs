using System.Drawing;

namespace YellowSnow
{
    class Colorizer
    {
        public static Color GetBGColor(int level)
        {
            Color from = Themes.Selected.bgOld;
            Color to = Themes.Selected.bgNew;

            return GetColor(level, from, to);
        }

        public static Color GetFGColor(int level)
        {
            Color from = Themes.Selected.fgOld;
            Color to = Themes.Selected.fgNew;

            return GetColor(level, from, to);
        }

        private static Color GetColor(int level, Color from, Color to)
        {
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
