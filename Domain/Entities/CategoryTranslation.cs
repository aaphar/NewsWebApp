using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public sealed class CategoryTranslation : BaseAuditableEntity
    {
        public string? Title { get; set; }

        public Status Status { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime PublishDate { get; set; }

        public int LanguageId { get; set; }

        public int CategoryId { get; set; }

        public Language? Language { get; set; }

        public Category? Category { get; set; }
    }
}