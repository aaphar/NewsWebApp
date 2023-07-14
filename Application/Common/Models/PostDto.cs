namespace Application.Common.Models
{
    public class PostDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public DateTime? PublishDate { get; set; }

        public DateTime? InsertDate { get; set; }

        public int CategoryId { get; set; }
    }
}
