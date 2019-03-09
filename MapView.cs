using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace YellowSnow
{
    public static class MapView
    {
        static Bitmap buffer = null;

        public static Image RenderBox(Image source, Annotations annotations, int start, int end)
        {
            int imageWidth = source.Width;
            int imageHeight = source.Height;

            if (buffer == null || buffer.Width != imageWidth || buffer.Height != source.Height)
                buffer = new Bitmap(imageWidth, imageHeight);

            int x1 = 5;
            int x2 = imageWidth - 5;
            int y1 = (start * imageHeight) / annotations.GetNumLines();
            int y2 = ((end + 1) * imageHeight) / annotations.GetNumLines();

            using (var graphics = Graphics.FromImage(buffer))
            {
                graphics.DrawImage(source, 0, 0);

                var brush = new SolidBrush(Themes.Selected.fgOld);
                var pen = new Pen(brush);
                pen.Width = 2;

                graphics.DrawRectangle(pen, x1, y1, x2 - x1, y2 - y1);
            }

            return buffer;
        }
    }
}
