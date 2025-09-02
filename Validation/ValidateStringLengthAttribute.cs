using System.ComponentModel.DataAnnotations;

namespace Supabase_Minimal_API.Validation;

public class ValidateStringLengthAttribute : ValidationAttribute
{
    int _allowedLength;
    string _columnName;

    public ValidateStringLengthAttribute(string columnName, int maxLength)
    {
        _columnName = columnName;
        _allowedLength = maxLength;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return new ValidationResult("No value given");

        string? stringValue = value as string;
        if (stringValue == null)
            return new ValidationResult("No value given");

        if (stringValue.Length > _allowedLength)
            return new ValidationResult($"{_columnName} length must be below {_allowedLength}");

        return ValidationResult.Success;
    }
}