using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowSnow
{
    class AnnotationsVCS: Annotations
    {
        private List<Line> lines;

        public AnnotationsVCS(List<Line> lines)
        {
            this.lines = lines;

            Dictionary<long, bool> times = new Dictionary<long, bool>();
            foreach (var line in lines)
                times[line.time] = true;

            CalculateColorMap(times);
        }

        override public int GetNumLines()
        {
            return lines.Count;
        }

        override public Color GetColor(int line)
        {
            return timeToColors[lines[line].time];
        }

        string lineNumberString(int n)
        {
            string s = n.ToString();
            if (s.Length < 5)
                s = s + " ";
            while (s.Length < 5)
                s = " " + s;
            return s;
        }

        string editorString(string o)
        {
            string s = o;
            if (s.Length < 8)
                s = s + " ";
            while (s.Length < 8)
                s = " " + s;
            return s;
        }

        override public string GetSummary(int line)
        {
            return "";
            //            return string.Format("Line {0}/{1}, edited by {3}, {4}",line + 1, lines.Count, lines[line].getEditor(), format_time(lines[line].getTime() );
        }

        override public string GetHTML(int line)
        {
            return "";
 //           return lineNumberString(line + 1) + lines[line].getText().toHtmlEscaped();
        }
    }
}
