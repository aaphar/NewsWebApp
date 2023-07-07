using Domain.Common;

namespace Domain.Entities
{
    public class Language : BaseEntity
    {
        public Language()
        {
            CategoryTranslations = new HashSet<CategoryTranslation>();
            PostTranslations = new HashSet<PostTranslation>();
        }

        public short Id { get; set; }

        public string? Name { get; set; }

        public string? LanguageCode { get; set; }

        public virtual ICollection<CategoryTranslation>? CategoryTranslations { get; private set; }

        public virtual ICollection<PostTranslation>? PostTranslations { get; private set; }
    }
}