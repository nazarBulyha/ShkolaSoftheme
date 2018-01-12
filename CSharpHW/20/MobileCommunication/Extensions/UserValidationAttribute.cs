namespace MobileCommunication.Extensions
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Text.RegularExpressions;

	using MobileCommunication.Models;

	public class UserValidationAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			if (!(value is User user))
				return false;

			const string ValidEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
											 + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
											 + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
			var validEmailRegex = new Regex(ValidEmailPattern, RegexOptions.IgnoreCase);

			if (user.Name.Length <= 3 && user.Name.Length >= 50)
			{
				ErrorMessage = "Wrong name length.";
				return false;
			}

			if (user.Surname.Length <= 3 && user.Surname.Length >= 50)
			{
				ErrorMessage = "Wrong surname length.";
				return false;
			}

			if (!validEmailRegex.IsMatch(user.Email))
			{
				ErrorMessage = "Wrong e-mail format.";
			}

			if (!DateTime.TryParse(user.DateBirth.ToString("r"), out var temp))
			{
				ErrorMessage = "Wrong data format.";
				return false;
			}

			// ReSharper disable once InvertIf
			if (user.Number < 0 && user.Number < 2219320)
			{
				ErrorMessage = "Wrong number.";
				return false;
			}

			return true;
		}
	}
}