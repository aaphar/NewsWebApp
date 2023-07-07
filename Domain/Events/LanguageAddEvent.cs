using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class LanguageAddEvent:BaseEvent
    {
        public LanguageAddEvent(Language language)
        {
            Language=language;            
        }

        public Language Language { get; }
    }
}
