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
    using OnlineCourseManagementSystem.Web.ViewModels.Exams;

    public class ExamsService : IExamsService
    {
        private readonly IDeletableEntityRepository<Exam> examsRepository;

        public ExamsService(IDeletableEntityRepository<Exam> examsRepository)
        {
            this.examsRepository = examsRepository;
        }

        public async Task CreateAsync(CreateExamInputModel input)
        {
            Exam exam = new Exam
            {
                Name = input.Name,
                CourseId = input.CourseId,
                LecturerId = input.LecturerId,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
            };

            await this.examsRepository.AddAsync(exam);
            await this.examsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Exam exam = this.examsRepository.All().FirstOrDefault(e => e.Id == id);
            this.examsRepository.Delete(exam);
            await this.examsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.examsRepository
                .All()
                .OrderByDescending(e => e.CreatedOn)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.examsRepository
                .All()
                .Where(e => e.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task UpdateAsync(EditExamInputModel input)
        {
            Exam exam = this.examsRepository.All().FirstOrDefault(e => e.Id == input.Id);

            exam.Name = input.Name;
            exam.Description = input.Description;
            exam.CourseId = input.CourseId;
            exam.StartDate = input.StartDate;
            exam.EndDate = input.EndDate;

            await this.examsRepository.SaveChangesAsync();
        }
    }
}
