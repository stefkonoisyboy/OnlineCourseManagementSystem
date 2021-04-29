namespace OnlineCourseManagementSystem.Web.ViewModels.Absences
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Students;

    public class CreateAbsenceListInputModel
    {
        public List<CreateAbsenceInputModel> Inputs { get; set; }

        public IEnumerable<AllStudentsByCourseViewModel> Students { get; set; }

        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public int LectureId { get; set; }

        public string LectureTitle { get; set; }
    }
}
