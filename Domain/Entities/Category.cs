using Domain.Common;
using Domain.Primitive;
using Domain.Primitives;

namespace Domain.Entities
{
    public sealed class Category : AggregateRoot<short>
    {
        //public short Id { get; private set; }

        private readonly List<CategoryTranslation> _categoryTranslations = new();

        private readonly List<Post> _posts = new();

        public string? Description { get; private set; }

        public IReadOnlyCollection<CategoryTranslation>? CategoryTranslations => _categoryTranslations;

        public IReadOnlyCollection<Post>? Posts => _posts;

        private Category(short id) : base(id)
        {
        }

    }
}