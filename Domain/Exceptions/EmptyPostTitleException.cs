using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class EmptyPostTitleException : DomainException
    {
        public EmptyPostTitleException() : base("Post title cannot be empty")
        {

        }
    }
}
