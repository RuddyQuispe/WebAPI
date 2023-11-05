using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public abstract class BaseController:ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= this.HttpContext.RequestServices.GetService<IMediator>();
    }
}
