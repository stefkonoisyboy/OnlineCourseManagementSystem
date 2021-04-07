using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Users
{
    public class AllUsersListViewModel
    {
        public IEnumerable<AllUsersViewModel> Users { get; set; }
    }
}
