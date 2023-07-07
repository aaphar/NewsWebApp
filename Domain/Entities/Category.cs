using Domain.Common;

namespace Domain.Entities
{
    public class Category:BaseEntity
    {
        public Category()
        {
            CategoryTranslations = new HashSet<CategoryTranslation>();
            Posts = new HashSet<Post>();
        }

        public short Id { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<CategoryTranslation>? CategoryTranslations { get; private set; }

        public virtual ICollection<Post>? Posts { get; private set; }
    }
}