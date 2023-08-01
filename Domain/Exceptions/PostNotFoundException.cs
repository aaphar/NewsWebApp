namespace Domain.Exceptions;
public sealed class PostNotFoundException : Exception
{
    public PostNotFoundException(long id)
           : base($"The Post with the ID = {id} was not found")
    {
    }
}

