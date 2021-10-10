using OnlineCourseManagementSystem.Web.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.MessageQAs
{
    public class AllMessagesByChannleIdListViewModel
    {
        public IEnumerable<AllMessagesByChannelIdViewModel> Messages { get; set; }

        public CreateMessageQAInputModel Input { get; set; }

        public CurrentUserViewModel CurrentUser { get; set; }

        public int ChannelId { get; set; }
    }
}
