using AutoMapper;
using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Courses
{
    public class CoursesChartViewModel : IMapFrom<UserExam>
    {
        public string ExamName { get; set; }

        public double Grade { get; set; }
    }
}
