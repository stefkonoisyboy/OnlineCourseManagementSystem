namespace OnlineCourseManagementSystem.Web.ViewModels.Subjects
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Courses;

    public class AllSubjectsRoadMapViewModel
    {
        public SubjectViewModel ProgrammingBasics { get; set; }

        public SubjectViewModel Fundamentals { get; set; }

        public IEnumerable<SubjectRoadMapViewModel> CsharpSubjectsRoadMap { get; set; }

        public IEnumerable<SubjectRoadMapViewModel> JavaSubjectsRoadMap { get; set; }

        public IEnumerable<SubjectRoadMapViewModel> JSSubjectRoadMap { get; set; }
    }
}
