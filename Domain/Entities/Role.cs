using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public sealed class Role : IdentityRole<int>
    {
        public ICollection<User>? Users { get; set; }

        public DateTime Created { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public long? LastModifiedBy { get; set; }
    }
}
