using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace YellowSnow
{
    internal class AnnotationsFS : Annotations
    {
        private List<FSEntry> files;

        public AnnotationsFS(List<FSEntry> files)
        {
            this.files = files;

            var times = new Dictionary<long, bool>();
            foreach (var file in files)
                times[file.modified] = true;

            CalculateColorMap(times);
        }

        override public int GetNumLines()
        {
            return files.Count;
        }

        override public Color GetColor(int line)
        {
            var file = files[line];
            return timeToColors[file.modified];
        }

        override public string GetSummary(int line)
        {
            return "";
//            return "File '" + files[line].getName() + "', modified by " + files[line].getEditor() + ", " + format_time(files[line].getModifiedTime());
        }

        override public string GetHTML(int line)
        {
            return "";
//            QUrl url = QUrl::fromLocalFile(files[line].getPath() + "/" + files[line].getName());
//           return "<div><img src='/icon.png' width='16px' height='16px' /><a href='" + url.toString() + "'>" + files[line].getName().toHtmlEscaped() + "</a></div>";
        }
    }
}