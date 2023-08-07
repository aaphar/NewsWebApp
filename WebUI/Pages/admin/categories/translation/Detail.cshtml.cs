using Application.Common.Models;
using Application.Operations.CategoryTranslations.Commands.DeleteCategoryTranslation;
using Application.Operations.CategoryTranslations.Queries.GetCategoryTranslationById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.categories.translation
{
    public class DetailModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public CategoryTranslationDto? Translation { get; set; }

        public short CategoryId { get; set; }

        public DetailModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OngetAsync(int id)
        {
            GetCategoryTranslationByIdQuery getCategoryTranslationByIdQuery = new()
            {
                Id = id
            };

            Translation = await _mediator.Send(getCategoryTranslationByIdQuery);
        }

        public async Task<IActionResult> OnPostDeleteAsync(short Id)
        {
            await _mediator.Send(new DeleteCategoryTranslationCommand(Id));

            string _message = $"Translation with Id = {Id} was successfully deleted";

            await Console.Out.WriteLineAsync(_message);

            // redirect problem
            return RedirectToPage("/admin/categories/detail", new { Id = CategoryId });

        }
    }
}
