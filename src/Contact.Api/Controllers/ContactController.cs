﻿using Contact.Api.Common;
using Contact.Application.Contacts.Commands;
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
        public async Task<DeleteContactResponse> Delete([FromBody] DeleteContactInformationCommand request, CancellationToken cancellationToken)
        => await _mediator.Send(request, cancellationToken);
    }
}
