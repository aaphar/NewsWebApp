using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;
public class ApplicationRole : IdentityRole<int>
{
    public virtual ICollection<Permission> Permissions { get; set; }

    public ICollection<ApplicationUser> Users { get; set; }


    public ApplicationRole()
    {
        Permissions = new HashSet<Permission>();
        Users = new HashSet<ApplicationUser>();
    }

    public bool IsPermissionInRole(string _permission)
    {
        return Permissions.Any(p => p.Name == _permission);
    }
}

