using Domain.Common;
using Domain.Enums;
using Domain.Primitive;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public sealed class PostTranslation : BaseAuditableEntity
    {
        public string? Title { get; set; }

        public string? Context { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime PublishDate { get; set; }

        public Status Status { get; set; }

        public long ViewCount { get; set; }

        public int LanguageId { get; set; }

        public int NewsId { get; set; }

        public int AuthorId { get; set; }

        public Language? Language { get; set; }

        public Post? Post { get; set; }

        public User? Author { get; set; }

        public IReadOnlyCollection<PostHashtag>? PostHashtags { get; set; }
    }
}