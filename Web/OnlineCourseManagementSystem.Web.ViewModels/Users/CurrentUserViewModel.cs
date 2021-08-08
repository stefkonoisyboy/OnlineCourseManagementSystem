using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Users
{
    public class CurrentUserViewModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileImageUrl { get; set; }
    }
}
