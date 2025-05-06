namespace FollyFactory.Metro.Validation;

public class BasicValidationResult : IValidationResult
{
    public List<ValidationError> Errors { get; set; } = [];
    public bool IsValid { get; set; }
    
    public void AddError(string propertyName, string errorMessage)
    {
        var error = Errors.FirstOrDefault(e => e.PropertyName == propertyName);
        if (error != null)
        {
            error.ErrorMessages.Add(errorMessage);
        }
        else
        {
            Errors.Add(new ValidationError(propertyName, [errorMessage]));
        }
    }
}