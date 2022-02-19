using Contact.Application.ContactInformations.Commands;
using Contact.Application.Contacts.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.ContactInformations.Validators
{
    public class CreateContactInformationValidator: AbstractValidator<CreateContactInformationCommand>
    {
        public CreateContactInformationValidator()
        {
            RuleFor(m => m.ContactId).GreaterThan(0).WithMessage("ContactID bilgisi 0'dan büyük olmalı");
        }
    }
}
