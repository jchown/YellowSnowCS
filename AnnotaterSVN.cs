﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;

namespace YellowSnow
{
    public class AnnotaterSVN : Annotater
    {
        private string program;
        private string dateFormat;

        public AnnotaterSVN()
        {
            program = "svn";
            dateFormat = "yyyy-MM-ddTHH:mm:ss.zzz";
        }

        public override Annotations GetAnnotationsFile(string filename)
        {
            var textLines = File.ReadAllLines(filename);

            string dir = filename.GetFilePath();
            Strings arguments = new Strings();
            arguments.Add("annotate");
            arguments.Add("--xml");
            arguments.Add(filename);

            Command command = new Command(program, dir, arguments);

            List<Line> lines = new List<Line>();
            var output = command.GetOutput();
            if (command.GetExitCode() != 0)
                throw GetCommandException(command);

            var entries = output.ToXML().GetElementsByTagName("entry");
            for (int i = 0; i < entries.Count; i++)
            {
                // <entry line-number="1555">
                // <commit revision="1182">
                // <author>limpbizkit@gmail.com</author>
                // <date>2012-08-26T19:31:06.283991Z</date>
                // </commit>
                // </entry>

                var entry = entries[i];
                int lineNum = int.Parse(entry.Attributes.GetNamedItem("line-number").Value);
                var commit = GetFirstChildElement(entry, "commit");

                string editor = GetFirstChildElement(commit, "author").InnerText;
                string dateText = GetFirstChildElement(commit, "date").InnerText.Substring(0, 23);
                DateTime edited = DateTime.Parse(dateText);//, dateFormat);

                lines.Add(new Line()
                {
                    editor = editor,
                    line = textLines[lineNum - 1],
                    time = Epoch.ToLong(edited)
                });
            }

            return new AnnotationsVCS(lines);
        }

        private Exception GetCommandException(Command command)
        {
            foreach (var line in command.GetOutput())
            {
                if (line.StartsWith("svn:"))
                    throw new Exception(line);
            }

            throw new Exception("svn returned exit code " + command.GetExitCode());
        }

        private XmlNode GetFirstChildElement(XmlNode parent, string name)
        {
            XmlNode child = parent.FirstChild;
            do
            {
                if (child.Name == name)
                    break;

                child = child.NextSibling;
            }
            while (child != null);

            return child;
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
            return Workspace.FindDir(filename, ".svn", ref workspaceRoot);
        }
    }
}
