using Contact.Application.DocumentLog.Commands;
using MediatR;

namespace Contact.Application.Document.Publisher
{
    public class PublishContactInformationStatReport : IRequest<PublishContactInformationStatReportResponse>
    { }

    public class PublishContactInformationStatReportHandler : IRequestHandler<PublishContactInformationStatReport, PublishContactInformationStatReportResponse>
    {

        private readonly IMediator _mediator;
        private Infrastructure.RabbitMQ.Publisher _publisher;

        public PublishContactInformationStatReportHandler(IMediator mediator)
        {
            _mediator = mediator;
            _publisher = new Infrastructure.RabbitMQ.Publisher();
        }

        public async Task<PublishContactInformationStatReportResponse> Handle(PublishContactInformationStatReport request, CancellationToken cancellationToken)
        {
            var documentLogResult = await _mediator.Send(new CreateDocumentLogCommand { ProcessStatus = Domain.Entities.DocumentLog.Status.Processing });
            _publisher.PublishMessage("ContactInformationStatReportQueue", documentLogResult.DocumentLogId.ToString());
            return new PublishContactInformationStatReportResponse
            {
                DocumentLogId = documentLogResult.DocumentLogId,
            };
        }
    }
    public class PublishContactInformationStatReportResponse
    {
        public int DocumentLogId { get; set; }
    }

}
