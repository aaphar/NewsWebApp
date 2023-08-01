using Domain.Common;

namespace Domain.Entities
{
    public sealed class Role : BaseAuditableEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public ICollection<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }
    }
}
