using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YellowSnow
{
    public class Themes
    {
        public static Theme Selected
        {
            get
            {
                switch (Properties.Settings.Default.Theme)
                {
                    case "YS":
                        return YELLOW_SNOW;
                    case "DB":
                        return DARK_BRUISES;
//                    case "SM":
//                        return SKID_MARKS;
                }

                return YELLOW_SNOW;
            }

            set
            {
                Properties.Settings.Default.Theme = value.code;
                Properties.Settings.Default.Save();
            }
        }

        public static readonly Theme YELLOW_SNOW = new Theme
        {
            code = "YS",
            name = "Yellow Snow",
            fgNew = Color.Black,
            fgOld = Color.Black,
            bgNew = Color.Yellow,
            bgOld = Color.White
        };

        public static readonly Theme DARK_BRUISES = new Theme
        {
            code = "DB",
            name = "Dark Bruises",
            fgNew = Color.Yellow,
            fgOld = Color.White,
            bgNew = Color.FromArgb(87, 38, 128),
            bgOld = Color.FromArgb(30, 30, 30)
        };
    }
}
