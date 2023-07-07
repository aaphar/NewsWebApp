using Domain.Common;
using Domain.Primitive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Role : Entity<int>
    {
        //public int Id { get; private set; }

        private readonly List<User> _users=new();

        public string? Title { get; private set; }

        public IReadOnlyCollection<User>? Users => _users;

        private Role(int id) : base(id)
        {
        }

    }
}
