using Contact.Application.Common.Interfaces;
using MediatR;


namespace Contact.Application.DocumentLog.Commands
{
    public class CreateDocumentLogCommand : IRequest<CreateDocumentLogResponse>
    {
        public Domain.Entities.DocumentLog.Status ProcessStatus { get; set; }
    }

    public class CreateDocumentLogCommandHandler : IRequestHandler<CreateDocumentLogCommand, CreateDocumentLogResponse>
    {
        IContactDbContext _context;

        public CreateDocumentLogCommandHandler(IContactDbContext context)
        {
            _context = context;
        }

        public async Task<CreateDocumentLogResponse> Handle(CreateDocumentLogCommand request, CancellationToken cancellationToken)
        {
            var documentLog = new Domain.Entities.DocumentLog(request.ProcessStatus);

            await _context.DocumnetLogs.AddAsync(documentLog, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateDocumentLogResponse
            {
                DocumentLogId = documentLog.Id
            };
        }
    }

    public class CreateDocumentLogResponse
    {
        public int DocumentLogId { get; set; }
    }
}
