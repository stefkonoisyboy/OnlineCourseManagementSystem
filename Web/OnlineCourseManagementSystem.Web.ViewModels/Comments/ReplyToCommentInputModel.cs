using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Comments
{
    public class ReplyToCommentInputModel : BaseCommentInputModel
    {
        public int ParentId { get; set; }
    }
}
