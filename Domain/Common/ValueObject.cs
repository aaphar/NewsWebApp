using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public abstract IEnumerable<object> Values { get; }

        public override bool Equals(object? obj)
        {
            return obj is ValueObject other && ValuesAreEqual(other);
        }

        public bool Equals(ValueObject? other)
        {
            return other is not null && ValuesAreEqual(other);
        }

        public override int GetHashCode()
        {
            return Values
                .Aggregate(
                default(int),
                HashCode.Combine);
        }

        private bool ValuesAreEqual(ValueObject other)
        {
            return Values.SequenceEqual(other.Values);
        }
    }
}
