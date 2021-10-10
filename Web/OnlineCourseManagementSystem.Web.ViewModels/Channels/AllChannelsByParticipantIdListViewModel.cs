using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Channels
{
    public class AllChannelsByParticipantIdListViewModel
    {
        public IEnumerable<GetAllChannelsByParticipantIdViewModel> Channels { get; set; }

        public CreateChannelInputModel Input { get; set; }

        public JoinChannelInputModel JoinInput { get; set; }
    }
}
