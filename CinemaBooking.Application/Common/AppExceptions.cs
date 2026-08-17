namespace CinemaBooking.Application.Common;

public class NotFoundException : Exception
{
    public NotFoundException(string entityName, object key)
        : base($"{entityName} with id '{key}' was not found.")
    {
    }
}

public class ForbiddenException : Exception
{
    public ForbiddenException(string message = "You are not allowed to perform this action.")
        : base(message)
    {
    }
}
