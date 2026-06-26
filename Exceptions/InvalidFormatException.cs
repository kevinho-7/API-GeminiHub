namespace GeminiHubApi.Exceptions;

public class InvalidFormatException : Exception
{
    public InvalidFormatException() : base("invalid Format.")
    {
    }

    public InvalidFormatException(string message) : base(message)
    {
    }
}