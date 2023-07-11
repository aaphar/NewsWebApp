using Domain.Common;
using Domain.Primitive;
using Domain.Primitives;

namespace Domain.Entities
{
    // sealed prevent inherited by other classes
    public sealed class Post : AggregateRoot<long>
    {
        //public long Id { get; private set; }

        private readonly List<PostTranslation> _postTranslations = new();

        public string? ImagePath { get; private set; }

        public short CategoryId { get; private set; }

        public Category? Category { get; private set; }

        public ICollection<PostTranslation>? PostTranslations => _postTranslations;

        private Post(long id) : base(id)
        {
        }

    }
}