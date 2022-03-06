namespace OnlineCourseManagementSystem.Web.ViewModels.Shedules
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class SheduleViewModel : IMapFrom<Shedule>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public int Duration { get; set; }

        public string FormattedDuartion => this.FormatDuration();

        private string FormatDuration()
        {
            int hours = this.Duration / 60;
            if (hours != 0)
            {
                if (hours > 1)
                {
                    return hours + " hours";
                }
                else
                {
                    return hours + " hour";
                }
            }

            return this.Duration + " minutes";
        }
    }
}
