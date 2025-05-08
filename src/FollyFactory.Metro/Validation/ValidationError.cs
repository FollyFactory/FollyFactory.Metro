namespace FollyFactory.Metro.Validation;

public record ValidationError(string PropertyName, List<string> ErrorMessages)
{
    public static ValidationError Create(string propertyName, string errorMessage)
    {
        return new ValidationError(propertyName, [errorMessage]);
    }
}