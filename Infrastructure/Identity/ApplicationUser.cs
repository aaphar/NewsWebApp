using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Infrastructure.Identity;
public class ApplicationUser : IdentityUser<int>
{
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public int RoleId { get; set; }

    public ApplicationRole Role { get; set; }

    public ICollection<PostTranslation> PostTranslations { get; set; }

    public ApplicationUser()
    {
        PostTranslations = new List<PostTranslation>();
        Role = new ApplicationRole();
    }

    public bool HasRole(string _role)
    {
        return Role.Name == _role;
    }

    public bool HasPermission(string _permission)
    {
        return Role.IsPermissionInRole(_permission);
    }
}