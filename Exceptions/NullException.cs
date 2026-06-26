namespace GeminiHubApi.Exceptions;

public class NullException : Exception
{
    public NullException() : base("This file is Null")
    {
    }

    public NullException(string message) : base(message)
    {
    }
}