using System.ComponentModel.DataAnnotations;

namespace DataAccess.Validation
{
    public class ValidateGenderAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string? gender = value?.ToString();

            return gender == "K" || gender == "M";
        }
    }
}
