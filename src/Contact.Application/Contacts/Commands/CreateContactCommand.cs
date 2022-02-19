using Contact.Application.Common.Interfaces;
using Contact.Domain.Entities;
using MediatR;


namespace Contact.Application.Contacts.Commands
{

    public class CreateContactCommand : IRequest<CreateContactResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
    }
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, CreateContactResponse>
    {
        private readonly IContactDbContext _context;

        public CreateContactCommandHandler(IContactDbContext context)
        {
            _context = context;
        }

        public async Task<CreateContactResponse> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var user = new User();
            var contact = user.CreateContact(request.FirstName, request.LastName, request.CompanyName);

            await _context.Contacts.AddAsync(contact, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new CreateContactResponse
            {
                ContactId = contact.Id
            };
        }
    }


    public class CreateContactResponse
    {
        public int ContactId { get; set; }
    }
}
