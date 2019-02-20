using System;
using System.Security;

namespace YellowSnow
{
    internal static class Format
    {
        public static string LineNumber(int n)
        {
            string s = n.ToString();
            if (s.Length < 5)
                s = s + " ";
            while (s.Length < 5)
                s = " " + s;
            return ToNBSP(s);
        }

        private static string ToNBSP(string s)
        {
            return s.Replace("\t", "    ").Replace(" ", "&nbsp;");
        }

        public static string Editor(string o)
        {
            string s = o;
            if (s.Length < 8)
                s = s + " ";
            while (s.Length < 8)
                s = " " + s;
            return s;
        }

        internal static object Timestamp(long time)
        {
            return time.ToString();
        }

        internal static string Line(string line)
        {
            return ToNBSP(SecurityElement.Escape(line));
        }
    }
}