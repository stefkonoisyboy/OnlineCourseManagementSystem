namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllExamsByUserIdViewModel : IMapFrom<Exam>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CourseDescription { get; set; }

        public int LectureId { get; set; }

        public bool? IsCertificated { get; set; }

        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public string CourseSubjectName { get; set; }

        public string CourseFileRemoteUrl { get; set; }

        public string ExamType => this.IsCertificated.HasValue ? "Certification Exam" : "Lecture Exam";

        public IEnumerable<Question> Questions { get; set; }

        [IgnoreMap]
        public int PassMarks => (int)(this.TotalMarks * 0.5);

        [IgnoreMap]
        public int TotalMarks => this.Questions.Sum(q => q.Points);
    }
}
