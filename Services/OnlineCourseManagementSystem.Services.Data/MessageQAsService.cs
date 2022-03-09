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
    using OnlineCourseManagementSystem.Web.ViewModels.MessageQAs;

    public class MessageQAsService : IMessageQAsService
    {
        private readonly IDeletableEntityRepository<MessageQA> messagesRepository;
        private readonly IDeletableEntityRepository<Like> likesRepository;

        public MessageQAsService(IDeletableEntityRepository<MessageQA> messagesRepository, IDeletableEntityRepository<Like> likesRepository)
        {
            this.messagesRepository = messagesRepository;
            this.likesRepository = likesRepository;
        }

        public async Task ArchiveAllAsync()
        {
            ICollection<MessageQA> messages = this.messagesRepository
                .All()
                .Where(m => !m.IsArchived)
                .ToList();

            foreach (var message in messages)
            {
                message.IsArchived = true;
            }

            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task ArchiveAsync(int messageId)
        {
            MessageQA message = this.messagesRepository.All().FirstOrDefault(m => m.Id == messageId);
            message.IsArchived = true;
            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task CreateAsync(CreateMessageQAInputModel input)
        {
            MessageQA message = new MessageQA
            {
                Content = input.Content,
                CreatorId = input.CreatorId,
                ChannelId = input.ChannelId,
            };

            await this.messagesRepository.AddAsync(message);
            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task CreateLikeAsync(string creatorId, int messageId)
        {
            Like like = new Like
            {
                CreatorId = creatorId,
                MessageQAId = messageId,
            };

            await this.likesRepository.AddAsync(like);
            await this.likesRepository.SaveChangesAsync();
        }

        public async Task CreateReplyAsync(CreateReplyInputModel input)
        {
            MessageQA reply = new MessageQA
            {
                Content = input.Content,
                ChannelId = input.ChannelId,
                CreatorId = input.CreatorId,
                ParentId = input.ParentId,
            };

            await this.messagesRepository.AddAsync(reply);
            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int messageId)
        {
            MessageQA message = this.messagesRepository.All().FirstOrDefault(m => m.Id == messageId);
            this.messagesRepository.Delete(message);
            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task DeleteLikeAsync(string creatorId, int messageId)
        {
            Like like = this.likesRepository.All().FirstOrDefault(l => l.CreatorId == creatorId && l.MessageQAId == messageId);
            this.likesRepository.Delete(like);
            await this.likesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllArchivedMessagesByChannelId<T>(int channelId)
        {
            return this.messagesRepository
                .All()
                .OrderByDescending(m => m.IsHighlighted)
                .ThenByDescending(m => m.ModifiedOn)
                .Where(m => m.ChannelId == channelId && !m.ParentId.HasValue && !m.IsAnswered.HasValue && m.IsArchived)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllMessagesByChannelId<T>(int channelId)
        {
            return this.messagesRepository
                .All()
                .OrderByDescending(m => m.IsHighlighted)
                .ThenByDescending(m => m.Likes.Count())
                .ThenByDescending(m => m.ModifiedOn)
                .Where(m => m.ChannelId == channelId && !m.ParentId.HasValue && !m.IsAnswered.HasValue && !m.IsArchived)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllRecentMessagesByChannelId<T>(int channelId)
        {
            return this.messagesRepository
                .All()
                .OrderByDescending(m => m.CreatedOn)
                .Where(m => m.ChannelId == channelId && !m.ParentId.HasValue && !m.IsAnswered.HasValue && !m.IsArchived)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllReplies<T>()
        {
            return this.messagesRepository
                .All()
                .Where(m => m.ParentId != null)
                .OrderByDescending(m => m.CreatedOn)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllRepliesByParentId<T>(int parentId)
        {
            return this.messagesRepository
                .All()
                .Where(r => r.ParentId == parentId)
                .OrderByDescending(r => r.CreatedOn)
                .To<T>()
                .ToList();
        }

        public bool GetAnsweredStatus(int messageId)
        {
            MessageQA message = this.messagesRepository.All().FirstOrDefault(m => m.Id == messageId);
            return (message.IsAnswered.HasValue == false || message.IsAnswered.Value == false) ? false : true;
        }

        public T GetById<T>(int messageId)
        {
            return this.messagesRepository
                .All()
                .Where(m => m.Id == messageId)
                .To<T>()
                .FirstOrDefault();
        }

        public int GetCountOfRepliesForGivenMessage(int messageId)
        {
            return this.messagesRepository
                .All()
                .Count(m => m.ParentId == messageId);
        }

        public bool GetHighlightedStatus(int messageId)
        {
            MessageQA message = this.messagesRepository.All().FirstOrDefault(m => m.Id == messageId);
            return (message.IsHighlighted.HasValue == false || message.IsHighlighted.Value == false) ? false : true;
        }

        public bool GetStarredStatus(int messageId)
        {
            MessageQA message = this.messagesRepository.All().FirstOrDefault(m => m.Id == messageId);
            return (message.IsStarred.HasValue == false || message.IsStarred.Value == false) ? false : true;
        }

        public bool HasUserLikedMessage(string creatorId, int messageId)
        {
            return this.likesRepository.All().Any(l => l.CreatorId == creatorId && l.MessageQAId == messageId);
        }

        public async Task MarkAsAnsweredAsync(int messageId, bool status)
        {
            MessageQA message = this.messagesRepository.All().FirstOrDefault(m => m.Id == messageId);
            message.IsAnswered = status;
            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task MarkAsHighlightedAsync(int messageId, bool status)
        {
            MessageQA message = this.messagesRepository.All().FirstOrDefault(m => m.Id == messageId);
            message.IsHighlighted = status;
            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task MarkAsStarredAsync(int messageId, bool status)
        {
            MessageQA message = this.messagesRepository.All().FirstOrDefault(m => m.Id == messageId);
            message.IsStarred = status;
            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task RestoreAllAsync()
        {
            ICollection<MessageQA> messages = this.messagesRepository
               .All()
               .Where(m => m.IsArchived)
               .ToList();

            foreach (var message in messages)
            {
                message.IsArchived = false;
            }

            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task RestoreAsync(int messageId)
        {
            MessageQA message = this.messagesRepository.All().FirstOrDefault(m => m.Id == messageId);
            message.IsArchived = false;
            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditMessageQAInputModel input)
        {
            MessageQA message = this.messagesRepository.All().FirstOrDefault(m => m.Id == input.Id);
            message.Content = input.Content;
            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task WithdrawAsync(int messageId)
        {
            MessageQA messageQA = this.messagesRepository.All().FirstOrDefault(m => m.Id == messageId);
            this.messagesRepository.Delete(messageQA);
            await this.messagesRepository.SaveChangesAsync();
        }
    }
}
