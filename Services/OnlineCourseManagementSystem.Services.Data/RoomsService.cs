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
    using OnlineCourseManagementSystem.Web.ViewModels.VideoConferences;

    public class RoomsService : IRoomsService
    {
        private readonly IDeletableEntityRepository<Room> roomsRepository;
        private readonly IDeletableEntityRepository<RoomParticipant> roomParticipantsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public RoomsService(
            IDeletableEntityRepository<Room> roomsRepository,
            IDeletableEntityRepository<RoomParticipant> roomParticipantsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.roomsRepository = roomsRepository;
            this.roomParticipantsRepository = roomParticipantsRepository;
            this.usersRepository = usersRepository;
        }

        public async Task<string> AddParticipantsToRoom(string roomId, string userIdentityName, bool isCreator = false)
        {
            Room room = this.roomsRepository.All().FirstOrDefault(r => r.Id == roomId);

            RoomParticipant participant = new RoomParticipant()
            {
                Name = userIdentityName,
                RoomId = roomId,
                IsCreator = isCreator,
                IsGivenRightToDisplayCamera = room.IsDisplayCameraAllowed,
                IsGivenRightToUnmuteMic = room.IsUnmutedMicAllowed,
                IsGivenRightToShareScreen = room.IsShareScreenAllowed,
            };

            await this.roomParticipantsRepository.AddAsync(participant);
            await this.roomParticipantsRepository.SaveChangesAsync();

            return participant.Id;
        }

        public async Task<string> CreateRoomAsync(CreateRoomInputModel inputModel)
        {
            Room room = new Room()
            {
                Name = inputModel.Name,
                CreatorId = inputModel.CreatorId,
                IsDisplayCameraAllowed = inputModel.IsDisplayCameraAllowed,
                IsUnmutedMicAllowed = inputModel.IsUnmutedMicAllowed,
                IsShareScreenAllowed = inputModel.IsShareScreenAllowed,
            };

            ApplicationUser creator = this.usersRepository.All().FirstOrDefault(u => u.Id == inputModel.CreatorId);

            await this.roomsRepository.AddAsync(room);
            await this.roomsRepository.SaveChangesAsync();

            return room.Id;
        }

        public async Task DontGiveRightToDisplayCamera(string participantId)
        {
            RoomParticipant participant = this.roomParticipantsRepository.All().FirstOrDefault(rp => rp.Id == participantId);
            participant.IsGivenRightToDisplayCamera = false;

            await this.roomParticipantsRepository.SaveChangesAsync();
        }

        public async Task DontGiveRightToShareScreen(string participantId)
        {
            RoomParticipant participant = this.roomParticipantsRepository.All().FirstOrDefault(rp => rp.Id == participantId);
            participant.IsGivenRightToShareScreen = false;

            await this.roomParticipantsRepository.SaveChangesAsync();
        }

        public async Task DontGiveRightToUnMuteMic(string participantId)
        {
            RoomParticipant participant = this.roomParticipantsRepository.All().FirstOrDefault(rp => rp.Id == participantId);
            participant.IsGivenRightToUnmuteMic = false;

            await this.roomParticipantsRepository.SaveChangesAsync();
        }

        public T GetRoomById<T>(string roomId)
        {
            return this.roomsRepository
                .All()
                .Where(r => r.Id == roomId)
                .To<T>()
                .FirstOrDefault();
        }

        public T GetRoomParticipant<T>(string participantId)
        {
            return this.roomParticipantsRepository
                .All()
                .Where(rp => rp.Id == participantId)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task GiveRightToDisplayCamera(string participantId)
        {
            RoomParticipant participant = this.roomParticipantsRepository.All().FirstOrDefault(rp => rp.Id == participantId);
            participant.IsGivenRightToDisplayCamera = true;

            await this.roomParticipantsRepository.SaveChangesAsync();
        }

        public async Task GiveRightToShareScreen(string participantId)
        {
            RoomParticipant participant = this.roomParticipantsRepository.All().FirstOrDefault(rp => rp.Id == participantId);
            participant.IsGivenRightToShareScreen = true;

            await this.roomParticipantsRepository.SaveChangesAsync();
        }

        public async Task GiveRightToUnMuteMic(string participantId)
        {
            RoomParticipant participant = this.roomParticipantsRepository.All().FirstOrDefault(rp => rp.Id == participantId);
            participant.IsGivenRightToUnmuteMic = true;

            await this.roomParticipantsRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateRoomInputModel inputModel)
        {
            Room room = this.roomsRepository.All().FirstOrDefault(r => r.Id == inputModel.Id);

            room.IsDisplayCameraAllowed = inputModel.IsDisplayCameraAllowed;
            room.IsShareScreenAllowed = inputModel.IsShareScreenAllowed;
            room.IsUnmutedMicAllowed = inputModel.IsUnmuteMicAllowed;

            await this.roomsRepository.SaveChangesAsync();
        }
    }
}
