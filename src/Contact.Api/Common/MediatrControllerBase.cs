using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contact.Api.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class MediatrControllerBase : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected MediatrControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
