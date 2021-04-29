namespace OnlineCourseManagementSystem.Web.ViewModels.Absences
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models.Enumerations;

    public class BaseAbsenceInputModel
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public int LectureId { get; set; }

        [Required]
        public AbsenceType Type { get; set; }

        [MaxLength(100)]
        public string Reason { get; set; }

        public string StudentId { get; set; }
    }
}
