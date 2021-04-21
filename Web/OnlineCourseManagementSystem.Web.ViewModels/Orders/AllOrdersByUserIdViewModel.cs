using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Orders
{
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
