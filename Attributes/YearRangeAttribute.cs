using System;
using System.ComponentModel.DataAnnotations;

namespace Projet_5.Attributes
{
    public class YearRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int year)
            {
                int currentYear = DateTime.Now.Year;

                if (year < 1990 || year > currentYear )

                {
                    return new ValidationResult($"L'année doit être comprise entre 1990 et {currentYear}.");
                }
            }
            return ValidationResult.Success!;
        }
    }
}
