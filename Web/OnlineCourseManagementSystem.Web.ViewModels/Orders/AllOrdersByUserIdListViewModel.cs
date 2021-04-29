namespace OnlineCourseManagementSystem.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllOrdersByUserIdListViewModel
    {
        public IEnumerable<AllOrdersByUserIdViewModel> Orders { get; set; }
    }
}
