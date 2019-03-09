using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;

namespace YellowSnow
{
    public class AnnotaterNull : Annotater
    {
        public AnnotaterNull()
        {
        }

        public override Annotations GetAnnotationsFile(string filename)
        {
            var textLines = File.ReadAllLines(filename);

            FileAttributes attr = File.GetAttributes(filename);
            string editor = File.GetAccessControl(filename).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
            long edited = Epoch.ToLong(File.GetLastAccessTime(filename));

            List<Line> lines = new List<Line>();
            for (int i = 0; i < textLines.Length; i++)
            {
                lines.Add(new Line()
                {
                    editor = editor,
                    line = textLines[i],
                    time = edited
                });
            }

            return new AnnotationsVCS(lines);
        }

        public override Annotations GetAnnotationsDir(string filename)
        {
            List<FSEntry> lines = new List<FSEntry>();

            /*
            foreach (var entry in Directory.EnumerateFileSystemEntries(filename))
            {
                QFile file(dir.filePath(entry));

                QFileInfo info(file);
                string editor = info.owner();
                DateTime edited = info.lastModified();
                string commitHash, authorName, authorEmail, subject;

                //  Get the most recent edit

                QStringList arguments;
                arguments << "info" << "--xml" << file.fileName();

                Command command(program, dir.absolutePath(), arguments);
                auto xml = command.getXML();

                auto commits = xml.elementsByTagName("commit");
                if (commits.count() > 0)
                {
                    var commit = commits.item(0);
                    editor = commit.firstChildElement("author").text();
                    var dateText = commit.firstChildElement("date").text().left(23);
                    edited = QDateTime::fromString(dateText, dateFormat);
                }

                lines.append(File(editor, entry, edited, info));
            }
            */

            return new AnnotationsFS(lines);
        }

        public override bool IsInWorkspace(string filename)
        {
            throw new Exception("The null annotater knows no workspaces");
        }
    }
}
