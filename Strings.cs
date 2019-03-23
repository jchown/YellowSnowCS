using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Xml;

namespace YellowSnow
{
    public class Strings : List<string>
    {
        public Strings()
        {
        }

        public Strings(IEnumerable<string> collection) : base(collection)
        {
        }

        public string Join(string separator)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string s in this)
            {
                if (sb.Length != 0)
                    sb.Append(separator);

                sb.Append(s);
            }

            return sb.ToString();
        }

        internal static Strings Load(string filename)
        {
            return new Strings(File.ReadAllText(filename).Split('\n'));
        }

        public XmlDocument ToXML()
        {
            var xml = new XmlDocument();
            xml.LoadXml(Join("\n"));
            return xml;
        }
    }
}