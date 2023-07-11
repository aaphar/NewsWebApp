using Domain.Primitive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Primitives
{
    public abstract class AggregateRoot<T> : Entity<T>
    {
        protected AggregateRoot(T id) : base(id)
        {
        }
    }
}
