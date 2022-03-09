namespace OnlineCourseManagementSystem.Web.ViewModels.Reviews
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllReviewsByCourseIdViewModel : IMapFrom<Review>
    {
        public string Content { get; set; }

        public double Rating { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserProfileImageUrl { get; set; }
    }
}
