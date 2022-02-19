using Contact.Application.Contacts.Commands;
using FluentValidation;


namespace Contact.Application.Contacts.Validators
{
    public class CreateContactValidator : AbstractValidator<CreateContactCommand>
    {
        private const int FIRST_NAME_MAX_LENGTH = 50;
        private const int LAST_NAME_MAX_LENGTH = 50;
        private const int COMPANY_NAME_MAX_LENGTH = 200;
        public CreateContactValidator()
        {
            RuleFor(m => m.FirstName)
                .NotEmpty().WithMessage("Kişi adı boş olamaz.")
                .NotNull().WithMessage("Kişi adı boş olamaz.")
                .MaximumLength(FIRST_NAME_MAX_LENGTH).WithMessage($"Kişinin adı {FIRST_NAME_MAX_LENGTH} karakterden uzun olamaz.");

            RuleFor(m => m.LastName)
                .NotEmpty().WithMessage("Kişi soyadı boş olamaz.")
                .NotNull().WithMessage("Kişi soyadı boş olamaz.")
                .MaximumLength(LAST_NAME_MAX_LENGTH).WithMessage($"Kişinin soyadı {LAST_NAME_MAX_LENGTH} karakterden uzun olamaz.");

            RuleFor(m => m.CompanyName)
                .MaximumLength(COMPANY_NAME_MAX_LENGTH).WithMessage($"Şirket adı {COMPANY_NAME_MAX_LENGTH} karakterden uzun olamaz.");
        }
    }
}
