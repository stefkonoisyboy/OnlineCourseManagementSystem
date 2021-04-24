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
        private readonly Cloudinary cloudinaryUtility;
        private readonly CloudinaryService cloudinaryService;

        public AssignmentsService(IDeletableEntityRepository<Assignment> assignmentRepository, IDeletableEntityRepository<UserAssignment> userAssignmentRepository, Cloudinary cloudinaryUtility)
        {
            this.assignmentRepository = assignmentRepository;
            this.userAssignmentRepository = userAssignmentRepository;
            this.cloudinaryUtility = cloudinaryUtility;

            this.cloudinaryService = new CloudinaryService(cloudinaryUtility);
        }

        public IEnumerable<T> GetAllFinishedBy<T>(string userId)
        {
            var assignments = this.userAssignmentRepository
                .AllWithDeleted()
                .Where(x => x.TurnedOn != null && x.Points != null)
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

        public T GetById<T>(int assignmentId)
        {
            return this.assignmentRepository.All()
                    .Where(a => a.Id == assignmentId)
                    .To<T>()
                    .FirstOrDefault();
        }

        public IEnumerable<T> GetAllUsersForAssignment<T>(int assignmentId)
        {
            return this.userAssignmentRepository.All()
                .Where(a => a.AssignmentId == assignmentId && a.Points == null)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllBy<T>(int courseId)
        {

            return this.userAssignmentRepository
                .All()
                .Where(x => x.Points == null)
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
            Assignment assignment = this.assignmentRepository.All().FirstOrDefault(x => x.Id == inputModel.Id);

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

            Assignment assignment = this.assignmentRepository.All().FirstOrDefault(a => a.Id == userAssignment.AssignmentId);
            await this.userAssignmentRepository.SaveChangesAsync();

            return assignment.CourseId;
        }
    }
}
