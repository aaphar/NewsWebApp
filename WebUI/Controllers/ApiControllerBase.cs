using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[ApiController]
[Route("admin/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected IMediator Mediator { get; }

    public ApiControllerBase(IMediator mediator)
    {
        Mediator = mediator;
    }
}
