using AutoMapper;
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
    public class GetAllContactsQuery : IRequest<List<GetAllContactResponse>>
    {
    }

    public class GetAllContactsHandler : IRequestHandler<GetAllContactsQuery, List<GetAllContactResponse>>
    {
        private readonly IContactDbContext _context;
        private readonly IMapper _mapper;
        public GetAllContactsHandler(IContactDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetAllContactResponse>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
        {
            var contacts = await _context.Contacts.ToListAsync(cancellationToken);
            return _mapper.Map<List<GetAllContactResponse>>(contacts);
        }
    }

    public class GetAllContactResponse
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
    }
}
