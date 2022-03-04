namespace OnlineCourseManagementSystem.Web.ViewModels.Home
{
    using OnlineCourseManagementSystem.Web.ViewModels.ChatbotMessages;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Reviews;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class HomeViewModel
    {
        public int MyProperty { get; set; }

        public int AssignmentsCount { get; set; }

        public int CoursesInCartCount { get; set; }

        public string UserId { get; set; }

        public IEnumerable<TopLatestCoursesViewModel> LatestCourses { get; set; }

        public IEnumerable<TopNextCoursesViewModel> NextCourses { get; set; }

        public IEnumerable<AllReviewsByCourseIdViewModel> Reviews { get; set; }

        public CurrentUserViewModel CurrentUser { get; set; }

        public AllChatbotMessagesByCreatorIdListViewModel Chatbot { get; set; }
    }
}
