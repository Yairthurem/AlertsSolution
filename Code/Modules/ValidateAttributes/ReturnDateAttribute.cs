using System.ComponentModel.DataAnnotations;
using FlightAlerts.Modules;
public class ReturnDate : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var alert = (Alert)validationContext.ObjectInstance;

        if (alert.ReturnDate.HasValue && alert.DepartureDate > alert.ReturnDate.Value)
        {
            return new ValidationResult("ReturnDate must be equal to or later than DepartureDate.");
        }

        return ValidationResult.Success;
    }
}