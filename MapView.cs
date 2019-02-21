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

        static Image RenderBox(Image source, Annotations annotations, int start, int end)
        {
            int imageWidth = source.Width;
            int imageHeight = source.Height;

            if (buffer == null || buffer.Width != imageWidth || buffer.Height != source.Height)
                buffer = new Bitmap(imageWidth, imageHeight);

            int x1 = 1;
            int x2 = imageWidth - 2;
            int y1 = (start * imageHeight) / annotations.GetNumLines() + 2;
            int y2 = ((end + 1) * imageHeight) / annotations.GetNumLines() - 3;

            using (var graphics = Graphics.FromImage(buffer))
            {
                graphics.DrawImage(source, 0, 0);

                var pen = new Pen(Brushes.DarkGray);
                graphics.DrawRectangle(pen, x1, y1, x2 - x1, y2 - y1);
            }

            return buffer;
        }
    }
}
