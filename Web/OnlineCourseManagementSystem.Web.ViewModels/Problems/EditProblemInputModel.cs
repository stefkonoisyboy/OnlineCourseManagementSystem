namespace OnlineCourseManagementSystem.Web.ViewModels.Problems
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class EditProblemInputModel : BaseProblemInputModel, IMapFrom<Problem>
    {
        public int Id { get; set; }
    }
}
