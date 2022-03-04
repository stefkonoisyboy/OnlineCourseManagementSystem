namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Ganss.XSS;
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

        public async Task AddVideoAsync(AddVideoToLectureInputModel input)
        {
            Lecture lecture = this.lecturesRepository.All().FirstOrDefault(l => l.Id == input.LectureId);

            File file = new File
            {
                Extension = ".mp4",
                RemoteUrl = input.RemoteUrl,
                LectureId = input.LectureId,
                UserId = input.UserId,
            };

            await this.filesRepository.AddAsync(file);

            await this.lecturesRepository.SaveChangesAsync();
            await this.filesRepository.SaveChangesAsync();
        }

        public async Task AddPresentationFileAsync(AddPresentationToLectureInputModel input)
        {
            Lecture lecture = this.lecturesRepository.All().FirstOrDefault(l => l.Id == input.LectureId);

            string fileName = lecture.Title + Guid.NewGuid().ToString() + ".pptx";
            string remoteUrl = await this.UploadPresentationAsync(input.File, fileName);

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

        public async Task AddWordFileAsync(AddWordDocumentToLectureInputModel input)
        {
            Lecture lecture = this.lecturesRepository.All().FirstOrDefault(l => l.Id == input.LectureId);

            string fileName = lecture.Title + Guid.NewGuid().ToString() + ".docx";
            string remoteUrl = await this.UploadWordFileAsync(input.File, fileName);

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
                Description = new HtmlSanitizer().Sanitize(input.Description),
                CourseId = input.CourseId,
                CreatorId = input.CreatorId,
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

        public int GetCourseIdByLectureId(int id)
        {
            return this.lecturesRepository
                .All()
                .FirstOrDefault(l => l.Id == id)
                .CourseId;
        }

        public IEnumerable<T> GetAllById<T>(int courseId, int page, int itemsPerPage = 3)
        {
            return this.lecturesRepository
                .All()
                .Where(l => l.CourseId == courseId)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .OrderByDescending(l => l.StartDate)
                .To<T>()
                .ToList();
        }

        public async Task UpdateAsync(EditLectureInputModel input)
        {
            Lecture lecture = this.lecturesRepository.All().FirstOrDefault(l => l.Id == input.Id);

            lecture.Title = input.Title;
            lecture.Description = new HtmlSanitizer().Sanitize(input.Description);
            lecture.CourseId = input.CourseId;

            await this.lecturesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllVideosById<T>(int lectureId)
        {
            return this.filesRepository
                .All()
                .Where(f => f.LectureId == lectureId && f.Extension == ".mp4")
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllByCreatorId<T>(int page, string creatorId, int items = 3)
        {
            return this.lecturesRepository
                .All()
                .Where(l => l.CreatorId == creatorId && l.Course.CreatorId == creatorId)
                .Skip((page - 1) * items).Take(items)
                .OrderByDescending(l => l.CreatedOn)
                .To<T>()
                .ToList();
        }

        public int GetLecturesCountByCreatorId(string creatorId)
        {
            return this.lecturesRepository
                .All()
                .Count(l => l.CreatorId == creatorId);
        }

        public IEnumerable<T> GetAllInInterval<T>(DateTime startDate, DateTime endDate)
        {
            return this.lecturesRepository
                .All()
                .Where(l => l.StartDate >= startDate && l.EndDate <= endDate)
                .OrderBy(l => l.StartDate)
                .ThenBy(l => l.EndDate)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllByFileId<T>(int id)
        {
            return this.lecturesRepository
                .All()
                .Where(l => l.Files.Any(f => f.Id == id))
                .OrderBy(l => l.CreatedOn)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.lecturesRepository
                .All()
                .Where(l => l.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public IEnumerable<T> GetAllByAdmin<T>()
        {
            return this.lecturesRepository
                .All()
                .OrderByDescending(l => l.CreatedOn)
                .To<T>()
                .ToList();
        }

        public string GetNameById(int id)
        {
            return this.lecturesRepository
                .All()
                .FirstOrDefault(l => l.Id == id)
                .Title;
        }

        public async Task UpdateModifiedOnById(int id)
        {
            Lecture lecture = this.lecturesRepository.All().FirstOrDefault(l => l.Id == id);
            lecture.ModifiedOn = DateTime.UtcNow;
            await this.lecturesRepository.SaveChangesAsync();
        }

        public int GetLecturesCountById(int courseId)
        {
            return this.lecturesRepository
                .All()
                .Count(l => l.CourseId == courseId);
        }

        private async Task<string> UploadWordFileAsync(IFormFile formFile, string fileName)
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
                RawUploadParams uploadParams = new RawUploadParams
                {
                    Folder = "courses-files",
                    File = new FileDescription(fileName, ms),
                    PublicId = fileName + ".docx",
                };

                result = this.cloudinary.Upload(uploadParams);
            }

            return result?.SecureUrl.AbsoluteUri;
        }

        private async Task<string> UploadPresentationAsync(IFormFile formFile, string fileName)
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
                RawUploadParams uploadParams = new RawUploadParams
                {
                    Folder = "courses-files",
                    File = new FileDescription(fileName, ms),
                    PublicId = fileName + ".pptx",
                };

                result = this.cloudinary.Upload(uploadParams);
            }

            return result?.SecureUrl.AbsoluteUri;
        }
    }
}
