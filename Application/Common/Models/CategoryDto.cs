namespace Application.Common.Models
{
    public class CategoryDto
    {
        public short Id { get; set; }
        public string? Description { get; set; }

        public ICollection<PostDto> Posts { get; set; }
    }
}
