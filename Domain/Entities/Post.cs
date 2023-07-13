using Domain.Common;

namespace Domain.Entities
{
    // sealed prevent inherited by other classes
    public sealed class Post : BaseAuditableEntity
    {
        private readonly List<PostTranslation> _postTranslations = new();

        public string? Title { get; private set; }

        public short CategoryId { get; private set; }

        public Category? Category { get; private set; }

        public DateTime? PublishDate { get; private set; }

        public DateTime? InsertDate { get; private set; }

        public ICollection<PostTranslation>? PostTranslations => _postTranslations;

    }
}