﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Security;

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

        override public Line GetLine(int line)
        {
            return lines[line];
        }

        override public int GetNumLines()
        {
            return lines.Count;
        }

        override public int GetLevel(int line)
        {
            return timeToLevel[lines[line].time];
        }

        override public string GetSummary(int line)
        {
            return string.Format("Line {0}/{1}, edited by {2}, {3}", line + 1, lines.Count, lines[line].editor, Format.Timestamp(lines[line].time));
        }

        override public string GetHTML(int line)
        {
            return Format.LineNumber(line + 1) + Format.Line(lines[line].line);
        }
    }
}
