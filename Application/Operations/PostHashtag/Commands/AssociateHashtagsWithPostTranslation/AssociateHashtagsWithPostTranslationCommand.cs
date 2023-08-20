using Application.Common.Interfaces;
using Application.Operations.Hashtags.Commands.CreateHashtag;
using Application.Operations.PostHashtag.Commands.RemovePostHashtags;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.PostHashtag.Commands.AssociateHashtagsWithPostTranslation;

public record AssociateHashtagsWithPostTranslationCommand:IRequest<Unit>
{
    public long PostTranslationId { get; set; }
    public string PostHashtags { get; set; }
}
public class AssociateHashtagsWithPostTranslationCommandHandler : IRequestHandler<AssociateHashtagsWithPostTranslationCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;

    public AssociateHashtagsWithPostTranslationCommandHandler(IApplicationDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(AssociateHashtagsWithPostTranslationCommand request, CancellationToken cancellationToken)
    {
        // Remove existing associations between hashtags and translation
        await _mediator.Send(new RemovePostHashtagsCommand { PostTranslationId = request.PostTranslationId });

        var hashtags = request.PostHashtags.Split(' ')
            .Select(tag => tag.Trim())
            .Where(tag => !string.IsNullOrEmpty(tag)).ToList();

        if(hashtags!=null && hashtags.Any())
        {
            foreach (var tag in hashtags)
            {
                string hashtagTitle = tag.StartsWith("#") 
                    ? tag.Substring(1) : tag;

                var existingHashtag = await _context.Hashtags
                    .FirstOrDefaultAsync(h => h.Title == hashtagTitle);

                long hashtagId = 0;

                if (existingHashtag == null)
                {
                    hashtagId = await _mediator.Send(new CreateHashtagCommand { Title = hashtagTitle });
                }
                else
                {
                    hashtagId = existingHashtag.Id;
                }

                var postHashtag = new Domain.Entities.PostHashtag { NewsId = request.PostTranslationId, HashtagId = hashtagId };
                _context.PostHashtags.Add(postHashtag);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
        return Unit.Value;
    }
}