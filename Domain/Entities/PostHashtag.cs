using Domain.Common;
using Domain.Primitive;
using Domain.Primitives;

namespace Domain.Entities
{
    public sealed class PostHashtag : AggregateRoot<long>
    {
        public long NewsId { get; private set; }

        public int HashtagId { get; private set; }

        public PostTranslation? PostTranslation { get; private set; }

        public Hashtag? Hashtag { get; private set; }

        private PostHashtag(long id) : base(id)
        {
        }
    }
}