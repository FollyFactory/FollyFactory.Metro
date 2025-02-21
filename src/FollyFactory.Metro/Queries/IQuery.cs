using FollyFactory.Metro.Validation;
using System.ComponentModel.DataAnnotations;

namespace FollyFactory.Metro.Queries;

public interface IQuery<TResult>
{
}

public class QueryValidationException : ApplicationValidationException
{
    public QueryValidationException(string message) : base(message)
    {
    }

    public QueryValidationException(IValidationResult results) : base(results)
    {
    }
}