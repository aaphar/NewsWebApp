using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public sealed class Role : IdentityRole<int>
    {
        public ICollection<User>? Users { get; set; }
    }
}
