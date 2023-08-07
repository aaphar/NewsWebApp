using Domain.Common;

namespace Domain.Entities
{
    // sealed prevent inherited by other classes
    public sealed class Post : BaseAuditableEntity
    {
        public long Id { get; set; }

        public string? Title { get; set; }

        public string? ImagePath { get; set; }

        public DateTime? PublishDate { get; set; }

        public DateTime? InsertDate { get; set; }

        public short? CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<PostTranslation>? PostTranslations { get; set; }
    }

}
