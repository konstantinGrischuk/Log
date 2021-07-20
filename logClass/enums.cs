using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logClass
{
    public enum LogType
    {
        File,
        Event,
        Both
    }
    static public class DateTimePatern
    {
        public static string Full = "d MMMM yy HH:mm:ss ";
        public static string Short = "MM.dd.yyyy HH:mm:ss ";
    }
}
