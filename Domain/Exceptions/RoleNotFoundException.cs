using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class RoleNotFoundException : Exception
    {
        public RoleNotFoundException(int id)
        : base($"The Role with the ID = {id} was not found") { }
    }
}
