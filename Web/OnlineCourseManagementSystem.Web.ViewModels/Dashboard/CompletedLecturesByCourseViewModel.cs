using AutoMapper;
using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Dashboard
{
    public class CompletedLecturesByCourseViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [IgnoreMap]
        public int CompletedLectures { get; set; }
    }
}
