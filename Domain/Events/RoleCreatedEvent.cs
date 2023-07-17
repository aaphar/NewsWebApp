using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class RoleCreatedEvent : BaseEvent
    {
        public RoleCreatedEvent(Role role)
        {
            Role = role;
        }

        public Role Role { get; }
    }
}
