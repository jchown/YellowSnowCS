using System;
using System.Drawing;
using System.Collections.Generic;

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
            timeToColors.Clear();

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

        protected Dictionary<long, Color> timeToColors;
    }
}