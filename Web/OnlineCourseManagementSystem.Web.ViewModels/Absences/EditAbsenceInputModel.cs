namespace OnlineCourseManagementSystem.Web.ViewModels.Absences
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class EditAbsenceInputModel : IMapFrom<Absence>
    {
        public int CourseId { get; set; }

        public int LectureId { get; set; }

        public AbsenceType Type { get; set; }

        public string Reason { get; set; }

        public string StudentId { get; set; }

        public int Id { get; set; }

        public string StudentUserFirstName { get; set; }

        public string StudentUserLastName { get; set; }
    }
}
