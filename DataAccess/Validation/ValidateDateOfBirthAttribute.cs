using System.ComponentModel.DataAnnotations;

namespace DataAccess.Validation
{
    public class ValidateDateOfBirthAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime? dateOfBirth = value as DateTime?;

            if (dateOfBirth is null)
            {
                return false;
            }

            return dateOfBirth <= DateTime.UtcNow;
        }
    }
}
