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
    using OnlineCourseManagementSystem.Web.ViewModels.Albums;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

    public class AlbumsService : IAlbumsService
    {
        private readonly IDeletableEntityRepository<Album> albumRepository;

        public AlbumsService(IDeletableEntityRepository<Album> albumRepository)
        {
            this.albumRepository = albumRepository;
        }

        public async Task CreateAsync(BaseAlbumInputModel albumInputModel)
        {
            if (this.albumRepository.All().Any(x => x.Name == albumInputModel.Name))
            {
                throw new ArgumentException("There is already existing album with this name");
            }

            Album album = new Album
            {
                Name = albumInputModel.Name,
                UserId = albumInputModel.UserId,
            };

            await this.albumRepository.AddAsync(album);

            await this.albumRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(string userId, int albumId, string name)
        {
            Album album = this.albumRepository.All().FirstOrDefault(x => x.Id == albumId);

            album.Name = name;

            await this.albumRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllById<T>(string userId)
        {
            return this.albumRepository.All().Where(x => x.UserId == userId).To<T>().ToArray();
        }
    }
}
