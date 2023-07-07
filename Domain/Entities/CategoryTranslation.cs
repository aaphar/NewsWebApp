using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class CategoryTranslation : BaseEntity
    {
        public short Id { get; set; }

        public string? Title { get; set; }

        public Status Status { get; set; }

        public DateTime InsertDate { get; set; } = DateTime.Now;

        public DateTime PublishDate { get; set; } = DateTime.Now;

        public short LanguageId { get; set; }

        public short CategoryId { get; set; }

        public virtual Language? Language { get; set; }

        public virtual Category? Category { get; set; }
    }
}