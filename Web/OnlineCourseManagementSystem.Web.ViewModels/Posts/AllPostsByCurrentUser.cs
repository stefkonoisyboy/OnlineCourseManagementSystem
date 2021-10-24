namespace OnlineCourseManagementSystem.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllPostsByCurrentUser
    {
        public IEnumerable<PostViewModel> Posts { get; set; }
    }
}
