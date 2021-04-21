using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Orders
{
    public class AllOrdersByUserIdListViewModel
    {
        public IEnumerable<AllOrdersByUserIdViewModel> Orders { get; set; }
    }
}
