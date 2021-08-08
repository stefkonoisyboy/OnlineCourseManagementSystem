using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Lecturers
{
    public class AllLecturersByCourseIdViewModel : IMapFrom<CourseLecturer>
    {
        public string LecturerUserProfileImageUrl { get; set; }

        public string LecturerUserFirstName { get; set; }

        public string LecturerUserLastName { get; set; }

        public string LecturerUserBackground { get; set; }
    }
}
