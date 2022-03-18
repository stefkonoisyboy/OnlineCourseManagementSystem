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
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data.MachineLearning;
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
        private readonly IDeletableEntityRepository<Completition> completitionsRepository;
        private readonly IDeletableEntityRepository<Skill> skillsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly Cloudinary cloudinary;

        public CoursesService(
            IDeletableEntityRepository<Course> coursesRepository,
            IDeletableEntityRepository<CourseTag> courseTagsRepository,
            IDeletableEntityRepository<File> filesRepository,
            IDeletableEntityRepository<CourseLecturer> courseLecturersRepository,
            IDeletableEntityRepository<UserCourse> userCoursesRepository,
            IDeletableEntityRepository<Completition> completitionsRepository,
            IDeletableEntityRepository<Skill> skillsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            Cloudinary cloudinary)
        {
            this.coursesRepository = coursesRepository;
            this.courseTagsRepository = courseTagsRepository;
            this.filesRepository = filesRepository;
            this.courseLecturersRepository = courseLecturersRepository;
            this.userCoursesRepository = userCoursesRepository;
            this.completitionsRepository = completitionsRepository;
            this.skillsRepository = skillsRepository;
            this.usersRepository = usersRepository;
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
                Description = new HtmlSanitizer().Sanitize(input.Description),
                CreatorId = input.CreatorId,
                MachineLearningId = this.coursesRepository.All().OrderBy(c => c.MachineLearningId).LastOrDefault().MachineLearningId + 1,
            };

            await this.coursesRepository.AddAsync(course);
            await this.coursesRepository.SaveChangesAsync();
        }

        public async Task CreateMetaAsync(CreateMetaInputModel input)
        {
            Course course = this.coursesRepository.All().FirstOrDefault(c => c.Id == input.CourseId);

            if (this.skillsRepository.All().Count(s => s.CourseId == course.Id) > 0)
            {
                throw new ArgumentException("Cannot edit already created course! Only admin has this permission!");
            }

            course.Price = input.Price;
            course.StartDate = input.StartDate;
            course.SubjectId = input.SubjectId;
            course.RecommendedDuration = input.RecommendedDuration;
            course.TrailerRemoteUrl = input.TrailerRemoteUrl;

            await this.coursesRepository.SaveChangesAsync();

            string[] skills = input.Skills.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            foreach (var skill in skills)
            {
                Skill skillDb = new Skill
                {
                    Text = skill,
                    CourseId = course.Id,
                };

                await this.skillsRepository.AddAsync(skillDb);
            }

            await this.skillsRepository.SaveChangesAsync();

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

        public async Task EnrollAsync(int courseId, string userId, string path)
        {
            if (this.userCoursesRepository.All().Any(uc => uc.UserId == userId && uc.CourseId == courseId))
            {
                return;
            }

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
                .OrderByDescending(c => c.StartDate)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllActive<T>(int page, string name, int itemsPerPage = 5)
        {
            var query = this.coursesRepository.All().AsQueryable();

            if (name != null)
            {
                query = query.Where(q => q.Name.Contains(name) || q.Tags.Any(t => t.Tag.Name.Contains(name)));
            }

            query = query
                .OrderByDescending(q => q.StartDate)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

            return query.To<T>().ToList();
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

        public IEnumerable<T> GetAllActiveBySubjectId<T>(int page, int subjectId, int itemsPerPage = 6)
        {
            return this.coursesRepository
                .All()
                .Where(c => c.SubjectId == subjectId)
                .OrderByDescending(q => q.StartDate)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public int GetAllActiveCoursesBySubjectIdCount(int subjectId)
        {
            return this.coursesRepository
                .All()
                .Count(c => c.SubjectId == subjectId);
        }

        public int GetAllActiveCoursesCount(string name)
        {
            if (name != null)
            {
                return this.coursesRepository
               .All()
               .Count(c => (c.Name.Contains(name) || c.Tags.Any(t => t.Tag.Name.Contains(name))));
            }

            return this.coursesRepository
                .All()
                .Count();
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

        public IEnumerable<SelectListItem> GetAllAsSelectListItemsByCreatorId(string creatorId)
        {
            return this.coursesRepository
                .All()
                .Where(c => c.CreatorId == creatorId)
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                })
                .ToList();
        }

        public IEnumerable<T> GetAllByAdmin<T>()
        {
            return this.coursesRepository
                .All()
                .OrderByDescending(c => c.CreatedOn)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllByCreatorId<T>(int id, string creatorId, int itemsPerPage = 6)
        {
            return this.coursesRepository
               .All()
               .OrderByDescending(c => c.StartDate)
               .Where(c => c.CreatorId == creatorId)
               .Skip((id - 1) * itemsPerPage).Take(itemsPerPage)
               .To<T>()
               .ToList();
        }

        public IEnumerable<T> GetAllByCreatorIdAndSubjectId<T>(int id, string creatorId, int subjectId, int itemsPerPage = 6)
        {
            return this.coursesRepository
              .All()
              .OrderByDescending(c => c.StartDate)
              .Where(c => c.CreatorId == creatorId && c.SubjectId == subjectId)
              .Skip((id - 1) * itemsPerPage).Take(itemsPerPage)
              .To<T>()
              .ToList();
        }

        public IEnumerable<T> GetAllByNameOrTag<T>(SearchByCourseNameOrTagInputModel input)
        {
            var query = this.coursesRepository.All().AsQueryable();

            if (input.Text != null)
            {
                query = query.Where(c => c.Name.Contains(input.Text) || c.Tags.Any(t => t.Tag.Name.Contains(input.Text)));
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByTag<T>(SearchByTagInputModel input)
        {
            return this.coursesRepository
                .All()
                .OrderByDescending(c => c.StartDate)
                .Where(c => c.Tags.Any(t => t.Tag.Name == input.Name))
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllByUser<T>(int id, string userId, int itemsPerPage = 6)
        {
            return this.coursesRepository
                .All()
                .OrderByDescending(c => c.StartDate)
                .Where(c => c.Users.Any(u => u.UserId == userId))
                .Skip((id - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllByUserAndSubject<T>(int id, string userId, int subjectId, int itemsPerPage = 6)
        {
            return this.coursesRepository
               .All()
               .OrderByDescending(c => c.StartDate)
               .Where(c => c.Users.Any(u => u.UserId == userId) && c.SubjectId == subjectId)
               .Skip((id - 1) * itemsPerPage).Take(itemsPerPage)
               .To<T>()
               .ToList();
        }

        public IEnumerable<T> GetAllCompletedByUserId<T>(string userId)
        {
            return this.coursesRepository
                .All()
                .Where(c => c.Users.Any(u => u.UserId == userId) && c.Lectures.Count() > 0 && c.Lectures.Count() == this.completitionsRepository.All().Count(comp => comp.UserId == userId && comp.Lecture.CourseId == c.Id))
                .To<T>()
                .ToList();
        }

        public int GetAllCoursesByCreatorIdAndSubjectIdCount(int subjectId, string creatorId)
        {
            return this.coursesRepository
               .All()
               .Count(c => c.CreatorId == creatorId && c.SubjectId == subjectId);
        }

        public int GetAllCoursesByCreatorIdCount(string creatorId)
        {
            return this.coursesRepository
                .All()
                .Count(c => c.CreatorId == creatorId);
        }

        public int GetAllCoursesByUserIdAndSubjectIdCount(int subjectId, string userId)
        {
            return this.userCoursesRepository
                .All()
                .Count(uc => uc.UserId == userId && uc.Course.SubjectId == subjectId);
        }

        public int GetAllCoursesByUserIdCount(string userId)
        {
            return this.userCoursesRepository
                .All()
                .Count(uc => uc.UserId == userId);
        }

        public IEnumerable<T> GetAllFollewedByUserId<T>(string userId)
        {
            return this.coursesRepository
                .All()
                .Where(c => c.Users.Any(u => u.UserId == userId))
                .To<T>()
                .ToList();
        }

        public IEnumerable<UserInCourse> GetAllForTestingAIByUserId(int id, string userId, int itemsPerPage = 12)
        {
            return this.coursesRepository
                .All()
                .Where(c => !c.Users.Any(u => u.UserId == userId) && c.MachineLearningId != 0)
                .Select(c => new UserInCourse
                {
                    UserId = this.usersRepository.All().FirstOrDefault(u => u.Id == userId).MachineLearningId,
                    CourseId = c.MachineLearningId,
                    CourseName = c.Name,
                    CoursePrice = c.Price.ToString("f2"),
                    ParticipantsCount = c.Users.Count(),
                    StartDate = c.StartDate.ToString("MMMM yyyy"),
                    Id = c.Id,
                })
                .ToList();
        }

        public IEnumerable<T> GetAllPast<T>()
        {
            return this.coursesRepository
                .All()
                .OrderByDescending(c => c.StartDate)
                .Where(c => c.EndDate < DateTime.UtcNow)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllRecommended<T>()
        {
            return this.coursesRepository
                .All()
                .OrderByDescending(c => c.Reviews.Average(r => r.Rating))
                .ThenByDescending(c => c.StartDate)
                .Take(10)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllUnapproved<T>()
        {
            return this.coursesRepository
                .All()
                .OrderByDescending(c => c.StartDate)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllUpcoming<T>()
        {
            return this.coursesRepository
                .All()
                .OrderByDescending(c => c.StartDate)
                .Where(c => c.StartDate > DateTime.UtcNow)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllCurrentYearBySubjectId<T>(int subjectId)
        {
            return this.coursesRepository
                .All()
                .Where(c => c.StartDate.Year == DateTime.UtcNow.Year && c.Id == subjectId)
                .OrderBy(c => c.StartDate)
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

        public IEnumerable<T> GetBySubjectAndCourseName<T>(int subjectId, string name)
        {
            return this.coursesRepository
                .All()
                .Where(c => c.SubjectId == subjectId && c.Name.Contains(name))
                .OrderByDescending(c => c.StartDate)
                .Skip(1)
                .To<T>()
                .ToList();
        }

        public int GetCourseIdByFileId(int fileId)
        {
            return this.filesRepository
                .All()
                .FirstOrDefault(f => f.Id == fileId)
                .Lecture.CourseId;
        }

        public T GetLastActiveCourseBySubjectId<T>(int subjectId)
        {
            return this.coursesRepository
                .All()
                .Where(c => c.SubjectId == subjectId)
                .OrderByDescending(c => c.StartDate)
                .To<T>()
                .FirstOrDefault();
        }

        public IEnumerable<T> GetTopLatest<T>()
        {
            return this.coursesRepository
                .All()
                .OrderByDescending(c => c.StartDate)
                .Take(4)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetTopNext<T>()
        {
            return this.coursesRepository
                .All()
                .Where(c => c.StartDate > DateTime.UtcNow)
                .OrderByDescending(c => c.StartDate)
                .Take(4)
                .To<T>()
                .ToList();
        }

        public bool IsCourseAvailable(int courseId, string userId)
        {
            return !this.userCoursesRepository
                .All()
                .Any(uc => uc.UserId == userId && uc.CourseId == courseId);
        }

        public async Task UpdateAsync(EditCourseInputModel input)
        {
            Course course = this.coursesRepository.All().FirstOrDefault(c => c.Id == input.Id);

            course.Name = input.Name;
            course.Description = new HtmlSanitizer().Sanitize(input.Description);

            await this.coursesRepository.SaveChangesAsync();
        }

        public async Task UpdateMetaAsync(EditMetaInputModel input)
        {
            Course course = this.coursesRepository.All().FirstOrDefault(c => c.Id == input.Id);

            course.Price = input.Price;
            course.SubjectId = input.SubjectId;
            course.StartDate = input.StartDate;
            course.RecommendedDuration = input.RecommendedDuration;

            await this.coursesRepository.SaveChangesAsync();

            if (input.Image != null)
            {
                string fileName = course.Name + Guid.NewGuid().ToString();
                string remoteUrl = await this.UploadImageAsync(input.Image, fileName);

                File file = this.filesRepository.All().FirstOrDefault(f => f.Id == course.FileId);

                if (file == null)
                {
                    File newFile = new File
                    {
                        Extension = System.IO.Path.GetExtension(input.Image.FileName),
                        RemoteUrl = remoteUrl,
                        CourseId = course.Id,
                    };

                    await this.filesRepository.AddAsync(newFile);
                    await this.filesRepository.SaveChangesAsync();

                    course.FileId = newFile.Id;
                }
                else
                {
                    file.RemoteUrl = remoteUrl;
                    await this.filesRepository.SaveChangesAsync();
                }

                await this.coursesRepository.SaveChangesAsync();
            }
        }

        public async Task UpdateModifiedOnById(int id)
        {
            Course course = this.coursesRepository.All().FirstOrDefault(c => c.Id == id);
            course.ModifiedOn = DateTime.UtcNow;
            await this.coursesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllUnactiveCourses<T>(int subjectId)
        {
            return this.coursesRepository
                .All()
                .Where(c => c.SubjectId == subjectId)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllTopLatestBySubjectInModule<T>(int moduleId)
        {
            List<int?> subjectIds = this.coursesRepository.All().Where(c => c.Subject.ModuleId == moduleId).Select(c => c.SubjectId).ToList();
            List<Course> courses = new List<Course>();
            foreach (var subjectId in subjectIds)
            {
                courses.Add(this.coursesRepository.All().OrderByDescending(c => c.StartDate).FirstOrDefault(c => c.SubjectId == subjectId));
            }

            return courses
                .AsQueryable()
                .To<T>()
                .ToList();
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
