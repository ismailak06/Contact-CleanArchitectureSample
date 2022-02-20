using Contact.Application.Common.Exceptions;
using Contact.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contact.Application.DocumentLog.Commands
{
    public class UpdateDocumentLogStatusCommand : IRequest<UpdateDocumentLogStatusResponse>
    {
        public int DocumentLogId { get; set; }
        public Domain.Entities.DocumentLog.Status ProcessStatus { get; set; }
    }

    public class UpdateDocumentLogStatusCommandHandler : IRequestHandler<UpdateDocumentLogStatusCommand, UpdateDocumentLogStatusResponse>
    {
        IContactDbContext _context;

        public UpdateDocumentLogStatusCommandHandler(IContactDbContext context)
        {
            _context = context;
        }

        public async Task<UpdateDocumentLogStatusResponse> Handle(UpdateDocumentLogStatusCommand request, CancellationToken cancellationToken)
        {
            var documentLog = await _context.DocumnetLogs.FirstOrDefaultAsync(x => x.Id == request.DocumentLogId);
            if (documentLog is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.DocumentLog), request.DocumentLogId);
            }

            documentLog.SetStatus(request.ProcessStatus);
            await _context.SaveChangesAsync(cancellationToken);
            return new UpdateDocumentLogStatusResponse
            {
                DocumentLogId = documentLog.Id
            };
        }
    }
    public class UpdateDocumentLogStatusResponse
    {
        public int DocumentLogId { get; set; }
    }
}
