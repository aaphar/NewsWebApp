using Domain.Common;
using Domain.Entities;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public sealed record LanguageAddEvent(int id, string languageName, string LanguageCode) : IDomainEvent
    {
    }
}
