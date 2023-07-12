using Application.Language.Commands.CreateLanguage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{

    [Authorize]
    public class LanguageController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public LanguageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("/admin/Language/")]
        public async Task<ActionResult<int>> Create(CreateLanguageCommand command)
        {
            int languageId = await _mediator.Send(command);

            return languageId;
        }
    }
}
