using System;
using System.ComponentModel.DataAnnotations;

public class GreaterThanAttribute : ValidationAttribute
{
    private readonly double _minValue;

    public GreaterThanAttribute(double minValue)
    {
        _minValue = minValue;
        ErrorMessage = $"The value must be greater than {_minValue}.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (double.TryParse(value.ToString(), out var numericValue))
        {
            if (numericValue > _minValue)
            {
                return ValidationResult.Success;
            }
        }
        return new ValidationResult(ErrorMessage);
    }
}