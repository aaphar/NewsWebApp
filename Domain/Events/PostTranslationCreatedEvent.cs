using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events;
public class PostTranslationCreatedEvent : BaseEvent
{
    public PostTranslationCreatedEvent(PostTranslation postTranslation)
    {
        PostTranslation = postTranslation;
    }

    public PostTranslation PostTranslation { get; }
}

