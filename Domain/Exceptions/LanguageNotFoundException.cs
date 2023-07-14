using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class LanguageNotFoundException : Exception
    {
        public LanguageNotFoundException(int id)
            : base($"The Language with the ID = {id} was not found")
        {
        }
    }
}
