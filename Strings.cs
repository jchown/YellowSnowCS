using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace YellowSnow
{
    public class Strings : List<string>
    {
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
    }
}