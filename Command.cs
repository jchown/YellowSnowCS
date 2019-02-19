using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace YellowSnow
{
    public class Command
    {
        Strings output = new Strings();

        public Command(string program, string dir, Strings arguments)
        {
            using (Process process = new Process())
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = program;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = dir;

                process.OutputDataReceived += (sender, rcv) => Log(rcv.Data);
                process.ErrorDataReceived += (sender, rcv) => Log(rcv.Data);
                process.Start();

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
    }
}