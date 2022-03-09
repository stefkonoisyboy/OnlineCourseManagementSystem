namespace OnlineCourseManagementSystem.Web.ViewModels.Contests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllFinishedContestsViewModel : IMapFrom<Contest>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime EndDate { get; set; }
    }
}
