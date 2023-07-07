using Domain.Common;

namespace Domain.Entities
{
    public class PostHashtag : BaseEntity
    {
        public long NewsId { get; set; }

        public int HashtagId { get; set; }

        public virtual PostTranslation? PostTranslation { get; set; }

        public virtual Hashtag? Hashtag { get; set; }
    }
}