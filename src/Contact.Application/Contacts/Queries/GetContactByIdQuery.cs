using AutoMapper;
using Contact.Application.Common.Exceptions;
using Contact.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Contacts.Queries
{
    public class GetContactByIdQuery : IRequest<GetContactByIdResponse>
    {
        public int ContactId { get; set; }
    }
    public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, GetContactByIdResponse>
    {
        IContactDbContext _context;
        IMapper _mapper;

        public GetContactByIdQueryHandler(IContactDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetContactByIdResponse> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            var contact = await _context.Contacts.Include(m => m.ContactInformations).FirstOrDefaultAsync(m => m.Id == request.ContactId, cancellationToken);
            if (contact is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Contact), request.ContactId);
            }

            return _mapper.Map<GetContactByIdResponse>(contact);
        }
    }
    public class GetContactByIdResponse
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public List<ContactInformationResponse> ContactInformations { get; set; }
    }

    public class ContactInformationResponse
    {
        public int ContactInformationId { get; set; }
        public int InformationType { get; set; }
        public string InformationTypeDisplayName { get; set; }
        public string Content { get; set; }
    }
}
