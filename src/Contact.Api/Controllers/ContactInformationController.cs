﻿using Contact.Api.Common;
using Contact.Application.ContactInformations.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact.Api.Controllers
{
    [Route("api/contact/{contactId:int}/[controller]")]
    [ApiController]
    public class ContactInformationController : MediatrControllerBase
    {
        public ContactInformationController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("")]
        public async Task<CreateContactInformationResponse> Create([FromBody] CreateContactInformationCommand request, CancellationToken cancellationToken)
           => await _mediator.Send(request, cancellationToken);
    }
}