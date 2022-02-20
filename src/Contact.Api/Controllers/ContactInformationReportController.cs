using Contact.Api.Common;
using Contact.Application.Document.Publisher;
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
    }
}
