using Domain.Common;
using Domain.Primitive;

namespace Domain.Entities
{
    public sealed class PostHashtag : BaseAuditableEntity
    {
        public long NewsId { get; set; }

        public long HashtagId { get; set; }

        public PostTranslation? PostTranslation { get; set; }

        public Hashtag? Hashtag { get; set; }
    }
}