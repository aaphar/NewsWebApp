using Application.Common.Models;
using Application.Operations.Categories.Commands.CreateCategory;
using Application.Operations.Categories.Queries.GetCategories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.categories
{
    public class CategoryModel : PageModel
    {
        private readonly IMediator _mediator;

        private readonly IValidator<CreateCategoryCommand> _validator;


        public List<CategoryDto>? Categories { get; set; }

        public CategoryDto? Category { get; set; }

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

    }
}
