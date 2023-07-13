using Domain.Common;

namespace Domain.Entities
{
    public sealed class Hashtag : BaseAuditableEntity
    {
        private readonly List<PostHashtag>? _postHashtags = new();

        public string? Title { get; private set; }

        public IReadOnlyCollection<PostHashtag>? PostHashtags => _postHashtags;
    }
}