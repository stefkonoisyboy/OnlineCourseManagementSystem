namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.VideoConferences;

    public interface ITwilioService
    {
        IEnumerable<RoomDetails> GetAllRoomsAsync();

        TwilioJwt GetTwilioJwt(string identity);
    }
}
