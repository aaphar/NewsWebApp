namespace Domain.Exceptions;

public class CategoryNotFoundWithGivenLanguageException:Exception
{
    public CategoryNotFoundWithGivenLanguageException(short categoryId, string languageCode)
      : base($"The {languageCode} translation of given Category with the ID = {categoryId} was not found")
    {
    }
}
