using Domain.Events;
using MediatR;

namespace Application.Language.Events;
internal sealed class LanguageAddedDomainEventHandler : INotificationHandler<LanguageAddEvent>
{
    public Task Handle(LanguageAddEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

