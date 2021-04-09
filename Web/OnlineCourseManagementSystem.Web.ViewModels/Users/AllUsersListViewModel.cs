namespace OnlineCourseManagementSystem.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllUsersListViewModel
    {
        public IEnumerable<AllUsersViewModel> Users { get; set; }
    }
}
