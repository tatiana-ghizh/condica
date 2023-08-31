using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVU.CONDICA.Server.Config
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : Controller
    {
        private IMediator mediator;

        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
