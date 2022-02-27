namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Web.ViewModels.AudienceComments;

    public interface IAudienceCommentsService
    {
        Task CreateAsync(CreateAudienceCommentInputModel input);

        IEnumerable<T> GetAllByChannelCode<T>(string code);
    }
}
