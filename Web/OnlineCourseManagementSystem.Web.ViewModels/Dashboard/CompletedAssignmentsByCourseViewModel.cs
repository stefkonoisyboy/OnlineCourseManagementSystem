namespace OnlineCourseManagementSystem.Web.ViewModels.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class CompletedAssignmentsByCourseViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [IgnoreMap]
        public int CompletedAssignments { get; set; }
    }
}
