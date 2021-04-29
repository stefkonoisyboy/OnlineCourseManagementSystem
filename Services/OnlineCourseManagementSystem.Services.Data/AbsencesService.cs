namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Absences;

    public class AbsencesService : IAbsencesService
    {
        private readonly IDeletableEntityRepository<Absence> absencesRepository;

        public AbsencesService(IDeletableEntityRepository<Absence> absencesRepository)
        {
            this.absencesRepository = absencesRepository;
        }

        public async Task CreateAsync(CreateAbsenceListInputModel input)
        {
            for (int i = 0; i < input.Students.Count(); i++)
            {
                Absence absence = new Absence
                {
                    CourseId = input.CourseId,
                    LectureId = input.LectureId,
                    StudentId = input.Students.ToList()[i].Id,
                    Type = input.Inputs[i].Type,
                    Reason = input.Inputs[i].Reason,
                };

                if (!this.absencesRepository.All().Any(a => a.CourseId == absence.CourseId && a.LectureId == absence.LectureId && a.StudentId == absence.StudentId))
                {
                    await this.absencesRepository.AddAsync(absence);
                }
            }

            await this.absencesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsyncForSpecificLecture(int courseId, int lectureId)
        {
            IEnumerable<Absence> absences = this.absencesRepository
                .All()
                .Where(a => a.CourseId == courseId && a.LectureId == lectureId)
                .ToList();

            foreach (var absence in absences)
            {
                this.absencesRepository.Delete(absence);
            }

            await this.absencesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsyncForSpecificStudent(int courseId, int lectureId, string studentId)
        {
            Absence absence = this.absencesRepository
                .All()
                .FirstOrDefault(a => a.CourseId == courseId && a.LectureId == lectureId && a.StudentId == studentId);

            this.absencesRepository.Delete(absence);
            await this.absencesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByCourseAndLecture<T>(int courseId, int lectureId)
        {
            return this.absencesRepository
                .All()
                .Where(a => a.CourseId == courseId && a.LectureId == lectureId)
                .OrderBy(a => a.Id)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllByStudentInSpecifiedRange<T>(GetAllByStudentInIntervalInputModel input)
        {
            return this.absencesRepository
                .All()
                .Where(a => a.CourseId == input.CourseId && a.StudentId == input.StudentId && a.Lecture.StartDate >= input.StartDate && a.Lecture.StartDate <= input.EndDate)
                .OrderBy(a => a.Id)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.absencesRepository
                .All()
                .Where(a => a.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task UpdateAsync(EditAbsenceInputModel input)
        {
            Absence absence = this.absencesRepository
                .All()
                .FirstOrDefault(a => a.Id == input.Id);

            absence.Reason = input.Reason;
            absence.Type = input.Type;

            await this.absencesRepository.SaveChangesAsync();
        }

        public async Task UpdateMultipleAsync(IEnumerable<EditAbsenceInputModel> input)
        {
            foreach (var absence in input)
            {
                Absence dbAbsence = this.absencesRepository
                .All()
                .FirstOrDefault(a => a.Id == absence.Id);

                dbAbsence.Reason = absence.Reason;
                dbAbsence.Type = absence.Type;
            }

            await this.absencesRepository.SaveChangesAsync();
        }
    }
}
