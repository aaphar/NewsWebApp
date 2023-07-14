using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class LanguageCreatedEvent : BaseEvent
    {
        public LanguageCreatedEvent(Language language)
        {
            Language= language;
        }

        public Language Language { get; }
    }
}
