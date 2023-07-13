using Domain.Common;

namespace Domain.Entities
{
    public sealed class Category : BaseAuditableEntity
    {  
        public string? Description { get; private set; }

        private readonly List<CategoryTranslation> _categoryTranslations = new();

        private readonly List<Post> _posts = new();

        public IReadOnlyCollection<CategoryTranslation>? CategoryTranslations => _categoryTranslations;

        public IReadOnlyCollection<Post>? Posts => _posts;
    }
}