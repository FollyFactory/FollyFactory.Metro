namespace FollyFactory.Metro.Validation;
public interface IValidationResult
{
    List<ValidationError> Errors { get;  }
    void AddError(string propertyName, string errorMessage);
    bool IsValid { get; set; }
}