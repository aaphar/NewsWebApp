﻿using Domain.Common;

namespace Domain.Entities
{
    public sealed class Role : BaseAuditableEntity
    {
        public short Id { get; set; }
        public string? Title { get; set; }

        public ICollection<User>? Users { get; set; }

    }
}
