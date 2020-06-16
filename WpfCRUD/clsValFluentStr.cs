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
