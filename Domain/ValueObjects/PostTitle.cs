using Domain.Common;
using Domain.Entities;
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
        public string Value { get; } // make Value object immutabele

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private PostTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyPostTitleException();
            }

            Value = value;
        }

        public static implicit operator string(PostTitle title)
            => title.Value;

        public static implicit operator PostTitle(string title)
            => new(title);

        public static void ValidateUniqueness(IEnumerable<PostTranslation> postTranslations, PostTitle title)
        {
            if (postTranslations.Any(pt => pt.Title == title))
            {
                throw new PostTitleAlreadyExistException();
            }
        }
    }
}
