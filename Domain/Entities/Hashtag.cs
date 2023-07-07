using Domain.Common;
using Domain.Primitive;

namespace Domain.Entities
{
    public sealed class Hashtag : Entity<int>
    {
        //public int Id { get; private set; }

        private readonly List<PostHashtag>? _postHashtags = new();

        public string? Title { get; private set; }

        public IReadOnlyCollection<PostHashtag>? PostHashtags => _postHashtags;

        private Hashtag(int id) : base(id)
        {
        }
    }
}