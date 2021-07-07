using AutoMapper;
using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    public class AllExamsByUserIdViewModel : IMapFrom<Exam>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Question> Questions { get; set; }

        [IgnoreMap]
        public int PassMarks => (int)(this.TotalMarks * 0.5);

        [IgnoreMap]
        public int TotalMarks => this.Questions.Sum(q => q.Points);
    }
}
