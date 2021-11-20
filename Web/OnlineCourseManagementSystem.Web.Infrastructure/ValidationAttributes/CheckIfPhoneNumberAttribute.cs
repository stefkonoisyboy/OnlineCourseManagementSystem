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
            foreach (var number in value.ToString())
            {
                if (!(number >= 0 && number <= 9))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
