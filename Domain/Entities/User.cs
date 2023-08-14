using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public sealed class User : IdentityUser<int>
    {
        public override int Id { get; set; }
        
        public string? Name { get; set; }
                
        public string? Surname { get; set; }
        
        public string Password { get; set; }

        public string? ImagePath { get; set; }

        public int? RoleId { get; set; }
        
        public Role? Role { get; set; }

        public ICollection<PostTranslation>? PostTranslations { get; set; }
    }
}