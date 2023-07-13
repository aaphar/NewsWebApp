using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class PostTranslationDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Context { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime PublishDate { get; set; }

        public Status Status { get; set; }

        public long ViewCount { get; set; }

        public short LanguageId { get; set; }

        public long NewsId { get; set; }

        public int AuthorId { get; set; }
    }
}
