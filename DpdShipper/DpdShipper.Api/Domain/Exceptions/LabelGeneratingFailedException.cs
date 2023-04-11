namespace DpdShipper.Api.Domain.Exceptions;

public class LabelGeneratingFailedException : Exception
{
    public LabelGeneratingFailedException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}