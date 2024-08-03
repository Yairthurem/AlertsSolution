using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class FlightAbbrevationValidAttribute : ValidationAttribute
{
    private const int RequiredLength = 6;

    public FlightAbbrevationValidAttribute()
    {
        ErrorMessage = $"The field must be exactly {RequiredLength} English alphabet characters.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var stringValue = value as string;

        // Check if the string is exactly 6 characters long and contains only letters
        if (stringValue != null &&
            stringValue.Length == RequiredLength &&
            Regex.IsMatch(stringValue, @"^[a-zA-Z]+$"))
        {
            return ValidationResult.Success;
        }
        return new ValidationResult(ErrorMessage);
    }
}