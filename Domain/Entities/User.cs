using Domain.Common;
using Domain.Primitive;

namespace Domain.Entities
{
    public sealed class User : Entity<int>
    {
        //public int Id { get; private set; }

        private readonly List<PostTranslation> _translations = new();

        public string? UserName { get; private set; }

        public string? Email { get; private set; }

        public string? Password { get; private set; }

        public int RoleId { get; private set; }

        public Role? Role { get; private set; }

        public IReadOnlyCollection<PostTranslation>? PostTranslations => _translations;

        private User(int id) : base(id)
        {
        }

    }
}