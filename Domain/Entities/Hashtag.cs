using Domain.Common;

namespace Domain.Entities
{
    public sealed class Hashtag : BaseAuditableEntity
    {
        public long Id { get; set; }
        public string? Title { get; set; }

        public ICollection<PostHashtag>? PostHashtags {get; set;}
    }
}