namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllLecturesByCreatorIdViewModel : IMapFrom<Lecture>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string CourseFileRemoteUrl { get; set; }

        public string DifferenceBetweenNowAndModifiedOn => this.DifferenceInSpecialFormat();

        private string DifferenceInSpecialFormat()
        {
            DateTime dateTime = new DateTime(0001, 1, 1, 0, 0, 0);

            if (this.ModifiedOn == null || this.ModifiedOn == dateTime)
            {
                return "Not updated yet";
            }

            string result = string.Empty;

            TimeSpan diff = DateTime.UtcNow.Subtract(this.ModifiedOn);
            int seconds = (int)diff.TotalSeconds;
            int minutes = seconds / 60;
            int hours = minutes / 60;
            int days = hours / 24;
            int weeks = days / 7;
            int years = weeks / 52;

            if (seconds == 0)
            {
                result = "Updated now";
            }
            else if (seconds == 1)
            {
                result = "1 second ago";
            }
            else if (seconds > 1 && seconds <= 59)
            {
                result = $"{seconds} seconds ago";
            }
            else if (minutes == 1)
            {
                result = "1 minute ago";
            }
            else if (minutes > 1 && minutes <= 59)
            {
                result = $"{minutes} minutes ago";
            }
            else if (hours == 1)
            {
                result = "1 hour ago";
            }
            else if (hours > 1 && hours <= 23)
            {
                result = $"{hours} hours ago";
            }
            else if (days == 1)
            {
                result = "1 day ago";
            }
            else if (days > 1 && days <= 6)
            {
                result = $"{days} days ago";
            }
            else if (weeks == 1)
            {
                result = "1 week ago";
            }
            else if (weeks > 1 && weeks <= 51)
            {
                result = $"{weeks} weeks ago";
            }
            else if (years == 1)
            {
                result = "1 year ago";
            }
            else if (years > 1)
            {
                result = $"{years} years ago";
            }

            return result;
        }
    }
}
