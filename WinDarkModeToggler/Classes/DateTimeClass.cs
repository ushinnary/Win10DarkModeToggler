using System;

namespace WinDarkModeToggler.Classes
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