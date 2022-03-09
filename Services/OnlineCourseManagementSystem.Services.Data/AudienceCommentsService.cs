namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.AudienceComments;

    public class AudienceCommentsService : IAudienceCommentsService
    {
        private readonly IDeletableEntityRepository<AudienceComment> audienceCommentsRepository;

        public AudienceCommentsService(IDeletableEntityRepository<AudienceComment> audienceCommentsRepository)
        {
            this.audienceCommentsRepository = audienceCommentsRepository;
        }

        public async Task CreateAsync(CreateAudienceCommentInputModel input)
        {
            AudienceComment audienceComment = new AudienceComment
            {
                Content = input.Content,
                AuthorId = input.AuthorId,
                ChannelId = input.ChannelId,
            };

            await this.audienceCommentsRepository.AddAsync(audienceComment);
            await this.audienceCommentsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByChannelCode<T>(string code)
        {
            return this.audienceCommentsRepository
                .All()
                .Where(ac => ac.Channel.Code == code)
                .OrderByDescending(ac => ac.CreatedOn)
                .To<T>()
                .ToList();
        }
    }
}
