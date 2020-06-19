using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRUD
{
    class clsValFluentStr: AbstractValidator<clsKorisnik>
    {
        public clsValFluentStr()
        {
            RuleFor(x => x.ime).NotEmpty();
            RuleFor(x => x.prezime).NotEmpty().WithMessage("Please specify a first name");
            RuleFor(x => x.jmbg).NotEmpty().WithMessage("Please specify a first name");

            RuleFor(x => x.god).NotEmpty().WithMessage("Please specify a years").NotEqual(0u);
            RuleFor(x => x.email).NotEmpty().EmailAddress().WithMessage("Please specify a email");
            RuleFor(x => x.kime).NotEmpty().WithMessage("Please specify a valid postcode");
        }

        
    }
}

/*
 string s = value as string;
            if (!(string.IsNullOrEmpty(s) && string.IsNullOrWhiteSpace(s)))
            {
                if(!s.Any(char.IsLetter) && s.Length != 13)
                    return ValidationResult.ValidResult;
                else
                    return new ValidationResult(false, "Ovo polje ne sme imati brojeve, biti manje od 4 i veće od 10 karaktera!");
            }
            return new ValidationResult(false, "Obavezno popunite ovo polje!");

string s = value as string;
            if (!(string.IsNullOrEmpty(s) && string.IsNullOrWhiteSpace(s)))
            {
                if (!s.Any(char.IsDigit) && s.Length > 4 && s.Length < 10)
                    return ValidationResult.ValidResult;
                else
                    return new ValidationResult(false, "Ovo polje ne sme imati brojeve, biti manje od 4 i veće od 10 karaktera!");
            }
            return new ValidationResult(false, "Obavezno popunite ovo polje!");
 */
