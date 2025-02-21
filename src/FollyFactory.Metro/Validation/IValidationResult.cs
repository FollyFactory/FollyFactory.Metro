namespace FollyFactory.Metro.Validation;
public interface IValidationResult
{
    List<ValidationError> Errors { get; set; }
    bool IsValid { get; set; }
}

public record ValidationError(string PropertyName, string ErrorMessage);