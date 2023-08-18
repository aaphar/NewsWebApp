namespace Domain.Exceptions;

public sealed class HashtagNotFoundException:Exception
{
    public HashtagNotFoundException(long id)
            : base($"The Hashtah with the ID = {id} was not found")
    {
    }
}
