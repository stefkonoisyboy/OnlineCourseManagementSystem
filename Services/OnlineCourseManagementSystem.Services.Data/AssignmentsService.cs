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
    using OnlineCourseManagementSystem.Web.ViewModels.Assignments;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;

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

            if (inputModel.Files != null)
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

        public T GetById<T>(int assignmentId)
        {
            return this.assignmentRepository.All()
                    .Where(a => a.Id == assignmentId)
                    .To<T>()
                    .FirstOrDefault();
        }

        public IEnumerable<T> GetAllUserForAssignment<T>(int assignmentId)
        {
            return this.userAssignmentRepository.All()
                .Where(a => a.AssignmentId == assignmentId)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllBy<T>(int courseId)
        {
            return this.assignmentRepository
                .All()
                .Where(x => x.CourseId == courseId)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllBy<T>(string userId)
        {
            return this.userAssignmentRepository
                .All()
                .Where(x => x.UserId == userId && x.User.Roles.FirstOrDefault().RoleId.EndsWith("Student"))
                .To<T>()
                .ToList();
        }


        public async Task UpdateAsync(EditAssignmentInputModel inputModel)
        {
            Assignment assignment = this.assignmentRepository.All().FirstOrDefault(x => x.Id == inputModel.Id);

            assignment.StartDate = inputModel.StartDate;
            assignment.EndDate = inputModel.EndDate;
            //await this.AttachFile(assignment, inputModel.Files);
            assignment.Instructions = inputModel.Instructions;
            assignment.PossiblePoints = inputModel.PossiblePoints;

            //foreach (var studentId in inputModel.StudentsId)
            //{
            //    if (!this.userAssignmentRepository.All().Any(x => x.UserId == studentId && x.AssignmentId == inputModel.Id))
            //    {
            //        await this.userAssignmentRepository.AddAsync(
            //            new UserAssignment
            //        {
            //            Assignment = assignment,
            //            UserId = studentId,
            //        });
            //    }
            //    else
            //    {
            //        UserAssignment userAssignment = this.userAssignmentRepository.All().FirstOrDefault(x => x.UserId == studentId);
            //        userAssignment.Assignment = assignment;
            //    }
            //}

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

        public async Task AttachFile(Assignment assignment, IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
            {
                string extension = file.ContentType;
                string fileName = $"File_{DateTime.UtcNow.ToString("yyyy/dd/mm/ss")}";
                string remoteUrl = await this.UploadWordFileAsync(file, fileName);
                assignment.Files
                    .Add(new File
                    {
                        Extension = extension,
                        RemoteUrl = remoteUrl,
                    });
            }
        }

        public async Task MarkAsSeen(int assignmentId)
        {
            UserAssignment userAssignment = this.userAssignmentRepository.All().FirstOrDefault(a => a.AssignmentId == assignmentId);
            userAssignment.Seen = true;

            this.userAssignmentRepository.Update(userAssignment);

            await this.userAssignmentRepository.SaveChangesAsync();
        }

        public async Task<string> UploadWordFileAsync(IFormFile file, string fileName)
        {
            byte[] destinationData;
            using (var ms = new System.IO.MemoryStream())
            {
                await file.CopyToAsync(ms);
                destinationData = ms.ToArray();
            }

            UploadResult uploadResult = null;

            using (var ms = new System.IO.MemoryStream(destinationData))
            {
                RawUploadParams uploadParams = new RawUploadParams()
                {
                    Folder = "assignments",
                    File = new FileDescription(fileName, ms),
                    PublicId = $"{fileName}.docx",
                };

                uploadResult = this.cloudinaryUtility.Upload(uploadParams);
            }

            return uploadResult?.SecureUrl.AbsoluteUri;
        }
    }
}
