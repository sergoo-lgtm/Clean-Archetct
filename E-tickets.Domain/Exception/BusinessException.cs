namespace eTickets.Middlewares;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}