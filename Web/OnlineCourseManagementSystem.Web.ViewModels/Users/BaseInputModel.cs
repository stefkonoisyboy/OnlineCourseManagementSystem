namespace OnlineCourseManagementSystem.Web.ViewModels.Users
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Web.Infrastructure.ValidationAttributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class BaseInputModel
    {
        public string Id { get; set; }

        public string ProfileImageUrl { get; set; }

        [ImageExtensionValidation]
        public IFormFile NewImage { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Background { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public int TownId { get; set; }

        public IEnumerable<SelectListItem> TownItems { get; set; }

        [Required]
        public string Address { get; set; }

        [IgnoreMap]
        public bool Subscribed { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
