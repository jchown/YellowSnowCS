using System.Drawing;

namespace YellowSnow
{
    public class Font
    {
        public static int PointSize
        {
            get
            {
                return Properties.Settings.Default.FontPointSize;
            }

            set
            {
                Properties.Settings.Default.FontPointSize = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
