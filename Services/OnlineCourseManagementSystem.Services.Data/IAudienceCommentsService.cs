using OnlineCourseManagementSystem.Web.ViewModels.AudienceComments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface IAudienceCommentsService
    {
        Task CreateAsync(CreateAudienceCommentInputModel input);

        IEnumerable<T> GetAllByChannelCode<T>(string code);
    }
}
