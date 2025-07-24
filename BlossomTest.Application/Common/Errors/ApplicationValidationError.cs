namespace BlossomTest.Application.Common.Errors;

/// <summary>
/// Represents an exception that occurs when one or more validation failures have occurred.
/// </summary>
public sealed class ApplicationValidationError
{    
    public ApplicationValidationError(IEnumerable<ValidationFailure> failures)
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}
