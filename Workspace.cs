﻿using System.IO;

namespace YellowSnow
{
    internal class Workspace
    {
        internal static bool FindDir(string baseDir, string dirName, ref string dir)
        {
            string d = baseDir + Path.DirectorySeparatorChar + dirName;
            if (Directory.Exists(d))
            {
                dir = baseDir;
                return true;
            }

            int slash = baseDir.LastIndexOf(Path.DirectorySeparatorChar);
            if (slash > 0)
            {
                return FindDir(baseDir.Substring(0, slash), dirName, ref dir);
            }

            return false;
        }
    }
}