using Domain.Enums;

namespace Application.Common.Models
{
    public class CategoryTranslationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Status? Status { get; set; }

        public DateTime? InsertDate { get; set; }
                
        public DateTime? PublishDate { get; set; }

        public short? LanguageId { get; set; }

        public short? CategoryId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
