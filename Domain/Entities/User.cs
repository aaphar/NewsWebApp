using Domain.Common;

namespace Domain.Entities
{
    public sealed class User : BaseAuditableEntity
    {
        private readonly List<PostTranslation> _translations = new();

        public string? Name { get; private set; }

        public string? Surname { get; private set; }

        public string? UserName { get; private set; }

        public string? Email { get; private set; }

        public string? Password { get; private set; }

        public int RoleId { get; private set; }

        public Role? Role { get; private set; }

        public IReadOnlyCollection<PostTranslation>? PostTranslations => _translations;
    }
}