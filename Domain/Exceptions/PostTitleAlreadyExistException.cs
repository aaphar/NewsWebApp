using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class PostTitleAlreadyExistException : DomainException
    {
        public PostTitleAlreadyExistException()
        {
        }

        public PostTitleAlreadyExistException(PostTitle title) : base($"Post with the title {title} already exist.")
        {
        }
    }
}
