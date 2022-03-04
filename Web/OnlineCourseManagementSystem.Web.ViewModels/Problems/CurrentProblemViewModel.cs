using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Problems
{
    public class CurrentProblemViewModel : IMapFrom<Problem>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ContestId { get; set; }
    }
}
