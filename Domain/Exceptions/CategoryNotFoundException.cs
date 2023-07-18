using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(short id)
            : base($"The Category with the ID = {id} was not found")
        {
        }
    }
}
