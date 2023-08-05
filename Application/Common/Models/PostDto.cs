namespace Application.Common.Models
{
    public class PostDto
    {
        public long Id { get; set; }

        public string? Title { get; set; }

        public string? ImagePath { get; set; }

        public DateTime? PublishDate { get; set; }

        public DateTime? InsertDate { get; set; }

        public short CategoryId { get; set; }
    }
}
