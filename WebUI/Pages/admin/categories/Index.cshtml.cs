using Application.Common.Models;
using Application.Operations.Categories.Commands.CreateCategory;
using Application.Operations.Categories.Commands.DeleteCategory;
using Application.Operations.Categories.Queries.GetCategories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.categories
{
    public class CategoryModel : PageModel
    {
        private readonly IMediator _mediator;

        private readonly IValidator<CreateCategoryCommand> _validator;


        public List<CategoryDto>? Categories { get; set; }

        public CategoryDto? Category { get; set; }


        public List<CategoryTranslationDto>? CategoryTranslations { get; set; }

        public CategoryTranslationDto? CategoryTranslation { get; set; }

        public CategoryModel(
            IMediator mediator,
            IValidator<CreateCategoryCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        public async Task OnGetAsync()
        {
            Categories = await _mediator.Send(new GetCategoriesQuery());
        }

        public async Task<IActionResult> OnPostDeleteAsync(short Id)
        {
            await _mediator.Send(new DeleteCategoryCommand(Id));

            string _message = $"Category with Id = {Id} was successfully deleted";

            await Console.Out.WriteLineAsync(_message);

            return RedirectToPage("/admin/categories/index");
        }
    }
}
