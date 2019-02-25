using System;
using System.Diagnostics;
using System.Xml;

namespace YellowSnow
{
    public class Command
    {
        Strings output = new Strings();

        public Command(string program, string dir, Strings arguments, params string[] envVars)
        {
            using (Process process = new Process())
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = program;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = dir;
                process.StartInfo.Arguments = arguments.Join(" ");
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                foreach (string env in envVars)
                {
                    int eq = env.IndexOf('=');
                    process.StartInfo.Environment.Add(env.Substring(0, eq), env.Substring(eq + 1));
                }

                process.OutputDataReceived += (sender, rcv) => Log(rcv.Data);
                process.ErrorDataReceived += (sender, rcv) => Log(rcv.Data);
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                if (!process.WaitForExit(10000))
                {
                    throw new Exception("Timeout waiting " + program);
                }
            }
        }

        private void Log(string data)
        {
            lock (output)
            {
                output.Add(data);
            }
        }

        public Strings GetOutput()
        {
            return output;
        }

        public XmlDocument GetXML()
        {
            var xml = new XmlDocument();
            xml.LoadXml(GetOutput().Join("\n"));
            return xml;
        }
    }
}