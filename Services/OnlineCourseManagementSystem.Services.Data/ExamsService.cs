namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Exams;

    public class ExamsService : IExamsService
    {
        private readonly IDeletableEntityRepository<Exam> examsRepository;
        private readonly IDeletableEntityRepository<Answer> answersRepository;
        private readonly IDeletableEntityRepository<Question> questionsRepository;
        private readonly IDeletableEntityRepository<UserExam> userExamsRepository;
        private readonly IDeletableEntityRepository<Lecture> lecturesRepository;
        private readonly IDeletableEntityRepository<Choice> choicesRepository;
        private readonly IDeletableEntityRepository<Completition> completitionsRepository;
        private readonly IDeletableEntityRepository<Certificate> certificationsRepository;

        public ExamsService(
            IDeletableEntityRepository<Exam> examsRepository,
            IDeletableEntityRepository<Answer> answersRepository,
            IDeletableEntityRepository<Question> questionsRepository,
            IDeletableEntityRepository<UserExam> userExamsRepository,
            IDeletableEntityRepository<Lecture> lecturesRepository,
            IDeletableEntityRepository<Choice> choicesRepository,
            IDeletableEntityRepository<Completition> completitionsRepository,
            IDeletableEntityRepository<Certificate> certificationsRepository)
        {
            this.examsRepository = examsRepository;
            this.answersRepository = answersRepository;
            this.questionsRepository = questionsRepository;
            this.userExamsRepository = userExamsRepository;
            this.lecturesRepository = lecturesRepository;
            this.choicesRepository = choicesRepository;
            this.completitionsRepository = completitionsRepository;
            this.certificationsRepository = certificationsRepository;
        }

        public async Task AddExamToCertificateAsync(AddExamToCertificateInputModel input)
        {
            Exam exam = this.examsRepository.All().FirstOrDefault(e => e.Id == input.ExamId);
            exam.IsCertificated = true;
            await this.examsRepository.SaveChangesAsync();
        }

        public async Task AddExamToLectureAsync(int lectureId, AddExamToLectureInputModel input)
        {
            Exam exam = this.examsRepository.All().FirstOrDefault(e => e.Id == input.ExamId);
            exam.LectureId = lectureId;
            await this.examsRepository.SaveChangesAsync();
        }

        public bool CanStartCertificate(int courseId, string userId)
        {
            int lecturesCount = this.lecturesRepository
                .All()
                .Count(l => l.CourseId == courseId);

            int completedLecturesCount = this.completitionsRepository
                .All()
                .Count(c => c.Lecture.CourseId == courseId && c.UserId == userId);

            return lecturesCount == completedLecturesCount;
        }

        public async Task CreateAsync(CreateExamInputModel input)
        {
            Exam exam = new Exam
            {
                Name = input.Name,
                CourseId = input.CourseId,
                LecturerId = input.LecturerId,
                StartDate = input.StartDate,
                Duration = input.Duration,
                CreatorId = input.CreatorId,
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

        public IEnumerable<T> GetAllByAdmin<T>()
        {
            return this.examsRepository
                .All()
                .OrderByDescending(c => c.CreatedOn)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllByCurrentUserId<T>(string userId)
        {
            return this.userExamsRepository
                .All()
                .Where(ue => ue.UserId == userId)
                .OrderByDescending(ue => ue.CreatedOn)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllByLectureId<T>(int lectureId)
        {
            return this.examsRepository
                .All()
                .Where(e => e.LectureId == lectureId)
                .OrderByDescending(e => e.CreatedOn)
                .To<T>()
                .ToList();
        }

        // Todo: Add check if exams are active when switching to production environment
        public IEnumerable<T> GetAllByUserId<T>(string userId)
        {
            return this.examsRepository
                .All()
                .Where(e => e.Course.Users.Any(u => u.UserId == userId) && !e.Users.Any(u => u.UserId == userId))
                .OrderByDescending(e => e.StartDate)
                .To<T>()
                .ToList();
        }

        public IEnumerable<SelectListItem> GetAllExamsAsSelectListItemsByCreatorId(string creatorId)
        {
            return this.examsRepository
                .All()
                .Where(e => e.CreatorId == creatorId)
                .OrderByDescending(e => e.CreatedOn)
                .Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString(),
                })
                .ToList();
        }

        public T GetByExamIdAndUserId<T>(string userId, int examId)
        {
            return this.userExamsRepository
                .All()
                .Where(ue => ue.ExamId == examId && ue.UserId == userId)
                .To<T>()
                .FirstOrDefault();
        }

        public T GetById<T>(int id)
        {
            return this.examsRepository
                .All()
                .Where(e => e.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public int GetCertificatedExamIdByCourse(int courseId)
        {
            return this.examsRepository
                .All()
                .FirstOrDefault(e => e.CourseId == courseId && e.IsCertificated.Value) == null
                ? this.examsRepository
                .All()
                .FirstOrDefault(e => e.CourseId == courseId && e.IsCertificated.Value).Id
                : 0;
        }

        public int GetCountOfAllUsersWhoPassedCertainExam(int examId)
        {
            return this.userExamsRepository
                .All()
                .Count(ue => ue.ExamId == examId);
        }

        public double GetCountOfUsersWithLowerGradesOnCertainExam(int examId, double grade)
        {
            return this.userExamsRepository
                .All()
                .Count(ue => ue.ExamId == examId && ue.Grade < grade);
        }

        public int GetCourseIdByExam(int examId)
        {
            return this.examsRepository
                .All()
                .FirstOrDefault(e => e.Id == examId)
                .CourseId
                .HasValue ? this.examsRepository
                .All()
                .FirstOrDefault(e => e.Id == examId)
                .CourseId
                .Value : -1;
        }

        public string GetCourseNameById(int id)
        {
            return this.examsRepository
                .All()
                .FirstOrDefault()
                .Course.Name;
        }

        public int GetDurationById(int id)
        {
            return this.examsRepository
                .All()
                .FirstOrDefault(e => e.Id == id)
                .Duration;
        }

        public int GetExamIdByUserIdAndCourseId(string userId, int courseId)
        {
            return this.userExamsRepository
                .All()
                .FirstOrDefault(ue => ue.UserId == userId && ue.Exam.CourseId == courseId)
                .ExamId;
        }

        public double GetGradeByUserIdAndCourseId(string userId, int courseId)
        {
            return this.userExamsRepository
                .All()
                .FirstOrDefault(ue => ue.UserId == userId && ue.Exam.CourseId == courseId)
                .Grade;
        }

        public string GetNameById(int id)
        {
            return this.examsRepository
                .All()
                .FirstOrDefault(e => e.Id == id)
                .Name;
        }

        public int GetPointsByUserIdAndExamId(string userId, int examId)
        {
            return this.answersRepository
                .All()
                .Where(a => a.Question.ExamId == examId && a.UserId == userId)
                .Sum(a => a.Question.Points);
        }

        public DateTime GetStartDateById(int id)
        {
            return this.examsRepository
                .All()
                .FirstOrDefault(e => e.Id == id)
                .StartDate;
        }

        public bool HasUserMadeCertainExam(int examId, string userId)
        {
            return this.userExamsRepository
                .All()
                .Any(ue => ue.ExamId == examId && ue.UserId == userId);
        }

        public bool IsExamAddedToLecture(int examId, int lectureId)
        {
            Exam exam = this.examsRepository.All().FirstOrDefault(e => e.Id == examId);

            return exam.LectureId == lectureId ? true : false;
        }

        public bool IsExamCertificated(int examId)
        {
            Exam exam = this.examsRepository.All().FirstOrDefault(e => e.Id == examId);
            return exam.IsCertificated.HasValue ? exam.IsCertificated.Value : false;
        }

        public async Task MarkAsSeenAsync(int id)
        {
            UserExam userExam = this.userExamsRepository.All().FirstOrDefault(ue => ue.Id == id);
            userExam.SeenOn = DateTime.UtcNow;
            await this.userExamsRepository.SaveChangesAsync();
        }

        public async Task SaveAnswerAsync(string userId, IFormCollection formCollection)
        {
            int[] questionIds = formCollection["questionId"].Select(int.Parse).ToArray();

            foreach (var questionId in questionIds)
            {
                int choiceIdCorrect = this.choicesRepository
                    .All()
                    .FirstOrDefault(c => c.QuestionId == questionId && c.IsCorrect)
                    .Id;

                string id = formCollection["question_" + questionId];

                int choiceMadeId = (id == null) ? 0 : int.Parse(id);

                if (choiceMadeId != 0)
                {
                    if (this.answersRepository.All().Any(a => a.QuestionId == questionId && a.UserId == userId))
                    {
                        Answer answer = this.answersRepository
                            .All()
                            .FirstOrDefault(a => a.QuestionId == questionId && a.UserId == userId);

                        answer.Text = this.choicesRepository.All().FirstOrDefault(c => c.Id == choiceMadeId).Text;
                        answer.IsCorrect = choiceIdCorrect == choiceMadeId;
                    }
                    else
                    {
                        Answer answer = new Answer
                        {
                            QuestionId = questionId,
                            UserId = userId,
                            Text = this.choicesRepository.All().FirstOrDefault(c => c.Id == choiceMadeId).Text,
                            IsCorrect = choiceIdCorrect == choiceMadeId,
                        };

                        await this.answersRepository.AddAsync(answer);
                    }
                }
            }

            await this.answersRepository.SaveChangesAsync();
        }

        public async Task<double> TakeExamAsync(int examId, string userId, IFormCollection formCollection)
        {
            double points = 0;
            int maxPoints = 0;

            int[] questionIds = formCollection["questionId"].Select(int.Parse).ToArray();

            foreach (var questionId in questionIds)
            {
                int choiceIdCorrect = this.choicesRepository
                    .All()
                    .FirstOrDefault(c => c.QuestionId == questionId && c.IsCorrect)
                    .Id;

                Question question = this.questionsRepository
                    .All()
                    .FirstOrDefault(q => q.Id == questionId);
                maxPoints += question.Points;

                string id = formCollection["question_" + questionId];

                int choiceMadeId = (id == null) ? 0 : int.Parse(id);

                if (choiceMadeId != 0)
                {
                    if (choiceMadeId == choiceIdCorrect)
                    {
                        points += question.Points;
                    }

                    Answer answer = new Answer
                    {
                        QuestionId = questionId,
                        UserId = userId,
                        Text = this.choicesRepository.All().FirstOrDefault(c => c.Id == choiceMadeId).Text,
                        IsCorrect = choiceIdCorrect == choiceMadeId,
                    };

                    await this.answersRepository.AddAsync(answer);
                }
            }

            await this.answersRepository.SaveChangesAsync();

            if (!this.userExamsRepository.All().Any(ue => ue.UserId == userId && ue.ExamId == examId))
            {
                double grade = (double)((points * 6) / maxPoints);
                UserExam userExam = new UserExam
                {
                    UserId = userId,
                    ExamId = examId,
                    Grade = grade,
                    Status = Status.Graded,
                };

                Exam exam = this.examsRepository.All().FirstOrDefault(e => e.Id == userExam.ExamId);
                if (exam.LectureId != null)
                {
                    Completition completition = new Completition
                    {
                        UserId = userId,
                        LectureId = exam.LectureId.Value,
                    };

                    await this.completitionsRepository.AddAsync(completition);
                    await this.completitionsRepository.SaveChangesAsync();
                }

                if (exam.IsCertificated.Value)
                {
                    Certificate certificate = new Certificate
                    {
                        UserId = userId,
                        CourseId = exam.CourseId.Value,
                        Grade = grade,
                    };

                    await this.certificationsRepository.AddAsync(certificate);
                    await this.certificationsRepository.SaveChangesAsync();
                }

                await this.userExamsRepository.AddAsync(userExam);
                await this.userExamsRepository.SaveChangesAsync();
            }

            return points;
        }

        public async Task UpdateAsync(EditExamInputModel input)
        {
            Exam exam = this.examsRepository.All().FirstOrDefault(e => e.Id == input.Id);

            exam.Name = input.Name;
            exam.Description = input.Description;
            exam.CourseId = input.CourseId;
            exam.StartDate = input.StartDate;

            await this.examsRepository.SaveChangesAsync();
        }
    }
}
