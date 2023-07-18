using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class CategoryCreatedEvent : BaseEvent
    {
        public CategoryCreatedEvent(Category category)
        {
            Category = category;
        }

        public Category Category { get; }
    }
}
