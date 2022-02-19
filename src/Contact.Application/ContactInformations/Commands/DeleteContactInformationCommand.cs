using Contact.Application.Common.Exceptions;
using Contact.Application.Common.Interfaces;
using Contact.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contact.Application.ContactInformations.Commands
{
    public class DeleteContactInformationCommand : IRequest<DeleteContactInformationResponse>
    {
        public int ContactId { get; set; }
        public int ContactInformationId { get; set; }
    }

    public class DeleteContactInformationCommandHandler : IRequestHandler<DeleteContactInformationCommand, DeleteContactInformationResponse>
    {
        private readonly IContactDbContext _context;
        public DeleteContactInformationCommandHandler(IContactDbContext context)
        {
            _context = context;
        }
        public async Task<DeleteContactInformationResponse> Handle(DeleteContactInformationCommand request, CancellationToken cancellationToken)
        {
            var contact = await _context.ContactInformations.FirstOrDefaultAsync(m => m.Id == request.ContactInformationId && m.Contact.Id == request.ContactId, cancellationToken);
            if (contact is null)
            {
                throw new NotFoundException(nameof(ContactInformation), request.ContactInformationId);
            }

            _context.ContactInformations.Remove(contact);
            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteContactInformationResponse
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
