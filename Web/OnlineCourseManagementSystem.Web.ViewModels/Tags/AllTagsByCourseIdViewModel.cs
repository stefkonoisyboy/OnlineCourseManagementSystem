using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Tags
{
    public class AllTagsByCourseIdViewModel : IMapFrom<CourseTag>
    {
        public string TagName { get; set; }
    }
}
