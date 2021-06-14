namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Models;

    public class Emoji : BaseDeletableModel<int>
    {
        public Emoji()
        {
            this.Messages = new HashSet<MessageEmoji>();
        }

        public string RemoteUrl { get; set; }

        public string Extension { get; set; }

        public virtual ICollection<MessageEmoji> Messages { get; set; }
    }
}
