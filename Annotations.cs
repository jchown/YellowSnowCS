using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace YellowSnow
{
    public abstract class Annotations
    {
        public abstract int GetNumLines();

        public abstract string GetSummary(int line);

        public abstract string GetHTML(int line);

        public abstract int GetLevel(int line);

        public abstract Line GetLine(int line);

        protected void CalculateColorMap(Dictionary<long, bool> times)
        {
            timeToLevel = new Dictionary<long, int>();

            long minTime = long.MaxValue;
            long maxTime = long.MinValue;
            List<long> sorted = new List<long>();

            foreach (var time in times.Keys)
            {
                if (minTime > time)
                    minTime = time;

                if (maxTime < time)
                    maxTime = time;

                sorted.Add(time);
            }

            sorted.Sort();

            for (int i=0; i<sorted.Count; i++)
            {
                var time = sorted[i];

                double t0 = ((double)time - minTime) / (maxTime - minTime);
                double t1 = ((double)i) / sorted.Count;

                double t = t0 * t1 * t0 * t1 * t0 * t1;
                timeToLevel[time] = (int)(t * 255);
            }
        }

        public string GetHTML()
        {
            var html = new StringBuilder("<html>");
            html.Append("<head><style>");
            html.Append(".line {  white-space: pre; font-family: Courier; width:100%; font-size: 10pt; }");

            for (int i = 0; i < 256; i++)
            {
                var color = Colorizer.GetColor(i);
                html.Append(string.Format(".level_{0} {{ background-color: {1} }}", i, ToHtml(color)));
            }
            html.Append("</style></head>");

            html.Append("<body>");
            for (int i = 0; i < GetNumLines(); i++)
            {
                html.Append(string.Format("<div class='line level_{0}' id='line_{1}'>", GetLevel(i), i));
                html.Append(string.Format("<a name='line_{0}' href='#line_{0}'></a>", i));
                html.Append(GetHTML(i));
                html.Append("</div>");
            }

            html.Append("</body></html>");
            return html.ToString();
        }

        private string ToHtml(Color color)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
        }

        public SoftImage CreateImage(int width, int height)
        {
            bitmap = new SoftImage(width, height);

            if (GetNumLines() > 0)
            {
                for (int y = 0; y < height; y++)
                {
                    var level = GetLevel((y * GetNumLines()) / height);
                    var color = Colorizer.GetColor(level);
                    for (int x = 0; x < width; x++)
                        bitmap.SetPixel(x, y, color);
                }
            }
            else
            {
                Color color = Colorizer.GetColor(0);
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                        bitmap.SetPixel(x, y, color);
                }
            }

            return bitmap;
        }

        protected Dictionary<long, int> timeToLevel;

        private SoftImage bitmap = null;
    }
}