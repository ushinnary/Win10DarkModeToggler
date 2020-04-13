using System;

namespace ThemeSwitcher.Classes
{
    internal class DateTimeClass
    {
        private DateTimeClass()
        {
        }

        public static DateTime getCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
