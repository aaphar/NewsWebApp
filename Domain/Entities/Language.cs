using Domain.Common;

namespace Domain.Entities
{
    public sealed class Language : BaseAuditableEntity
    {        
        public string? Title { get; set; }

        public string? LanguageCode { get; set; }

        public ICollection<CategoryTranslation>? CategoryTranslations { get; set; }

        public ICollection<PostTranslation>? PostTranslations { get; set; }
    }
}