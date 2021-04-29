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
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Tags;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> coursesRepository;
        private readonly IDeletableEntityRepository<CourseTag> courseTagsRepository;
        private readonly IDeletableEntityRepository<File> filesRepository;
        private readonly IDeletableEntityRepository<CourseLecturer> courseLecturersRepository;
        private readonly IDeletableEntityRepository<UserCourse> userCoursesRepository;
        private readonly Cloudinary cloudinary;

        public CoursesService(
            IDeletableEntityRepository<Course> coursesRepository,
            IDeletableEntityRepository<CourseTag> courseTagsRepository,
            IDeletableEntityRepository<File> filesRepository,
            IDeletableEntityRepository<CourseLecturer> courseLecturersRepository,
            IDeletableEntityRepository<UserCourse> userCoursesRepository,
            Cloudinary cloudinary)
        {
            this.coursesRepository = coursesRepository;
            this.courseTagsRepository = courseTagsRepository;
            this.filesRepository = filesRepository;
            this.courseLecturersRepository = courseLecturersRepository;
            this.userCoursesRepository = userCoursesRepository;
            this.cloudinary = cloudinary;
        }

        public async Task ApproveAsync(int courseId)
        {
            Course course = this.coursesRepository.All().FirstOrDefault(c => c.Id == courseId);
            course.IsApproved = true;
            await this.coursesRepository.SaveChangesAsync();
        }

        public string CourseNameByStudentAndCourse(string studentId, int courseId)
        {
            return this.coursesRepository
                .All()
                .FirstOrDefault(c => c.Users.Any(u => u.User.StudentId == studentId) && c.Id == courseId)
                .Name;
        }

        public async Task CreateAsync(CreateCourseInputModel input)
        {
            Course course = new Course
            {
                Name = input.Name,
                Description = input.Description,
                Price = input.Price,
                SubjectId = input.SubjectId,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
            };

            string fileName = course.Name + Guid.NewGuid().ToString();
            string remoteUrl = await this.UploadImageAsync(input.Image, fileName);

            File file = new File
            {
                Extension = System.IO.Path.GetExtension(input.Image.FileName),
                RemoteUrl = remoteUrl,
                CourseId = course.Id,
                UserId = input.UserId,
            };

            await this.filesRepository.AddAsync(file);
            await this.filesRepository.SaveChangesAsync();

            course.FileId = file.Id;
            await this.coursesRepository.AddAsync(course);
            await this.coursesRepository.SaveChangesAsync();

            foreach (var tagId in input.Tags)
            {
                CourseTag courseTag = new CourseTag
                {
                    CourseId = course.Id,
                    TagId = tagId,
                };

                await this.courseTagsRepository.AddAsync(courseTag);
            }

            await this.courseTagsRepository.SaveChangesAsync();

            foreach (var lecturerId in input.Lecturers)
            {
                CourseLecturer courseLecturer = new CourseLecturer
                {
                    CourseId = course.Id,
                    LecturerId = lecturerId,
                };

                await this.courseLecturersRepository.AddAsync(courseLecturer);
            }

            await this.courseLecturersRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Course course = this.coursesRepository.All().FirstOrDefault(c => c.Id == id);
            this.coursesRepository.Delete(course);
            await this.coursesRepository.SaveChangesAsync();
        }

        public async Task EnrollAsync(int courseId, string userId)
        {
            UserCourse userCourse = new UserCourse
            {
                CourseId = courseId,
                UserId = userId,
            };

            await this.userCoursesRepository.AddAsync(userCourse);
            await this.userCoursesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.coursesRepository
                .All()
                .Where(c => c.IsApproved.Value)
                .OrderByDescending(c => c.StartDate)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllActive<T>()
        {
            return this.coursesRepository
                .All()
                .OrderByDescending(c => c.StartDate)
                .Where(c => c.StartDate <= DateTime.UtcNow && c.EndDate >= DateTime.UtcNow && c.IsApproved.Value)
                .To<T>()
                .ToList();
        }

        public IEnumerable<SelectListItem> GetAllActive()
        {
            return this.coursesRepository
                .All()
                .Where(x => x.StartDate >= DateTime.UtcNow && x.EndDate <= DateTime.UtcNow)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                });
        }

        public IEnumerable<SelectListItem> GetAllAsSelectListItems()
        {
            return this.coursesRepository
                .All()
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                })
                .ToList();
        }

        public IEnumerable<T> GetAllByTag<T>(SearchByTagInputModel input)
        {
            return this.coursesRepository
                .All()
                .OrderByDescending(c => c.StartDate)
                .Where(c => c.Tags.Any(t => t.Tag.Name == input.Name) && c.IsApproved.Value)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllByUser<T>(string userId)
        {
            return this.userCoursesRepository
                .All()
                .OrderByDescending(c => c.Course.StartDate)
                .Where(c => c.UserId == userId && c.Course.IsApproved.Value)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllPast<T>()
        {
            return this.coursesRepository
                .All()
                .OrderByDescending(c => c.StartDate)
                .Where(c => c.EndDate < DateTime.UtcNow && c.IsApproved.Value)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllUnapproved<T>()
        {
            return this.coursesRepository
                .All()
                .OrderByDescending(c => c.StartDate)
                .Where(c => c.IsApproved.HasValue == false)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllUpcoming<T>()
        {
            return this.coursesRepository
                .All()
                .OrderByDescending(c => c.StartDate)
                .Where(c => c.StartDate > DateTime.UtcNow && c.IsApproved.Value)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.coursesRepository
                .All()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task UpdateAsync(EditCourseInputModel input)
        {
            Course course = this.coursesRepository.All().FirstOrDefault(c => c.Id == input.Id);

            course.Name = input.Name;
            course.Description = input.Description;
            course.Price = input.Price;
            course.StartDate = input.StartDate;
            course.EndDate = input.EndDate;
            course.SubjectId = input.SubjectId;

            await this.coursesRepository.SaveChangesAsync();
            await this.courseTagsRepository.SaveChangesAsync();
            await this.courseLecturersRepository.SaveChangesAsync();
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
