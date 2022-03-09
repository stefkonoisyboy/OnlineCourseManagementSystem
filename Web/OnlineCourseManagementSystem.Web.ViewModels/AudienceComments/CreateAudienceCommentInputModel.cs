namespace OnlineCourseManagementSystem.Web.ViewModels.AudienceComments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateAudienceCommentInputModel
    {
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public int ChannelId { get; set; }
    }
}
