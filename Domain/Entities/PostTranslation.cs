using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public sealed class PostTranslation : BaseAuditableEntity
    {
        public long Id { get; set; }
        public string? Title { get; set; }

        public string? Context { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime PublishDate { get; set; }

        public Status Status { get; set; }

        public long ViewCount { get; set; }

        public short LanguageId { get; set; }

        public long NewsId { get; set; }

        public int AuthorId { get; set; }

        public Language? Language { get; set; }

        public Post? Post { get; set; }

        public User? Author { get; set; }

        public IReadOnlyCollection<PostHashtag>? PostHashtags { get; set; }
    }
}