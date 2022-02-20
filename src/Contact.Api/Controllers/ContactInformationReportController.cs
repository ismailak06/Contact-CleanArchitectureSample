using Contact.Api.Common;
using Contact.Application.Document.Publisher;
using Contact.Application.DocumentLog.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInformationReportController : MediatrControllerBase
    {
        public ContactInformationReportController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateStatReport(CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new PublishContactInformationStatReport(), cancellationToken));

        [HttpGet]
        [Route("{documentLogId:int}")]
        public async Task<GetDocumentLogByIdQueryResponse> GetById([FromRoute] int documentLogId, CancellationToken cancellationToken)
        => await _mediator.Send(new GetDocumentLogByIdQuery { DocumentLogId = documentLogId }, cancellationToken);
        [HttpGet]
        public async Task<List<GetDocumentLogsQueryResponse>> GetAll(CancellationToken cancellationToken)
        => await _mediator.Send(new GetDocumentLogsQuery(), cancellationToken);
    }
}
