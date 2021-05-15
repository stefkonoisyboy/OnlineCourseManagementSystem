namespace OnlineCourseManagementSystem.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ReplyToCommentInputModel : BaseCommentInputModel
    {
        public int ParentId { get; set; }
    }
}
