using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Problems
{
    public class EditProblemInputModel : BaseProblemInputModel, IMapFrom<Problem>
    {
        public int Id { get; set; }
    }
}
