namespace Domain.Exceptions;
public class PasswordNotMatchException : Exception
{
    public PasswordNotMatchException(string password)
            : base($"The Password \"{password}\" is wrong")
    {
    }
}

