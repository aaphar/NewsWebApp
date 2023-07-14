using Domain.Common;

namespace Domain.Entities
{
    public sealed class Category : BaseAuditableEntity
    {  
        public string? Description { get; set; }

        public ICollection<CategoryTranslation>? CategoryTranslations { get; set; }
        
        public ICollection<Post>? Posts { get; set; }
    }
}