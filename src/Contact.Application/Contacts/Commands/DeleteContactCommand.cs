using Contact.Application.Common.Exceptions;
using Contact.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contact.Application.Contacts.Commands
{
    public class DeleteContactCommand : IRequest<DeleteContactResponse>
    {
        public int ContactId { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<DeleteContactCommand, DeleteContactResponse>
    {
        private readonly IContactDbContext _context;
        public CreateCategoryCommandHandler(IContactDbContext context)
        {
            _context = context;
        }
        public async Task<DeleteContactResponse> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(m => m.Id == request.ContactId, cancellationToken);
            if (contact is null)
            {
                throw new NotFoundException(nameof(Contact), request.ContactId);
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteContactResponse
            {
                ContactId = contact.Id
            };
        }
    }

    public class DeleteContactResponse
    {
        public int ContactId { get; set; }
    }
}
