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
            var contactCountListByCity = await _context.ContactInformations
                .Where(m => m.InformationType == Domain.Entities.ContactInformation.Type.Location)
                .GroupBy(m => new
                {
                    m.Content,
                    m.Contact.Id
                }).Select(m => new
                {
                    ContentName = m.Key.Content,
                }).ToListAsync();

            var contactInfoLocationStats = contactCountListByCity
                .GroupBy(m => m.ContentName)
                .Select(m => new ContactInformationTypeStat
                {
                    ContentName = m.Key,
                    Count = m.Count(),
                    PhoneNumberCount =
                    _context.Contacts
                    .Where(y =>
                        y.ContactInformations.Any(x => x.InformationType == Domain.Entities.ContactInformation.Type.Location && x.Content == m.Key))
                    .Include(x => x.ContactInformations)
                    .Select(x => x.PhoneNumberInformations).ToList().SelectMany(x => x).ToList().Count()
                }).ToList();



            saveReportFile(contactInfoLocationStats);
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

            worksheet.Cell(1, 1).Value = "Şehir Adı";
            worksheet.Cell(1, 2).Value = "Şehirdeki Kişi Sayısı";
            worksheet.Cell(1, 3).Value = "Telefon Numarası Sayısı";

            int row = 2;
            foreach (var stat in contactInfoStats)
            {
                worksheet.Cell(row, 1).Value = stat.ContentName;
                worksheet.Cell(row, 2).Value = stat.Count;
                worksheet.Cell(row, 3).Value = stat.PhoneNumberCount;

                row++;
            }
            using (MemoryStream memoryStream = new())
            {
                workbook.SaveAs(Path.Combine(Directory.GetCurrentDirectory(), "ReportFiles", Guid.NewGuid().ToString() + ".xlsx"));
            }
        }
        private class ContactInformationTypeStat
        {
            public string ContentName { get; set; }
            public int Count { get; set; }
            public int PhoneNumberCount { get; set; }

        }
    }

    public class CreateContactInformationStatReportResponse
    {
        public int DocumentLogId { get; set; }
    }
}
