using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace YellowSnow
{
    public class SoftImage
    {
        public readonly int width, height, stride;
        PixelFormat pf;
        byte[] pixels;

        public SoftImage(int width, int height)
        {
            this.pf = PixelFormats.Rgb24;
            this.width = width;
            this.height = height;
            stride = (width * pf.BitsPerPixel + 7) / 8;
            pixels = new byte[stride * height];
        }

        internal void SetPixel(int x, int y, Color color)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
                return;

            int offset = stride * y + x * 3;
            pixels[offset++] = color.R;
            pixels[offset++] = color.G;
            pixels[offset++] = color.B;
        }

        internal void Copy(SoftImage source, int v1, int v2)
        {
            if (v1 != 0 || v2 != 0 || source.width != width || source.height != height)
                throw new NotImplementedException();

            Array.Copy(source.pixels, pixels, source.pixels.Length);
        }

        internal void Rectangle(Color color, int x1, int y1, int x2, int y2)
        {
            for (int x = x1; x <= x2; x++)
            {
                SetPixel(x, y1, color);
                SetPixel(x, y2, color);
            }

            for (int y = y1; y <= y2; y++)
            {
                SetPixel(x1, y, color);
                SetPixel(x2, y, color);
            }
        }

        public BitmapSource ToBitmap()
        {
            return BitmapSource.Create(width, height, 96, 96, pf, null, pixels, stride);
        }
    }
}
