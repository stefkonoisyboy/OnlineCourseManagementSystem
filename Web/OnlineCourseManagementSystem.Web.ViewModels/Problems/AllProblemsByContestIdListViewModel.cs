using OnlineCourseManagementSystem.Web.ViewModels.Paging;
using OnlineCourseManagementSystem.Web.ViewModels.Submissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Problems
{
    public class AllProblemsByContestIdListViewModel : PagingViewModel
    {
        public IEnumerable<AllProblemsByContestIdViewModel> Problems { get; set; }

        public IEnumerable<Top5SubmissionsByUserIdAndProblemIdViewModel> Submissions { get; set; }

        public CreateSubmissionInputModel Input { get; set; }

        public CurrentProblemViewModel CurrentProblem { get; set; }
    }
}
