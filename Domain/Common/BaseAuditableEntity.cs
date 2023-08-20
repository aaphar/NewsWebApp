using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTime Created { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public long? LastModifiedBy { get; set; }
    }

}
