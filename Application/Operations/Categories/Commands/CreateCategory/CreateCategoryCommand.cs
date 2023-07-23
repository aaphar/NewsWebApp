using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Operations.Categories.Commands.CreateCategory;
public record CreateCategoryCommand : IRequest<short>
{
    public string? Description { get; set; }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, short>
{
    private readonly IApplicationDbContext _context;

    public CreateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<short> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Description = request.Description,
        };
               
        _context.Categories.Add(category);

        category.AddDomainEvent(new CategoryCreatedEvent(category));

        await _context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}

