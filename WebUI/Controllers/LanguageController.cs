using Application.Language.Commands.CreateLanguage;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LanguageController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public LanguageController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetLanguages()
        {
            return Ok(_dbContext.Languages.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetLanguage(short id)
        {
            var language = _dbContext.Languages.FirstOrDefault(l => l.Id == id);
            if (language == null)
            {
                return NotFound();
            }
            return Ok(language);
        }

    }

}
