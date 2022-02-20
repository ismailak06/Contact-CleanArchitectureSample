using ClosedXML.Excel;
using Contact.Application.Common.Interfaces;
using Contact.Application.DocumentLog.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contact.Application.Document.Command
{
    public class CreateContactInformationStatReportCommand : IRequest<CreateContactInformationStatReportResponse>
    {
        public int DocumentLogId { get; set; }
    }

    public class CreateContactInformationStatReportHandler : IRequestHandler<CreateContactInformationStatReportCommand, CreateContactInformationStatReportResponse>
    {
        IMediator _mediator;
        IContactDbContext _context;
        public CreateContactInformationStatReportHandler(IMediator mediator, IContactDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<CreateContactInformationStatReportResponse> Handle(CreateContactInformationStatReportCommand request, CancellationToken cancellationToken)
        {
            var contactInfoTypeStats = await _context.ContactInformations.GroupBy(m => m.InformationType).Select(m => new ContactInformationTypeStat
            {
                TypeName = m.Key.ToString(),
                Count = m.Count()
            }).ToListAsync();

            saveReportFile(contactInfoTypeStats);
            var result = await setDocumentLogStatusAsCompleted(request.DocumentLogId);

            return new CreateContactInformationStatReportResponse
            {
                DocumentLogId = result.DocumentLogId
            };
        }
        private async Task<UpdateDocumentLogStatusResponse> setDocumentLogStatusAsCompleted(int documentLogId)
        {
            var updateDocumentStatusInput = new UpdateDocumentLogStatusCommand
            {
                DocumentLogId = documentLogId,
                ProcessStatus = Domain.Entities.DocumentLog.Status.Completed
            };
            var result = await _mediator.Send(updateDocumentStatusInput);
            return result;
        }

        private void saveReportFile(List<ContactInformationTypeStat> contactInfoStats)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("İletişim Bilgisi İstatistik");

            int row = 1;
            foreach (var stat in contactInfoStats)
            {
                worksheet.Cell(row, 1).Value = stat.TypeName;
                worksheet.Cell(row, 2).Value = stat.Count;
                row++;
            }
            using (MemoryStream memoryStream = new())
            {
                workbook.SaveAs(Path.Combine(Directory.GetCurrentDirectory(), "ReportFiles", Guid.NewGuid().ToString()+".xlsx"));
            }
        }
        private class ContactInformationTypeStat
        {
            public string TypeName { get; set; }
            public int Count { get; set; }
        }
    }

    public class CreateContactInformationStatReportResponse
    {
        public int DocumentLogId { get; set; }
    }
}
