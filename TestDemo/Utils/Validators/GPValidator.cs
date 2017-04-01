using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TestDemo
{
	public class GPValidator
	{
		public GPValidator(){}

		public static bool isEmailOK(string emailID) {
			bool invalid = false;
			if (String.IsNullOrEmpty(emailID))
				return false;

			if (invalid)
				return false;

			// Return true if strIn is in valid e-mail format.
			try
			{
				return Regex.IsMatch(emailID,
					  @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
					  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
					  RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
			}
			catch (RegexMatchTimeoutException)
			{
				return false;
			}
		}

	}
}
