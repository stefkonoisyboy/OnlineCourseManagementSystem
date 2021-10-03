﻿namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Course> courseRepository;
        private readonly CloudinaryService cloudinaryService;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Course> courseRepository,
            Cloudinary cloudinary)
        {
            this.usersRepository = usersRepository;
            this.courseRepository = courseRepository;
            this.cloudinaryService = new CloudinaryService(cloudinary);
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.usersRepository
                .All()
                .OrderBy(u => u.FirstName + ' ' + u.LastName)
                .ThenBy(u => u.UserName)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(string id)
        {
            return this.usersRepository
                .All()
                .Where(u => u.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        //public T GetCourseCreatorById<T>(int courseId)
        //{
        //    var creator = this.courseRepository.All().FirstOrDefault(c => c.Id == courseId).Creator;
        //    return this.
        //}

        public string GetFullNameById(string id)
        {
            return this.usersRepository
                .All()
                .FirstOrDefault(u => u.Id == id)
                .FirstName + ' ' + this.usersRepository
                .All()
                .FirstOrDefault(u => u.Id == id)
                .LastName;
        }

        public string GetProfileImageUrlById(string id)
        {
            return this.usersRepository
               .All()
               .FirstOrDefault(u => u.Id == id)
               .ProfileImageUrl;
        }

        public async Task UpdateAsync(ManageAccountInputModel inputModel)
        {
            ApplicationUser user = this.usersRepository.All().FirstOrDefault(u => u.Id == inputModel.Id);

            if (!(inputModel.NewImage is null))
            {
                string extension = Path.GetExtension(inputModel.NewImage.FileName);
                user.ProfileImageUrl = await this.cloudinaryService.UploadFile(inputModel.NewImage, inputModel.NewImage.FileName, extension, "Profile_Images");
            }

            user.FirstName = inputModel.FirstName;
            user.LastName = inputModel.LastName;
            user.BirthDate = inputModel.Birthdate;
            user.Background = inputModel.Background;
            user.TownId = inputModel.TownId;
            user.Address = inputModel.Address;
            user.Email = inputModel.Email;

            this.usersRepository.Update(user);
            await this.usersRepository.SaveChangesAsync();
        }
    }
}
