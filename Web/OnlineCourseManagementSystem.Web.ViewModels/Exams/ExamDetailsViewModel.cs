namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class ExamDetailsViewModel : IMapFrom<Exam>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string LecturerFirstName { get; set; }

        public string LecturerLastName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<Question> Questions { get; set; }

        [IgnoreMap]
        public int PassMarks => (int)(this.TotalMarks * 0.5);

        [IgnoreMap]
        public int TotalMarks => this.Questions.Sum(q => q.Points);
    }
}
