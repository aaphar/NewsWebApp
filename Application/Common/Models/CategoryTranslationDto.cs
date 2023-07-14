using Domain.Enums;

namespace Application.Common.Models
{
    public class CategoryTranslationDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public Status Status { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime PublishDate { get; set; }

        public int LanguageId { get; set; }

        public int CategoryId { get; set; }
    }
}
