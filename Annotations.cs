using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace YellowSnow
{
    public abstract class Annotations
    {
        public abstract int GetNumLines();

        public abstract string GetSummary(int line);

        public abstract string GetHTML(int line);

        public abstract Color GetColor(int line);

        protected void CalculateColorMap(Dictionary<long, bool> times)
        {
            timeToColors = new Dictionary<long, Color>();

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
                int l = (int)((1 - t) * 255);
                timeToColors[time] = Color.FromArgb(255, 255, l);
            }
        }

        public string GetHTML()
        {
            var html = new StringBuilder("<html><body>");
            html.Append("<font face='Courier'>");

            for (int i = 0; i < GetNumLines(); i++)
            {
                var color = GetColor(i);
                html.Append("<div style='background-color: " + ColorTranslator.ToHtml(color) + "; white-space: pre'>");
                html.Append("<a name='line_" + i + "' href='#line_" + i + "'></a>");
                html.Append(GetHTML(i));
                html.Append("</div>");
            }

            html.Append("</font>");
            html.Append("</body></html>");
            return html.ToString();
        }

        public Image CreateImage(int width, int height)
        {
            bitmap = new Bitmap(width, height);

            if (GetNumLines() > 0)
            {
                for (int y = 0; y < height; y++)
                {
                    var color = GetColor((y * GetNumLines()) / height);
                    for (int x = 0; x < width; x++)
                        bitmap.SetPixel(x, y, color);
                }
            }
            else
            {
                using (var graphics = Graphics.FromImage(bitmap))
                    graphics.Clear(Color.White);
            }

            return bitmap;
        }

        protected Dictionary<long, Color> timeToColors;

        private Bitmap bitmap;
    }
}