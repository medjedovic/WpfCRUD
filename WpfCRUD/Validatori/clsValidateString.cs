using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfCRUD
{
    public class clsValidateString : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
			string vrijednost = value as string;
			if (!(string.IsNullOrEmpty(vrijednost) && string.IsNullOrWhiteSpace(vrijednost)))
			{
				if (char.IsUpper(vrijednost[0]) && vrijednost.Length > 3 && vrijednost.Length < 15 && !vrijednost.Any(char.IsDigit))
					return ValidationResult.ValidResult;
				else
					return new ValidationResult(false, "Prvo slovo veliko, manje od 4 i veše od 10 karaktera, ne može sadržato broj!");
			}
			return new ValidationResult(false, "Unesite nesto :(");
		}
    }
}
