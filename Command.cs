using System;
using System.Diagnostics;
using System.Xml;

namespace YellowSnow
{
    public class Command
    {
        private Strings output = new Strings();
        private int exitCode;

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

                process.OutputDataReceived += (sender, rcv) => OnData(rcv.Data);
                process.ErrorDataReceived += (sender, rcv) => OnData(rcv.Data);
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                if (!process.WaitForExit(10000))
                    throw new Exception("Timeout waiting " + program);
  
                exitCode = process.ExitCode;
            }
        }

        public Strings GetOutput()
        {
            return output;
        }

        public int GetExitCode()
        {
            return exitCode;
        }

        private void OnData(string data)
        {
            lock (output)
            {
                output.Add(data);
            }
        }
    }
}