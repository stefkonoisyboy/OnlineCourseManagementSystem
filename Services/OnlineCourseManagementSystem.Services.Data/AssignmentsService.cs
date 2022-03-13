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
    using OnlineCourseManagementSystem.Data.Models.Enumerations;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Assignments;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

    public class AssignmentsService : IAssignmentsService
    {
        private readonly IDeletableEntityRepository<Assignment> assignmentRepository;
        private readonly IDeletableEntityRepository<UserAssignment> userAssignmentRepository;
        private readonly IDeletableEntityRepository<Course> courseRepository;
        private readonly CloudinaryService cloudinaryService;

        public AssignmentsService(IDeletableEntityRepository<Assignment> assignmentRepository, IDeletableEntityRepository<UserAssignment> userAssignmentRepository, IDeletableEntityRepository<Course> courseRepository, Cloudinary cloudinaryUtility)
        {
            this.assignmentRepository = assignmentRepository;
            this.userAssignmentRepository = userAssignmentRepository;
            this.courseRepository = courseRepository;
            this.cloudinaryService = new CloudinaryService(cloudinaryUtility);
        }

        public IEnumerable<T> GetAllFinishedBy<T>(string userId)
        {
            var assignments = this.userAssignmentRepository
                .All()
                .Where(x => x.IsChecked && x.UserId == userId)
                .OrderByDescending(x => x.TurnedOn)
                .To<T>().ToArray();

            return assignments;
        }

        public async Task CreateAsync(CreateAssignmentInputModel inputModel)
        {
            Assignment assignment = new Assignment
            {
                Title = inputModel.Title,
                Instructions = inputModel.Instructions,
                CourseId = inputModel.CourseId,
                StartDate = inputModel.StartDate,
                EndDate = inputModel.EndDate,
            };

            if (inputModel.PossiblePoints != 0)
            {
                assignment.PossiblePoints = inputModel.PossiblePoints;
            }

            if (inputModel.Files != null)
            {
               await this.AttachFile(assignment, inputModel.Files, FileType.Resource);
            }

            foreach (var id in inputModel.StudentsId)
            {
                await this.userAssignmentRepository.AddAsync(new UserAssignment
                {
                    Assignment = assignment,
                    UserId = id,
                    Seen = false,
                });
            }

            await this.userAssignmentRepository.SaveChangesAsync();
        }

        public T GetById<T>(int assignmentId, string userId)
        {
            return this.userAssignmentRepository
                    .All()
                    .Where(ua => ua.AssignmentId == assignmentId && ua.UserId == userId)
                    .To<T>()
                    .FirstOrDefault();
        }

        public IEnumerable<T> GetAllUsersForAssignment<T>(int assignmentId)
        {
            return this.userAssignmentRepository.All()
                .Where(a => a.AssignmentId == assignmentId)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllBy<T>(int courseId)
        {
            return this.userAssignmentRepository
                .All()
                .Where(x => x.Assignment.CourseId == courseId)
                .To<T>()
                .Distinct()
                .ToList();
        }

        public IEnumerable<T> GetAllBy<T>(string userId)
        {
            return this.userAssignmentRepository
                .All()
                .Where(x => x.UserId == userId && x.User.Roles
                        .FirstOrDefault().RoleId
                        .EndsWith("Student") && x.TurnedOn == null)
                .OrderByDescending(x => x.Assignment.StartDate)
                .To<T>()
                .ToList();
        }

        public async Task UpdateAsync(EditAssignmentInputModel inputModel)
        {
            Assignment assignment = this.assignmentRepository.All().FirstOrDefault(x => x.Id == inputModel.AssignmentId);

            assignment.Title = inputModel.Title;
            assignment.StartDate = inputModel.StartDate;
            assignment.EndDate = inputModel.EndDate;
            assignment.Instructions = inputModel.Instructions;
            assignment.PossiblePoints = inputModel.PossiblePoints;

            await this.assignmentRepository.SaveChangesAsync();
            await this.userAssignmentRepository.SaveChangesAsync();
        }

        public async Task<int> DeleteAssignment(int assignmetId)
        {
            Assignment assignment = this.assignmentRepository.All().FirstOrDefault(x => x.Id == assignmetId);
            int courseId = assignment.CourseId;
            this.assignmentRepository.Delete(assignment);
            await this.assignmentRepository.SaveChangesAsync();

            return courseId;
        }

        public async Task AttachFile(Assignment assignment, IEnumerable<IFormFile> files, FileType fileType, string userId = null)
        {
            foreach (var file in files)
            {
                string extension = System.IO.Path.GetExtension(file.FileName);
                string fileName = $"File_{DateTime.UtcNow.ToString("yyyy/dd/mm/ss")}" + extension;

                string remoteUrl = await this.cloudinaryService.UploadFile(file, fileName, extension);
                assignment.Files
                    .Add(new File
                    {
                        UserId = userId,
                        Extension = extension,
                        RemoteUrl = remoteUrl,
                        Type = fileType,
                    });
            }
        }

        public async Task MarkAsSeen(int assignmentId, string userId)
        {
            UserAssignment userAssignment = this.userAssignmentRepository
                .All()
                .FirstOrDefault(ua => ua.AssignmentId == assignmentId && ua.UserId == userId);
            userAssignment.Seen = true;

            await this.userAssignmentRepository.SaveChangesAsync();
        }

        public async Task<int> TurnIn(FilesToAssignmentInputModel inputModel)
        {
            Assignment assignment = this.assignmentRepository.All().FirstOrDefault(a => a.Id == inputModel.AssignmentId);

            UserAssignment userAssignment = this.userAssignmentRepository
                .All()
                .FirstOrDefault(ua => ua.AssignmentId == inputModel.AssignmentId && ua.UserId == inputModel.UserId);
            userAssignment.TurnedOn = DateTime.UtcNow;

            if (inputModel.Files != null)
            {
                await this.AttachFile(assignment, inputModel.Files, FileType.Submit, inputModel.UserId);
            }

            await this.assignmentRepository.SaveChangesAsync();
            await this.userAssignmentRepository.SaveChangesAsync();

            return assignment.Id;
        }

        public async Task<int> MarkSubmittedAssignment(MarkSubmittedAssignmentInputModel inputModel)
        {
            UserAssignment userAssignment = this.userAssignmentRepository
                .All()
                .Where(ua => ua.AssignmentId == inputModel.AssignmentId && ua.UserId == inputModel.UserId)
                .FirstOrDefault();

            userAssignment.Points = inputModel.Points;
            userAssignment.Feedback = inputModel.Feedback;
            userAssignment.IsChecked = true;

            Assignment assignment = this.assignmentRepository.All().FirstOrDefault(a => a.Id == userAssignment.AssignmentId);
            if (inputModel.Points > assignment.PossiblePoints)
            {
                throw new ArgumentException("Points must be less than the stated!");
            }

            await this.userAssignmentRepository.SaveChangesAsync();

            return assignment.CourseId;
        }

        public async Task UndoTurnIn(int assignmentId, string userId)
        {
            UserAssignment userAssignment = this.userAssignmentRepository
                .All()
                .FirstOrDefault(ua => ua.AssignmentId == assignmentId && ua.UserId == userId);

            userAssignment.TurnedOn = null;

            await this.userAssignmentRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllCheckedBy<T>(int courseId)
        {
            return this.assignmentRepository
                .All()
                .Where(a => a.Users.Count(ua => ua.IsChecked) == a.Users.Count && a.CourseId == courseId)
                .To<T>()
                .Distinct()
                .ToList();
        }

        public async Task UpdateCheckedAsync(EditCheckedAssignmentInputModel inputModel)
        {
            UserAssignment userAssignment = this.userAssignmentRepository
                .All()
                .FirstOrDefault(ua => ua.AssignmentId == inputModel.AssignmentId && ua.UserId == inputModel.UserId);

            userAssignment.Points = inputModel.Points;
            userAssignment.Feedback = inputModel.Feedback;

            Assignment assignment = this.assignmentRepository.All().FirstOrDefault(a => a.Id == inputModel.AssignmentId);
            if (assignment.PossiblePoints < inputModel.Points)
            {
                throw new ArgumentException("Points must be less than the stated!");
            }

            await this.userAssignmentRepository.SaveChangesAsync();
        }

        public T GetCheckedBy<T>(int assignmentId, string userId)
        {
            return this.userAssignmentRepository
                .All()
                .Where(ua => ua.AssignmentId == assignmentId && ua.UserId == userId && ua.IsChecked)
                .To<T>()
                .FirstOrDefault();
        }

        public IEnumerable<T> GetAllCheckedUserAssignments<T>(int assignmentId)
        {
            return this.userAssignmentRepository
                .All()
                .Where(ua => ua.IsChecked && ua.AssignmentId == assignmentId)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int assignmentId)
        {
            return this.assignmentRepository
                .All()
                .Where(a => a.Id == assignmentId)
                .To<T>()
                .FirstOrDefault();
        }

        public IEnumerable<T> GetAllByAdmin<T>()
        {
            return this.assignmentRepository
                .All()
                .OrderByDescending(c => c.CreatedOn)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllByCourseAndUser<T>(int courseId, string userId)
        {
            return this.userAssignmentRepository
                .All()
                .Where(ua => ua.UserId == userId && ua.Assignment.CourseId == courseId && ua.TurnedOn == null)
                .OrderByDescending(ua => ua.CreatedOn)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllFinishedByCourseAndUser<T>(int courseId, string userId)
        {
            return this.userAssignmentRepository
                .All()
                .Where(ua => ua.UserId == userId && ua.Assignment.CourseId == courseId && ua.IsChecked && ua.TurnedOn != null)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllByLecturer<T>(string userId)
        {
            return this.userAssignmentRepository
                .All()
                .Where(ua => ua.Assignment.Course.CreatorId == userId)
                .To<T>()
                .ToList();
        }
    }
}
