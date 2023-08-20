using Application.Common.Interfaces;
using Domain.Exceptions;
using MediatR;

namespace Application.Operations.Categories.Commands.UpdateCategory;
public record UpdateCategoryCommand : IRequest<Unit>
{
    public short Id { get; init; }

    public string? Description { get; init; }

    public long AuthorId { get; set; }

}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (category is null)
        {
            throw new CategoryNotFoundException(request.Id);
        }

        category.Description = request.Description;

        category.LastModified = DateTime.Now;
        category.LastModifiedBy = request.AuthorId;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
