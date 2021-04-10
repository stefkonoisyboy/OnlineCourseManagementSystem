namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;

    public class LecturesService : ILecturesService
    {
        private readonly IDeletableEntityRepository<Lecture> lecturesRepository;
        private readonly IDeletableEntityRepository<File> filesRepository;
        private readonly Cloudinary cloudinary;

        public LecturesService(
            IDeletableEntityRepository<Lecture> lecturesRepository,
            IDeletableEntityRepository<File> filesRepository,
            Cloudinary cloudinary)
        {
            this.lecturesRepository = lecturesRepository;
            this.filesRepository = filesRepository;
            this.cloudinary = cloudinary;
        }

        public async Task AddResourceAsync(AddFileToLectureInputModel input)
        {
            Lecture lecture = this.lecturesRepository.All().FirstOrDefault(l => l.Id == input.LectureId);

            string fileName = lecture.Title + Guid.NewGuid().ToString();
            string remoteUrl = await this.UploadImageAsync(input.File, fileName);

            File file = new File
            {
                Extension = System.IO.Path.GetExtension(input.File.FileName),
                RemoteUrl = remoteUrl,
                LectureId = input.LectureId,
                UserId = input.UserId,
            };

            await this.filesRepository.AddAsync(file);

            await this.lecturesRepository.SaveChangesAsync();
            await this.filesRepository.SaveChangesAsync();
        }

        public async Task CreateAsync(CreateLectureInputModel input)
        {
            Lecture lecture = new Lecture
            {
                Title = input.Title,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                CourseId = input.CourseId,
            };

            await this.lecturesRepository.AddAsync(lecture);
            await this.lecturesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Lecture lecture = this.lecturesRepository.All().FirstOrDefault(l => l.Id == id);
            this.lecturesRepository.Delete(lecture);
            await this.lecturesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllById<T>(int id)
        {
            return this.lecturesRepository
                .All()
                .Where(l => l.CourseId == id)
                .OrderByDescending(l => l.StartDate)
                .To<T>()
                .ToList();
        }

        public async Task UpdateAsync(EditLectureInputModel input)
        {
            Lecture lecture = this.lecturesRepository.All().FirstOrDefault(l => l.Id == input.Id);

            lecture.Title = input.Title;
            lecture.StartDate = input.StartDate;
            lecture.EndDate = input.EndDate;

            await this.lecturesRepository.SaveChangesAsync();
        }

        private async Task<string> UploadImageAsync(IFormFile formFile, string fileName)
        {
            byte[] destinationData;

            using (var ms = new System.IO.MemoryStream())
            {
                await formFile.CopyToAsync(ms);
                destinationData = ms.ToArray();
            }

            UploadResult result = null;

            using (var ms = new System.IO.MemoryStream(destinationData))
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    Folder = "courses-files",
                    File = new FileDescription(fileName, ms),
                };

                result = this.cloudinary.Upload(uploadParams);
            }

            return result?.SecureUrl.AbsoluteUri;
        }
    }
}
