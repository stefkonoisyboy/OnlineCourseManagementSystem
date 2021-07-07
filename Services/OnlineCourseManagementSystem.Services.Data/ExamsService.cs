namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
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
        private readonly IDeletableEntityRepository<Choice> choicesRepository;

        public ExamsService(
            IDeletableEntityRepository<Exam> examsRepository,
            IDeletableEntityRepository<Answer> answersRepository,
            IDeletableEntityRepository<Question> questionsRepository,
            IDeletableEntityRepository<UserExam> userExamsRepository,
            IDeletableEntityRepository<Choice> choicesRepository)
        {
            this.examsRepository = examsRepository;
            this.answersRepository = answersRepository;
            this.questionsRepository = questionsRepository;
            this.userExamsRepository = userExamsRepository;
            this.choicesRepository = choicesRepository;
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

        public IEnumerable<T> GetAllByCurrentUserId<T>(string userId)
        {
            return this.userExamsRepository
                .All()
                .Where(ue => ue.UserId == userId)
                .OrderByDescending(ue => ue.CreatedOn)
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
