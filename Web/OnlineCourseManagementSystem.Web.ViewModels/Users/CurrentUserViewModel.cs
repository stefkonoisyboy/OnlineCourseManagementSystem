namespace OnlineCourseManagementSystem.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class CurrentUserViewModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileImageUrl { get; set; }
    }
}
