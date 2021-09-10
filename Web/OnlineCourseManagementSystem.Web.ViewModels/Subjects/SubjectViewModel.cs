namespace OnlineCourseManagementSystem.Web.ViewModels.Subjects
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class SubjectViewModel : IMapFrom<Subject>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
