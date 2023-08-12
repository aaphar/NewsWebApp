namespace Domain.Exceptions;
public class UserNotFoundException : Exception
{
    public UserNotFoundException(int id)
            : base($"The User with  id {id} was not found")
    {
    }

    public UserNotFoundException(string email)
            : base($"The User with  email {email} was not found")
    {
    }
}


