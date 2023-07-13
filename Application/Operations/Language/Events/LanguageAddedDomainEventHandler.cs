using Domain.Events;
using MediatR;

namespace Application.CommandQueries.Language.Events;
internal sealed class LanguageAddedDomainEventHandler : INotificationHandler<LanguageCreatedEvent>
{
    public Task Handle(LanguageCreatedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

