namespace Domain.Exceptions
{
    public class CategoryTranslationNotFoundException : Exception
    {
        public CategoryTranslationNotFoundException(int id)
            : base($"The Category Translation with the ID = {id} was not found")
        {

        }
    }
}
