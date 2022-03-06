namespace OnlineCourseManagementSystem.Web.ViewModels.Subjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;

    public class SubjectRoadMapViewModel : IMapFrom<Subject>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<CourseSubjectViewModel> Courses { get; set; }
    }
}
