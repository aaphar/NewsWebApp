using Domain.Common;
using Domain.Enums;
using Domain.Primitive;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public sealed class PostTranslation : BaseAuditableEntity
    {
        private readonly List<PostHashtag> _postHashtags = new();

        public string? Title { get; private set; }

        public string? Context { get; private set; }

        public DateTime InsertDate { get; private set; }

        public DateTime PublishDate { get; private set; }

        public Status Status { get; private set; }

        public long ViewCount { get; private set; }

        public short LanguageId { get; private set; }

        public long NewsId { get; private set; }

        public int AuthorId { get; private set; }

        public Language? Language { get; private set; }

        public Post? Post { get; private set; }

        public User? Author { get; private set; }

        public IReadOnlyCollection<PostHashtag>? PostHashtags => _postHashtags;
    }
}