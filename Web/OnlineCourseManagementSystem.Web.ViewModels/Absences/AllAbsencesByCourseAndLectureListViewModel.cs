namespace OnlineCourseManagementSystem.Web.ViewModels.Absences
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllAbsencesByCourseAndLectureListViewModel
    {
        public string CourseName { get; set; }

        public string LectureTitle { get; set; }

        public int CourseId { get; set; }

        public int LectureId { get; set; }

        public IEnumerable<AllAbsencesByCourseAndLectureViewModel> Absences { get; set; }
    }
}
