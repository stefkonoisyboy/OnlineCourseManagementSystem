namespace OnlineCourseManagementSystem.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllOrdersByUserIdViewModel : IMapFrom<Order>
    {
        public int CourseId { get; set; }

        public string CourseFileRemoteUrl { get; set; }

        public string CourseName { get; set; }

        public string CourseDescription { get; set; }

        public decimal CoursePrice { get; set; }

        public DateTime CourseStartDate { get; set; }

        public DateTime CourseEndDate { get; set; }
    }
}
