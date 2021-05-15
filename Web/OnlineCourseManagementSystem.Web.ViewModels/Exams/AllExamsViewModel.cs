namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllExamsViewModel : IMapFrom<Exam>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CourseName { get; set; }

        public string LecturerFirstName { get; set; }

        public string LecturerLastName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
