using Contact.Application.ContactInformations.Commands;
using FluentValidation;

namespace Contact.Application.ContactInformations.Validators
{
    public class CreateContactInformationValidator : AbstractValidator<CreateContactInformationCommand>
    {
        public CreateContactInformationValidator()
        {
            RuleFor(m => m.ContactId).GreaterThan(0).WithMessage("ContactID bilgisi 0'dan büyük olmalı");
        }
    }
}
