using Domain.Common;
using Domain.Primitive;
using Domain.Primitives;

namespace Domain.Entities
{
    public sealed class Language
    {
        public short Id { get; private set; }

        private readonly List<CategoryTranslation> _categories = new();
        private readonly List<PostTranslation> _postTranslations = new();

        public string? Name { get; private set; }

        public string? LanguageCode { get; private set; }

        public IReadOnlyCollection<CategoryTranslation>? CategoryTranslations => _categories;

        public IReadOnlyCollection<PostTranslation>? PostTranslations => _postTranslations;

        public Language(short id, string? name, string? languageCode)
        {
            Id = id;
            Name = name;
            LanguageCode = languageCode;
        }

        public void Update(string name, string languageCode)
        {
            Name = name;
            LanguageCode = languageCode;
        }
    }
}