namespace OnlineCourseManagementSystem.Web.ViewModels.ContactMessages
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;

    public class CreateContactMessageInputModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [MinLength(10)]
        [MaxLength(10)]
        [CheckIfPhoneNumber]
        public string Phone { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
