using Domain.Common;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            PostTranslations = new HashSet<PostTranslation>();
        }

        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public int RoleId { get; set; }

        public virtual Role? Role { get; set; }

        public virtual ICollection<PostTranslation>? PostTranslations { get; private set; }
    }
}