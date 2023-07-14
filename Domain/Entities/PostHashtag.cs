using Domain.Common;
using Domain.Primitive;

namespace Domain.Entities
{
    public sealed class PostHashtag : BaseAuditableEntity
    {
        public int NewsId { get; set; }

        public int HashtagId { get; set; }

        public PostTranslation? PostTranslation { get; set; }

        public Hashtag? Hashtag { get; set; }
    }
}