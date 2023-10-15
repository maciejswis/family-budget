namespace FamilyBudget.Core.Exceptions;

public class FamilyBudgetException : Exception
{
    public FamilyBudgetException(
        string? message = null,
        Exception? innerEx = null) : base(message, innerEx)
    { }
}

public class HttpResponseException : FamilyBudgetException
{
    public HttpResponseException(int statusCode, object? value = null) =>
        (StatusCode, Value) = (statusCode, value);

    public int StatusCode { get; }

    public object? Value { get; }
}

public class NotUniqueException : FamilyBudgetException
{
    public NotUniqueException(string message) : base(message)
    { }
}

public class DataAccessException : FamilyBudgetException
{
    public DataAccessException(string message, Exception innerEx)
        : base(message, innerEx)
    { }
}
