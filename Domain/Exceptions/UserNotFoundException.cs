namespace Domain.Exceptions;
public class UserNotFoundException : Exception
{
    public UserNotFoundException(string email)
            : base($"The User with {email} was not found")
    {
    }
}


