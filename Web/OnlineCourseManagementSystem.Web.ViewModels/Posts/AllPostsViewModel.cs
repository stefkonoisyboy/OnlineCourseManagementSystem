namespace OnlineCourseManagementSystem.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Courses;

    public class AllPostsViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }

        public IEnumerable<CourseViewModel> Courses { get; set; }

        public int CountOfAllPosts { get; set; }

        public string Search { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int PostsCount { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.PagesCount ? this.PagesCount : this.CurrentPage + 1;

        public int CourseId { get; set; }
    }
}
