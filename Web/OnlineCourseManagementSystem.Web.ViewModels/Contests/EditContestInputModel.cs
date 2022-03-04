using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Contests
{
    public class EditContestInputModel : BaseContestInputModel, IMapFrom<Contest>
    {
        public int Id { get; set; }
    }
}
