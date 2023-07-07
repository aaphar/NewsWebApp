using Domain.Common;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class PostTitle : ValueObject
    {
        public string Value { get; }

        public override IEnumerable<object> Values { yield return Value; }

        public PostTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyPostTitleException();
            }

            Value = value;
        }

        public static implicit operator string(PostTitle title)
        {
            return title.ToString();
        }

        public static implicit operator PostTitle(string title)
            => new(title);

    }
}
