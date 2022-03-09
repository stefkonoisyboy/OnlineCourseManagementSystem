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
    using OnlineCourseManagementSystem.Web.ViewModels.ChatbotMessages;

    public class ChatbotMessagesService : IChatbotMessagesService
    {
        private readonly IDeletableEntityRepository<ChatbotMessage> chatbotMessagesRepository;

        public ChatbotMessagesService(IDeletableEntityRepository<ChatbotMessage> chatbotMessagesRepository)
        {
            this.chatbotMessagesRepository = chatbotMessagesRepository;
        }

        public async Task CreateAsync(CreateChatbotMessageInputModel input)
        {
            if (string.IsNullOrWhiteSpace(input.Content))
            {
                throw new ArgumentException("Cannot ask an empty question!");
            }

            ChatbotMessage messageFromUser = new ChatbotMessage
            {
                Content = input.Content,
                CreatorId = input.CreatorId,
                IsMessageFromChatbot = false,
            };

            string messageFromChatbotContent = this.GenerateMessageFromChatbot(input.Content);

            ChatbotMessage messageFromChatbot = new ChatbotMessage
            {
                Content = messageFromChatbotContent,
                CreatorId = input.CreatorId,
                IsMessageFromChatbot = true,
            };

            await this.chatbotMessagesRepository.AddAsync(messageFromUser);
            await this.chatbotMessagesRepository.AddAsync(messageFromChatbot);

            await this.chatbotMessagesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByCreatorId<T>(string creatorId)
        {
            return this.chatbotMessagesRepository
                .All()
                .Where(cm => cm.CreatorId == creatorId)
                .OrderBy(cm => cm.CreatedOn)
                .To<T>()
                .ToList();
        }

        private string GenerateMessageFromChatbot(string content)
        {
            if (content.ToLower().Contains("offer"))
            {
                return "We offer: watching courses for life once you purchase them; premium support; learning from top lecturers; lecture source files; printed diploma; new lectures every day.";
            }
            else if (content.ToLower().Contains("where") && (content.ToLower().Contains("buy") || content.ToLower().Contains("purchase")) && content.ToLower().Contains("courses"))
            {
                return "You can buy your courses when you go to the page Upcoming and Active Courses, then go to details of certain course and click the Start Course button.";
            }
            else if (content.ToLower().Contains("good") && content.ToLower().Contains("morning"))
            {
                return "Good morning! How can I help you today?";
            }
            else if (content.ToLower().Contains("good") && content.ToLower().Contains("afternoon"))
            {
                return "Good afternoon! How can I help you today?";
            }
            else if (content.ToLower().Contains("good") && content.ToLower().Contains("evening"))
            {
                return "Good evening! How can I help you today?";
            }
            else if (content.ToLower().Contains("hi"))
            {
                return "Hi! How can I help you today?";
            }
            else if (content.ToLower().Contains("hello"))
            {
                return "Hello! How can I help you today?";
            }
            else if (content.ToLower().Contains("ask"))
            {
                return "Hello! How can I help you today?";
            }
            else if (content.ToLower().Contains("certificate") || content.ToLower().Contains("certification"))
            {
                return "You can get certificate by completing the assigned course to it.";
            }

            return "Can you repeat your question?";
        }
    }
}
