using OnlineCourseManagementSystem.Web.ViewModels.Subjects;
using OnlineCourseManagementSystem.Web.ViewModels.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    public class UpcomingAndActiveCoursesViewModel
    {
        public IEnumerable<AllUpcomingCoursesViewModel> UpcomingCourses { get; set; }

        public AllActiveCoursesListViewModel ListOfActiveCourses { get; set; }

        public IEnumerable<AllTagsViewModel> Tags { get; set; }

        public IEnumerable<AllSubjectsViewModel> Subjects { get; set; }

        public string Name { get; set; }
    }
}
