using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowSnow
{
    public static class StringExt
    {

        /// <summary>
        /// For "filename.ext" return "ext"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>

        public static string GetFileExt(this string str)
        {
            int lastDot = str.LastIndexOf('.');
            if (lastDot == -1)
                return "";

            return str.Substring(lastDot + 1);
        }

        /// <summary>
        /// For "some/path/file.ext" return "file.ext"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>

        public static string GetFileName(this string str)
        {
            int lastSlash = PathSeperatorIndex(str);
            if (lastSlash == -1 || str.Length == (lastSlash + 1))
                return str;

            return str.Substring(lastSlash + 1);
        }

        /// <summary>
        /// If this string is (say) "a/b/c", this function returns "a/b"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>

        public static string GetFilePath(this string str)
        {
            int lastSlash = PathSeperatorIndex(str);
            if (lastSlash == -1)
                return str;

            // if we don't have a slash we've stripped it too far back
            string tmp = str.Substring(0, lastSlash);
            int tmpSlash = PathSeperatorIndex(tmp);
            if (tmpSlash != -1)
                return tmp;

            return str.Substring(0, lastSlash + 1);
        }

        private static int PathSeperatorIndex(this string str)
        {
            return str.LastIndexOf(Path.DirectorySeparatorChar);
        }
    }
}
