namespace DpdShipper.Api.Domain.Exceptions;

public class LoginFailedException : Exception
{
    public LoginFailedException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}