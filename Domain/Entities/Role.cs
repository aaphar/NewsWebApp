using Domain.Common;

namespace Domain.Entities
{
    public sealed class Role : BaseAuditableEntity
    {
        private readonly List<User> _users = new();

        public string? Title { get; private set; }

        public IReadOnlyCollection<User>? Users => _users;

    }
}
