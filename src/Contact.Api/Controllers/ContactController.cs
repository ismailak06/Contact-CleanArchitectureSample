using Contact.Api.Common;
using Contact.Application.ContactInformations.Commands;
using Contact.Application.Contacts.Commands;
using Contact.Application.Contacts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : MediatrControllerBase
    {
        public ContactController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("")]
        public async Task<CreateContactResponse> Create([FromBody] CreateContactCommand request, CancellationToken cancellationToken)
            => await _mediator.Send(request, cancellationToken);

        [HttpDelete]
        [Route("")]
        public async Task<DeleteContactResponse> Delete([FromBody] DeleteContactCommand request, CancellationToken cancellationToken)
            => await _mediator.Send(request, cancellationToken);

        [HttpGet]
        [Route("")]
        public async Task<List<GetAllContactResponse>> GetAll(CancellationToken cancellationToken)
            => await _mediator.Send(new GetAllContactsQuery(), cancellationToken);

        [HttpGet]
        [Route("{contactId:int}")]
        public async Task<GetContactByIdResponse> GetByContactId([FromRoute] int contactId, CancellationToken cancellationToken)
            => await _mediator.Send(new GetContactByIdQuery { ContactId = contactId }, cancellationToken);
    }
}
