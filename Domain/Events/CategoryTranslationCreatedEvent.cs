using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class CategoryTranslationCreatedEvent : BaseEvent
    {
        public CategoryTranslation CategoryTranslation { get; }
        public CategoryTranslationCreatedEvent(CategoryTranslation categoryTranslation)
        {
            CategoryTranslation = categoryTranslation;
        }
    }
}
