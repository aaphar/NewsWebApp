using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public sealed class CategoryTranslation : BaseAuditableEntity
    {
        public string? Title { get; private set; }

        public Status Status { get; private set; }

        public DateTime InsertDate { get; private set; }

        public DateTime PublishDate { get; private set; }

        public short LanguageId { get; private set; }

        public short CategoryId { get; private set; }

        public Language? Language { get; private set; }

        public Category? Category { get; private set; }
    }
}