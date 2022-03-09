namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllLecturesForReportByCourseIdViewModel : IMapFrom<Lecture>
    {
        public string Title { get; set; }

        public int CompletitionsCount { get; set; }
    }
}
