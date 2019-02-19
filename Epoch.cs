using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowSnow
{
    public class Epoch
    {
        public static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static long ToLong(DateTime dateTime)
        {
            return (long) (dateTime - UNIX_EPOCH).TotalSeconds;
        }
    }
}
