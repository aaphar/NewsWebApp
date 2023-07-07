using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EmptyPostTitleException : Exception
    {
        public EmptyPostTitleException() : base("Post title cannot be empty")
        {

        }
    }
}
