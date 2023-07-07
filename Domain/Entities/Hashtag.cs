using Domain.Common;

namespace Domain.Entities
{
    public class Hashtag : BaseEntity
    {
        public Hashtag()
        {
            PostHashtags = new List<PostHashtag>();
        }

        public int Id { get; set; }

        public string? Title { get; set; }

        public virtual ICollection<PostHashtag>? PostHashtags { get; private set; }
    }
}