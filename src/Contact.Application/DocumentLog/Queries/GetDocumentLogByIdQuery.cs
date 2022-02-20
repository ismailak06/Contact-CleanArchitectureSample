using AutoMapper;
using Contact.Application.Common.Exceptions;
using Contact.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contact.Application.DocumentLog.Queries
{
    public class GetDocumentLogByIdQuery : IRequest<GetDocumentLogByIdQueryResponse>
    {
        public int DocumentLogId { get; set; }
    }

    public class GetDocumentLogByIdQueryHandler : IRequestHandler<GetDocumentLogByIdQuery, GetDocumentLogByIdQueryResponse>
    {
        IContactDbContext _context;
        IMapper _mapper;
        public GetDocumentLogByIdQueryHandler(IContactDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetDocumentLogByIdQueryResponse> Handle(GetDocumentLogByIdQuery request, CancellationToken cancellationToken)
        {
            var documentLog = await _context.DocumnetLogs.FirstOrDefaultAsync(m => m.Id == request.DocumentLogId, cancellationToken);
            if (documentLog is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.DocumentLog), request.DocumentLogId);
            }

            return _mapper.Map<GetDocumentLogByIdQueryResponse>(documentLog);
        }
    }
    public class GetDocumentLogByIdQueryResponse
    {
        public int DocumentLogId { get; set; }
        public Domain.Entities.DocumentLog.Status ProcessStatus { get; set; }
        public string ProcessStatusDisplayName { get; set; }

        public DateTime RequestDate { get; set; }
        public DateTime? CompleteDate { get; set; }
    }
}
