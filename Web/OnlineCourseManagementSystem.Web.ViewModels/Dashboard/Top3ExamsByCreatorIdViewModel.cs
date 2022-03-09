namespace OnlineCourseManagementSystem.Web.ViewModels.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class Top3ExamsByCreatorIdViewModel : IMapFrom<Exam>
    {
        public string Name { get; set; }

        public ICollection<UserExam> Users { get; set; }

        public double AverageSuccess => this.Users.Count() == 0 ? 0 : this.Users.Average(u => u.Grade);
    }
}
