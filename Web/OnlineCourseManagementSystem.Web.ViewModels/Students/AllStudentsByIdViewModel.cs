using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Students
{
    public class AllStudentsByIdViewModel : IMapFrom<Student>
    {
        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }
    }
}
