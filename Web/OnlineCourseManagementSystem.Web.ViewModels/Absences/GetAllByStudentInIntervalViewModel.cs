namespace OnlineCourseManagementSystem.Web.ViewModels.Absences
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class GetAllByStudentInIntervalViewModel : IMapFrom<Absence>
    {
        public string StudentUserFirstName { get; set; }

        public string StudentUserLastName { get; set; }

        public AbsenceType Type { get; set; }

        public string Reason { get; set; }

        public string LectureTitle { get; set; }

        public DateTime LectureStartDate { get; set; }

        public string CourseName { get; set; }
    }
}
