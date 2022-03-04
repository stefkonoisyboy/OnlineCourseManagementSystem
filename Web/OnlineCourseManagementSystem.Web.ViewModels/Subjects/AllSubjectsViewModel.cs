using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Subjects
{
    public class AllSubjectsViewModel : IMapFrom<Subject>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CoursesCount { get; set; }
    }
}
