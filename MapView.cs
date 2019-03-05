using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace YellowSnow
{
    public static class MapView
    {
        static SoftImage buffer = null;

        public static SoftImage RenderBox(SoftImage source, Annotations annotations, int start, int end)
        {
            int imageWidth = source.width;
            int imageHeight = source.height;

            if (buffer == null || buffer.width != imageWidth || buffer.height != source.height)
                buffer = new SoftImage(imageWidth, imageHeight);

            int x1 = 1;
            int x2 = imageWidth - 2;
            int y1 = (start * imageHeight) / annotations.GetNumLines();
            int y2 = ((end + 1) * imageHeight) / annotations.GetNumLines();

            buffer.Copy(source, 0, 0);
            buffer.Rectangle(Color.FromRgb(40, 40, 40), x1, y1, x2, y2);

            return buffer;
        }
    }
}
