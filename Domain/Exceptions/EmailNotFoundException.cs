namespace Domain.Exceptions;
public class EmailNotFoundException:Exception
{
    public EmailNotFoundException(string email)
            : base($"The Email {email} was not found")
    {
    }
}

