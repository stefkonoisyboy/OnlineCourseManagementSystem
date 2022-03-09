namespace OnlineCourseManagementSystem.Web.ViewModels.MessageQAs
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllMessagesByChannelIdViewModel : IMapFrom<MessageQA>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string CreatorId { get; set; }

        public string CreatorFirstName { get; set; }

        public string CreatorLastName { get; set; }

        public string CreatorProfileImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string DifferenceBetweenNowAndCreatedOn => this.DifferenceInSpecialFormat();

        public int Replies { get; set; }

        public bool IsStarred { get; set; }

        public bool IsHighlighted { get; set; }

        public int LikesCount { get; set; }

        public string ClassName => (this.IsHighlighted || this.IsStarred) ? this.IsHighlighted ? "bg-info" : "bg-light" : "bg-white";

        private string DifferenceInSpecialFormat()
        {
            string result = string.Empty;

            TimeSpan diff = DateTime.UtcNow.Subtract(this.CreatedOn);
            int seconds = (int)diff.TotalSeconds;
            int minutes = seconds / 60;
            int hours = minutes / 60;
            int days = hours / 24;
            int weeks = days / 7;
            int years = weeks / 52;

            if (seconds == 1)
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
