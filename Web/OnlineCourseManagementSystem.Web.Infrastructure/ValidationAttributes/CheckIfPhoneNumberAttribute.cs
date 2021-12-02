namespace OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CheckIfPhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string phone)
            {
                foreach (var number in phone)
                {
                    if (int.TryParse(number.ToString(), out int result))
                    {
                        if (!(result >= 0 && result <= 9))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
