namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class EditCourseInputModel : BaseCourseInputModel, IMapFrom<Course>
    {
        public int Id { get; set; }
    }
}
