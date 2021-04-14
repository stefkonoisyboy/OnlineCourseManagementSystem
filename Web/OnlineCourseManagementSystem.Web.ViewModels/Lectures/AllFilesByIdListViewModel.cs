using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    public class AllFilesByIdListViewModel
    {
        public IEnumerable<AllFilesByIdViewModel> Files { get; set; }
    }
}
