namespace OnlineCourseManagementSystem.Web.ViewModels.Students
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllStudentsByCourseViewModel : IMapFrom<Student>
    {
        public string Id { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }
    }
}
