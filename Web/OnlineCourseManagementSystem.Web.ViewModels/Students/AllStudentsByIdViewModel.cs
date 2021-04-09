namespace OnlineCourseManagementSystem.Web.ViewModels.Students
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllStudentsByIdViewModel : IMapFrom<Student>
    {
        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }
    }
}
