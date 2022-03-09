namespace OnlineCourseManagementSystem.Web.ViewModels.Contests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class EditContestInputModel : BaseContestInputModel, IMapFrom<Contest>
    {
        public int Id { get; set; }
    }
}
