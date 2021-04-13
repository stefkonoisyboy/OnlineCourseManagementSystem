namespace OnlineCourseManagementSystem.Services.Data
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Assignments;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AssignmentsService : IAssignmentsService
    {
        private readonly IDeletableEntityRepository<Assignment> assignmentRepository;
        private readonly IDeletableEntityRepository<UserAssignment> userAssignmentRepository;
        private readonly Cloudinary cloudinaryUtility;

        public AssignmentsService(IDeletableEntityRepository<Assignment> assignmentRepository, IDeletableEntityRepository<UserAssignment> userAssignmentRepository, Cloudinary cloudinaryUtility)
        {
            this.assignmentRepository = assignmentRepository;
            this.userAssignmentRepository = userAssignmentRepository;
            this.cloudinaryUtility = cloudinaryUtility;
        }

        public IEnumerable<T> GetAllFinishedBy<T>(string userId)
        {
            var assignments = this.userAssignmentRepository
                .AllWithDeleted()
                .Where(x => x.TurnedOn != null)
                .To<T>().ToArray();

            return assignments;

        }

        public async Task CreateAsync(CreateAssignmentInputModel inputModel)
        {
            Assignment assignment = new Assignment
            {
                Instructions = inputModel.Instructions,
                CourseId = inputModel.CourseId,
                StartDate = inputModel.StartDate,
                EndDate = inputModel.EndDate,
            };

            if (inputModel.PossiblePoints != 0)
            {
                assignment.PossiblePoints = inputModel.PossiblePoints;
            }

            if (inputModel.Files.Count() != 0)
            {
               await this.AttachFile(assignment, inputModel.Files);
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

        public void DeleteAssignment(int assignmetId)
        {
            Assignment assignment = this.assignmentRepository.All().FirstOrDefault(x => x.Id == assignmetId);
            this.assignmentRepository.Delete(assignment);
        }

        public IEnumerable<T> GetAllBy<T>(int courseId)
        {
            return this.assignmentRepository
                .All()
                .Where(x => x.CourseId == courseId)
                .To<T>()
                .ToArray();
        }

        public IEnumerable<T> GetAllBy<T>(string userId)
        {
            return this.userAssignmentRepository
                .All()
                .Where(x => x.UserId == userId && x.User.Roles.FirstOrDefault().RoleId.EndsWith("Student"))
                .To<T>()
                .ToArray();
        }

        //TODO:
        //public Task UpdateAsync()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task AttachFile(Assignment assignment, IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
            {
                string extension = file.ContentType;
                string fileName = $"File_IMG{DateTime.UtcNow.ToString("yyyy/dd/mm/ss")}";
                byte[] destinationData;
                using (var ms = new System.IO.MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    destinationData = ms.ToArray();
                }

                UploadResult uploadResult = null;

                using (var ms = new System.IO.MemoryStream(destinationData))
                {
                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        Folder = "assignments",
                        File = new FileDescription(fileName, ms),
                    };

                    uploadResult = this.cloudinaryUtility.Upload(uploadParams);
                }

                string remoteUrl = uploadResult?.SecureUrl.AbsoluteUri;
                assignment.Files.Add( new File
                {
                    Extension = extension,
                    RemoteUrl = remoteUrl,
                });
            }

        }

        public T GetBy<T>(int assingmentId)
        {
            var assignmentPageViewModel = this.assignmentRepository
                .All()
                .Where(x => x.Id == assingmentId)
                .To<T>()
                .ToList().FirstOrDefault();

            return assignmentPageViewModel;
        }

        public async Task MarkAsSeen(int assignmentId)
        {
            UserAssignment userAssignment = this.userAssignmentRepository.All().FirstOrDefault(a => a.AssignmentId == assignmentId);
            userAssignment.Seen = true;

            this.userAssignmentRepository.Update(userAssignment);

            await this.userAssignmentRepository.SaveChangesAsync();
        }
    }
}
