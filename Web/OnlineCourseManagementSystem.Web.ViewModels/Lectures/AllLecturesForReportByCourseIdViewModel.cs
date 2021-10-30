using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    public class AllLecturesForReportByCourseIdViewModel : IMapFrom<Lecture>
    {
        public string Title { get; set; }

        public int CompletitionsCount { get; set; }
    }
}
