namespace Domain.Exceptions
{
    public class PostTranslationNotFoundException:Exception
    {
        public PostTranslationNotFoundException(long id)
           : base($"The Post Translation with the ID = {id} was not found")
        {

        }
    }
}
