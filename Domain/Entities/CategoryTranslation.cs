using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public sealed class CategoryTranslation : BaseAuditableEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public Status Status { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime PublishDate { get; set; }

        public short LanguageId { get; set; }

        public short CategoryId { get; set; }

        public Language? Language { get; set; }

        public Category? Category { get; set; }
    }
}