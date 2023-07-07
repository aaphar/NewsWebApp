using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class PostTranslation : BaseEntity
    {
        public PostTranslation()
        {
            PostHashtags = new HashSet<PostHashtag>();
        }

        public long Id { get; set; }

        public PostTitle? Title { get; set; }

        public string? Context { get; set; }

        public DateTime InsertDate { get; set; } = DateTime.Now;

        public DateTime PublishDate { get; set; } = DateTime.Now;

        public Status Status { get; set; }

        public long ViewCount { get; set; }

        public short LanguageId { get; set; }

        public long NewsId { get; set; }

        public int AuthorId { get; set; }

        public virtual Language? Language { get; set; }

        public virtual Post? Post { get; set; }

        public virtual User? Author { get; set; }

        public virtual ICollection<PostHashtag>? PostHashtags { get; private set; }
    }
}