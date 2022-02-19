using Contact.Application.Common.Exceptions;
using Contact.Application.Common.Interfaces;
using Contact.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.ContactInformations.Commands
{
    public class CreateContactInformationCommand : IRequest<CreateContactInformationResponse>
    {
        public int ContactId { get; set; }
        public ContactInformation.Type ContactInformationType { get; set; }
        public string Content { get; set; }
    }
    public class CreateContactInformationCommandHandler : IRequestHandler<CreateContactInformationCommand, CreateContactInformationResponse>
    {
        IContactDbContext _context;

        public CreateContactInformationCommandHandler(IContactDbContext context)
        {
            _context = context;
        }

        public async Task<CreateContactInformationResponse> Handle(CreateContactInformationCommand request, CancellationToken cancellationToken)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(m => m.Id == request.ContactId);
            if (contact is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Contact), request.ContactId);
            }

            var user = new User();
            var contactInformation = user.CreateContactInformation(request.ContactInformationType, request.Content, contact);

            await _context.ContactInformations.AddAsync(contactInformation, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateContactInformationResponse
            {
                ContactInformationId = contactInformation.Id
            };
        }
    }
    public class CreateContactInformationResponse
    {
        public int ContactInformationId { get; set; }
    }
}
