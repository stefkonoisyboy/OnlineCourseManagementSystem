namespace OnlineCourseManagementSystem.Web.ViewModels.Channels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllChannelsByParticipantIdListViewModel
    {
        public IEnumerable<GetAllChannelsByParticipantIdViewModel> Channels { get; set; }

        public CreateChannelInputModel Input { get; set; }

        public JoinChannelInputModel JoinInput { get; set; }
    }
}
