using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfCRUD
{
    class clsValidateGod : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (int.TryParse(value.ToString(), out int br) && br >= 18 && br <= 1000)
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Ovo nije broj :(");
        }
    }
}
