using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfCRUD
{
    class clsValidateJMBG : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string vrijednost = value as string;
            if (!(string.IsNullOrEmpty(vrijednost) && string.IsNullOrWhiteSpace(vrijednost) && vrijednost.All(char.IsDigit)))
            {      
                //0- 0123
                //1- any num 0-9
                //2- 01
                //3- any num 0-9
                //4- 90

                //0  1  2  3  4  5  
                //3  0  0  7  9  8  6  7  8  3  9  2  5


                if (vrijednost[0] != 0 || 
                    vrijednost[0] != 1 || 
                    vrijednost[0] != 2 || 
                    vrijednost[0] != 3 || //mjesec ne može imati više od 30 dana - 0123

                    vrijednost[2] != 0 || //mjesec ne može biti više od 10 - 0 ili 1
                    vrijednost[2] != 1 ||
                    
                    vrijednost[4] != 9 || //godina može biti ili 9 ili 0
                    vrijednost[4] != 0
                    )
                    return ValidationResult.ValidResult;
                else
                    return new ValidationResult(false, "Prvo slovo veliko, manje od 4 i veše od 10 karaktera, ne može sadržato broj!");
            }
            return new ValidationResult(false, "Unesite nesto :(");
        }
    }
}
