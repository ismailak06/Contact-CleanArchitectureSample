using Contact.Application.Common.Exceptions;
using Contact.Application.Common.Interfaces;
using Contact.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contact.Application.Contacts.Commands
{
    public class DeleteContactInformationCommand : IRequest<DeleteContactResponse>
    {
        public int ContactInformationId { get; set; }
    }

    public class DeleteContactInformationCommandHandler : IRequestHandler<DeleteContactInformationCommand, DeleteContactResponse>
    {
        private readonly IContactDbContext _context;
        public DeleteContactInformationCommandHandler(IContactDbContext context)
        {
            _context = context;
        }
        public async Task<DeleteContactResponse> Handle(DeleteContactInformationCommand request, CancellationToken cancellationToken)
        {
            var contact = await _context.ContactInformations.FirstOrDefaultAsync(m => m.Id == request.ContactInformationId, cancellationToken);
            if (contact is null)
            {
                throw new NotFoundException(nameof(ContactInformation), request.ContactInformationId);
            }

            _context.ContactInformations.Remove(contact);
            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteContactResponse
            {
                ContactId = contact.Id
            };
        }
    }

    public class DeleteContactInformationResponse
    {
        public int ContactId { get; set; }
    }
}
