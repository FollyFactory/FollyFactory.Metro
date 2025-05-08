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
    
    
    /// <summary>
    /// Creates a valid validation result with no errors.
    /// </summary>
    public static BasicValidationResult Valid()
    {
        return new BasicValidationResult { IsValid = true };
    }
    
    /// <summary>
    /// Creates an invalid validation result with the specified error.
    /// </summary>
    public static BasicValidationResult Invalid(string propertyName, string errorMessage)
    {
        var result = new BasicValidationResult { IsValid = false };
        result.AddError(propertyName, errorMessage);
        return result;
    }
    
    /// <summary>
    /// Creates an invalid validation result with multiple errors for the same property.
    /// </summary>
    public static BasicValidationResult Invalid(string propertyName, IEnumerable<string> errorMessages)
    {
        var result = new BasicValidationResult { IsValid = false };
        var error = new ValidationError(propertyName, errorMessages.ToList());
        result.Errors.Add(error);
        return result;
    }
    
    /// <summary>
    /// Creates an invalid validation result with multiple validation errors.
    /// </summary>
    public static BasicValidationResult Invalid(IEnumerable<ValidationError> errors)
    {
        return new BasicValidationResult
        {
            IsValid = false,
            Errors = errors.ToList()
        };
    }
    
    /// <summary>
    /// Combines multiple validation results into a single result.
    /// The combined result is valid only if all input results are valid.
    /// </summary>
    public static BasicValidationResult Combine(params IValidationResult[] results)
    {
        var combinedResult = new BasicValidationResult
        {
            IsValid = results.All(r => r.IsValid)
        };
        
        foreach (var result in results)
        {
            foreach (var error in result.Errors)
            {
                combinedResult.Errors.Add(error);
            }
        }
        
        return combinedResult;
    }
}
