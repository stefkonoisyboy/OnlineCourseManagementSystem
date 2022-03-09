namespace OnlineCourseManagementSystem.Web.ViewModels.Tags
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllTagsByCourseIdViewModel : IMapFrom<CourseTag>
    {
        public string TagName { get; set; }
    }
}
