namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Chats;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class ChatsService : IChatsService
    {
        private readonly IDeletableEntityRepository<Chat> chatRepository;
        private readonly IDeletableEntityRepository<ChatUser> chatUserRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly CloudinaryService cloudinaryService;

        public ChatsService(IDeletableEntityRepository<Chat> chatRepository, IDeletableEntityRepository<ChatUser> chatUserRepository, IDeletableEntityRepository<ApplicationUser> userRepository, Cloudinary cloudinaryUtility)
        {
            this.chatRepository = chatRepository;
            this.chatUserRepository = chatUserRepository;
            this.userRepository = userRepository;
            this.cloudinaryService = new CloudinaryService(cloudinaryUtility);
        }

        public async Task CreateAsync(CreateChatInputModel inputModel)
        {
            if (!inputModel.FriendsToAdd.Any())
            {
                return;
            }

            List<string> addedUsersId = inputModel.FriendsToAdd.ToList();
            addedUsersId.Add(inputModel.CreatorId);
            this.CheckExistingChat(inputModel.CreatorId, addedUsersId.AsEnumerable());

            Chat chat = new Chat()
            {
                Name = string.Empty,
            };

            ChatUser userCreator = new ChatUser
            {
                UserId = inputModel.CreatorId,
            };

            if (inputModel.FriendsToAdd.Count() == 1)
            {
                chat.ChatType = ChatType.NormalChat;
                bool isExistingNormalChat = await this.UnLeaveNormalChat(inputModel.CreatorId, inputModel.FriendsToAdd.FirstOrDefault());
                if (isExistingNormalChat)
                {
                    return;
                }
            }
            else if (inputModel.FriendsToAdd.Count() > 1)
            {
                chat.ChatType = ChatType.GroupChat;
                chat.CreatorId = inputModel.CreatorId;
            }

            List<string> friendsName = new List<string>();
            foreach (var friendId in inputModel.FriendsToAdd)
            {
                ApplicationUser friend = this.userRepository.All().FirstOrDefault(c => c.Id == friendId);
                string friendName = $"{friend.FirstName} {friend.LastName}";
                friendsName.Add(friendName);
                chat.Users.Add(userCreator);
                ChatUser userAdded = new ChatUser
                {
                    UserId = friendId,
                };

                ApplicationUser user = this.userRepository.All().FirstOrDefault(u => u.Id == friendId);
                chat.Users.Add(userAdded);
            }

            await this.chatRepository.AddAsync(chat);
            await this.chatRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllBy<T>(string userId)
        {
            return this.chatUserRepository
                .All()
                .Where(x => x.UserId == userId && x.IsPinned == false)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllPinnedBy<T>(string userId)
        {
            return this.chatUserRepository
                .All()
                .Where(c => c.IsPinned && c.UserId == userId)
                .To<T>()
                .ToList();
        }

        public string GetNameBy(int? chatId, string userId)
        {
            List<string> users = this.chatUserRepository
                .All()
                .Where(c => c.ChatId == chatId && c.UserId != userId)
                .Select(c => $"{c.User.FirstName} {c.User.LastName}")
                .ToList();
            Chat chat = this.chatRepository.All().FirstOrDefault(c => c.Id == chatId);

            string chatName = string.Empty;

            if (users.Count == 1)
            {
                chatName = users.First();
            }
            else
            {
                if (string.IsNullOrEmpty(chat.Name))
                {
                    chatName = string.Join(',', users);
                }
                else
                {
                    chatName = chat.Name;
                }
            }

            return chatName;
        }

        public IEnumerable<T> GetUsersByChat<T>(int chatId)
        {
            return this.chatUserRepository
                .All()
                .Where(c => c.ChatId == chatId)
                .OrderBy(c => c.User.FirstName)
                .ThenBy(c => c.User.LastName)
                .To<T>()
                .ToList();
        }

        public async Task LeaveChat(int chatId, string userId)
        {
            ChatUser chatUser = this.chatUserRepository.All().FirstOrDefault(c => c.ChatId == chatId && c.UserId == userId);
            if (chatUser.Chat.ChatType == ChatType.GroupChat)
            {
                this.chatUserRepository.HardDelete(chatUser);
            }
            else
            {
                this.chatUserRepository.Delete(chatUser);
            }

            await this.chatUserRepository.SaveChangesAsync();
        }

        public async Task MuteChat(int chatId, string userId)
        {
            ChatUser chat = this.chatUserRepository.All().FirstOrDefault(c => c.ChatId == chatId && c.UserId == userId);

            chat.IsMuted = true;

            await this.chatUserRepository.SaveChangesAsync();
        }

        public async Task PinChat(int chatId, string userId)
        {
            ChatUser chat = this.chatUserRepository.All().FirstOrDefault(c => c.ChatId == chatId && c.UserId == userId);

            chat.IsPinned = true;

            await this.chatUserRepository.SaveChangesAsync();
        }

        public async Task UnmuteChat(int chatId, string userId)
        {
            ChatUser chat = this.chatUserRepository.All().FirstOrDefault(c => c.ChatId == chatId && c.UserId == userId);

            chat.IsMuted = false;

            await this.chatUserRepository.SaveChangesAsync();
        }

        public async Task UnPinChat(int chatId, string userId)
        {
            ChatUser chat = this.chatUserRepository.All().FirstOrDefault(c => c.ChatId == chatId && c.UserId == userId);

            chat.IsPinned = false;

            await this.chatUserRepository.SaveChangesAsync();
        }

        public async Task<bool> UnLeaveNormalChat(string creatorId, string userId)
        {
            List<int> creatorChatIds = this.chatUserRepository
                .AllWithDeleted()
                .Where(cu => cu.IsDeleted == true
                && cu.Chat.ChatType == ChatType.NormalChat
                && cu.UserId == creatorId)
                .Select(cu => cu.ChatId)
                .ToList();

            List<int> userChatIds = this.chatUserRepository
                    .AllWithDeleted()
                    .Where(cu => cu.UserId == userId
                        && cu.Chat.ChatType == ChatType.NormalChat)
                    .Select(cu => cu.ChatId)
                    .ToList();

            foreach (var creatorChatId in creatorChatIds)
            {
                foreach (var userChatId in userChatIds)
                {
                    if (creatorChatId == userChatId)
                    {
                        ChatUser chat = this.chatUserRepository
                            .AllWithDeleted()
                            .Where(cu => cu.ChatId == userChatId
                                && cu.UserId == creatorId).First();

                        chat.IsDeleted = false;
                        await this.chatUserRepository.SaveChangesAsync();
                        return true;
                    }
                }
            }

            return false;
        }

        public void CheckExistingChat(string creatorId, IEnumerable<string> addedUserIds)
        {
            var creatorChats = this.chatRepository
                .All()
                .Where(c => c.Users.Any(u => u.UserId == creatorId))
                .Select(c => new
                {
                    ChatId = c.Id,
                    Users = c.Users.Select(u => u.UserId).ToList(),
                })
                .Where(c => c.Users.Count() == addedUserIds.Count())
                .ToList();

            foreach (var creatorChat in creatorChats)
            {
                int count = 0;

                foreach (var userId in addedUserIds)
                {
                    if (creatorChat.Users.Contains(userId))
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (count == addedUserIds.Count())
                {
                    throw new ArgumentException($"There already exist chat with this users. Chat: {this.GetNameBy(creatorChat.ChatId, creatorId)}");
                }
            }
        }

        public async Task UpdateAsync(EditChatInputModel inputModel)
        {
            Chat chat = this.chatRepository.All().FirstOrDefault(c => c.Id == inputModel.Id);

            if (!(inputModel.Icon is null))
            {
                chat.IconRemoteUrl = await this.AttachFile(inputModel.Icon);
            }

            chat.Name = inputModel.Name;

            await this.chatRepository.SaveChangesAsync();
        }

        public async Task<string> AttachFile(IFormFile file)
        {
            string extension = System.IO.Path.GetExtension(file.FileName);
            string fileName = $"File_{DateTime.UtcNow.ToString("yyyy/dd/mm/ss")}" + extension;

            string remoteUrl = await this.cloudinaryService.UploadFile(file, fileName, extension);

            return remoteUrl;
        }

        public T GetBy<T>(int chatId)
        {
            return this.chatUserRepository
                .All()
                .Where(c => c.ChatId == chatId)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task AddUsersToChat(AddUsersToChatInputModel inputModel)
        {
            if (!inputModel.UsersId.Any())
            {
                return;
            }

            Chat chat = this.chatRepository
                .All()
                .FirstOrDefault(c => c.Id == inputModel.ChatId);

            ICollection<string> users = this.GetUsersByChat<UserViewModel>(inputModel.ChatId).Select(c => c.Id).ToList();
            foreach (var userId in inputModel.UsersId)
            {
                users.Add(userId);
            }

            this.CheckExistingChat(inputModel.UserId, users);

            foreach (var userId in inputModel.UsersId)
            {
                ChatUser chatuser = new ChatUser()
                {
                    ChatId = inputModel.ChatId,
                    UserId = userId,
                };
                chat.Users.Add(chatuser);
            }

            await this.chatRepository.SaveChangesAsync();
        }

        public IEnumerable<UserViewModel> GetAllUsersNotAddedBy(int chatId)
        {
            IEnumerable<string> usersChat = this.GetUsersByChat<UserViewModel>(chatId).Select(c => c.Id).AsEnumerable();
            ICollection<UserViewModel> notAddedUsers = new List<UserViewModel>();
            foreach (var user in this.userRepository.All().To<UserViewModel>())
            {
                if (!usersChat.Contains(user.Id))
                {
                    notAddedUsers.Add(user);
                }
            }

            return notAddedUsers;
        }

        public async Task RemoveUserFromChat(string userId, int chatId)
        {
            ChatUser user = this.chatUserRepository
                .All()
                .FirstOrDefault(cu => cu.UserId == userId && cu.ChatId == chatId);

            this.chatUserRepository.HardDelete(user);

            await this.chatUserRepository.SaveChangesAsync();
        }

        public string GetCreatorIdBy(int chatId)
        {
            return this.chatRepository
                .All()
                .FirstOrDefault(x => x.Id == chatId)
                .CreatorId;
        }

        public T GetByCurrentUser<T>(string userId, int chatId)
        {
            return this.chatUserRepository
                .All()
                .Where(cu => cu.UserId == userId && cu.ChatId == chatId)
                .To<T>()
                .FirstOrDefault();
        }

        public string GetIconUrl(string userId, int chatId)
        {
            string iconUrl = this.chatUserRepository
                .All()
                .Where(c => c.ChatId == chatId && c.UserId != userId && c.Chat.ChatType != ChatType.GroupChat)
                .Select(c => c.User.ProfileImageUrl)
                .FirstOrDefault();

            return iconUrl;
        }
    }
}
