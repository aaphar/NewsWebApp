using Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Language.Events
{
    internal sealed class UserAddedDomainEventHandler 
        : INotificationHandler<UserAddedDomainEvent>
    {
        public Task Handle(UserAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
