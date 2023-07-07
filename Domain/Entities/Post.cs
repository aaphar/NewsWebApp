using Domain.Common;

namespace Domain.Entities
{
    public class Post : BaseEntity
    {
        public Post()
        {
            PostTranslations = new HashSet<PostTranslation>();
        }

        public long Id { get; set; }

        public string? ImagePath { get; set; }

        public short CategoryId { get; set; }

        public virtual Category? Category { get; set; }

        public virtual ICollection<PostTranslation>? PostTranslations { get; private set; }
    }
}