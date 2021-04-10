namespace OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CheckIfEndDateIsGreaterThanStartDateValidationAttribute : ValidationAttribute
    {
        public CheckIfEndDateIsGreaterThanStartDateValidationAttribute(string startDate)
        {
            this.StartDate = startDate;
        }

        public string StartDate { get; set; }

        public override bool IsValid(object value)
        {
            if (value is DateTime endDate)
            {
                if (endDate < DateTime.Parse(this.StartDate))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
