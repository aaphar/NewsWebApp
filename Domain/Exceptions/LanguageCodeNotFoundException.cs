using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class LanguageCodeNotFoundException : Exception
    {
        public LanguageCodeNotFoundException(string Code)
            : base($"The Language with the Code = {Code} was not found")
        {
        }
    }
}
