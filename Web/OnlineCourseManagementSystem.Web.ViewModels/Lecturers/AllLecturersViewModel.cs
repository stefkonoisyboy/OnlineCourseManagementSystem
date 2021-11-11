namespace OnlineCourseManagementSystem.Web.ViewModels.Lecturers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllLecturersViewModel : IMapFrom<Lecturer>
    {
        public string Id { get; set; }

        public DateTime UserCreatedOn { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserEmail { get; set; }

        public string UserUserName { get; set; }

        public string UserPhoneNumber { get; set; }

        public string UserAddress { get; set; }

        public string UserProfileImageUrl { get; set; }

        public Title UserTitle { get; set; }
    }
}
