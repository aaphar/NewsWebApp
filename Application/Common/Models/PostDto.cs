using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class PostDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public short CategoryId { get; set; }

        public DateTime? PublishDate { get; set; }

        public DateTime? InsertDate { get; set; }
    }
}
