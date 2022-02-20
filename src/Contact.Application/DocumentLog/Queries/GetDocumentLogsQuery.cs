using AutoMapper;
using Contact.Application.Common.Exceptions;
using Contact.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contact.Application.DocumentLog.Queries
{
    public class GetDocumentLogsQuery : IRequest<List<GetDocumentLogsQueryResponse>>
    {
    }

    public class GetDocumentLogsQueryHandler : IRequestHandler<GetDocumentLogsQuery, List<GetDocumentLogsQueryResponse>>
    {
        IContactDbContext _context;
        IMapper _mapper;
        public GetDocumentLogsQueryHandler(IContactDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<GetDocumentLogsQueryResponse>> Handle(GetDocumentLogsQuery request, CancellationToken cancellationToken)
        {
            var documentLogs = await _context.DocumnetLogs.OrderByDescending(m => m.CreationDate).ToListAsync(cancellationToken);
            return _mapper.Map<List<GetDocumentLogsQueryResponse>>(documentLogs);
        }
    }
    public class GetDocumentLogsQueryResponse
    {
        public int DocumentLogId { get; set; }
        public Domain.Entities.DocumentLog.Status ProcessStatus { get; set; }
        public string ProcessStatusDisplayName { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? CompleteDate { get; set; }
    }
}
