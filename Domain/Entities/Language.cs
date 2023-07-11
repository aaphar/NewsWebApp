using Domain.Common;
using Domain.Primitive;
using Domain.Primitives;

namespace Domain.Entities
{
    public sealed class Language : AggregateRoot<short>
    {
        //public short Id { get; private set; }

        private readonly List<CategoryTranslation> _categories = new();
        private readonly List<PostTranslation> _postTranslations = new();

        public string? Name { get; private set; }

        public string? LanguageCode { get; private set; }

        public IReadOnlyCollection<CategoryTranslation>? CategoryTranslations => _categories;

        public IReadOnlyCollection<PostTranslation>? PostTranslations => _postTranslations;

        private Language(short id) : base(id)
        {
        }
    }
}