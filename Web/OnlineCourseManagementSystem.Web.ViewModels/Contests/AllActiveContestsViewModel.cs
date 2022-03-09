namespace OnlineCourseManagementSystem.Web.ViewModels.Contests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllActiveContestsViewModel : IMapFrom<Contest>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<Problem> Problems { get; set; }

        [IgnoreMap]
        public int RemainingDays => (this.EndDate - DateTime.UtcNow).Days;

        [IgnoreMap]
        public int RemainingHours => (this.EndDate - DateTime.UtcNow).Hours;

        [IgnoreMap]
        public int RemainingMinutes => (this.EndDate - DateTime.UtcNow).Minutes;
    }
}
