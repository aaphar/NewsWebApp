using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Language
{
    public class LanguageController : Controller
    {
        private readonly ApplicationDbContext _context; // Replace with your DbContext

        public LanguageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetLanguages()
        {
            var languages = _context.Languages.ToList(); // Adjust this based on your database structure
            return PartialView("_NavigationBar", languages);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
