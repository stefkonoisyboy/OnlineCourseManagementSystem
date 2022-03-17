namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Ganss.XSS;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Questions;

    public class QuestionsService : IQuestionsService
    {
        private readonly IDeletableEntityRepository<Question> questionsRepository;
        private readonly IDeletableEntityRepository<Choice> choicesRepository;

        public QuestionsService(IDeletableEntityRepository<Question> questionsRepository, IDeletableEntityRepository<Choice> choicesRepository)
        {
            this.questionsRepository = questionsRepository;
            this.choicesRepository = choicesRepository;
        }

        public async Task CreateAsync(CreateQuestionInputModel input)
        {
            Question question = new Question
            {
                Text = new HtmlSanitizer().Sanitize(input.Text),
                Points = input.Points,
                ExamId = input.ExamId,
                CorrectAnswerOption = input.CorrectAnswerOption,
            };

            if (input.Choices.Count(c => c.Text != null) < 2)
            {
                throw new ArgumentException("You should create at least 2 choices for this question!");
            }

            if (input.Choices.Count(c => c.Text != null) == 2 && (input.CorrectAnswerOption == "C" || input.CorrectAnswerOption == "D"))
            {
                throw new ArgumentException("You don't have enough count of choices created to mark this option as correct answer for the question!");
            }

            if (input.Choices.Count(c => c.Text != null) == 3 && input.CorrectAnswerOption == "D")
            {
                throw new ArgumentException("You don't have enough count of choices created to mark this option as correct answer for the question!");
            }

            if (string.IsNullOrWhiteSpace(input.CorrectAnswerOption))
            {
                throw new ArgumentException("There should be a correct option!");
            }

            await this.questionsRepository.AddAsync(question);
            await this.questionsRepository.SaveChangesAsync();

            foreach (var choice in input.Choices.Where(c => c.Text != null))
            {
                Choice dbChoice = new Choice
                {
                    Text = choice.Text,
                    QuestionId = question.Id,
                };

                await this.choicesRepository.AddAsync(dbChoice);
            }

            if (input.CorrectAnswerOption == "A")
            {
                Choice choice = question.Choices.FirstOrDefault();
                choice.IsCorrect = true;
            }
            else if (input.CorrectAnswerOption == "B")
            {
                Choice choice = question.Choices.Skip(1).FirstOrDefault();
                choice.IsCorrect = true;
            }
            else if (input.CorrectAnswerOption == "C")
            {
                Choice choice = question.Choices.Skip(2).FirstOrDefault();
                choice.IsCorrect = true;
            }
            else
            {
                Choice choice = question.Choices.Skip(3).FirstOrDefault();
                choice.IsCorrect = true;
            }

            await this.questionsRepository.SaveChangesAsync();
            await this.choicesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Question question = this.questionsRepository.All().FirstOrDefault(q => q.Id == id);
            this.questionsRepository.Delete(question);
            await this.questionsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByExam<T>(int examId, int page, string input, int itemsPerPage)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return this.questionsRepository
                .All()
                .Where(q => q.ExamId == examId)
                .OrderBy(q => q.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();
            }

            return this.questionsRepository
                .All()
                .Where(q => q.ExamId == examId && q.Text.ToLower().Contains(input.ToLower()))
                .OrderBy(q => q.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.questionsRepository
                .All()
                .Where(q => q.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public int GetCountByExamId(int examId, string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return this.questionsRepository
                .All()
                .Count(q => q.ExamId == examId);
            }

            return this.questionsRepository
                .All()
                .Count(q => q.ExamId == examId && q.Text.ToLower().Contains(input.ToLower()));
        }

        public async Task UpdateAsync(EditQuestionInputModel input)
        {
            Question question = this.questionsRepository.All().FirstOrDefault(q => q.Id == input.Id);

            if (input.Choices.Count(c => c.Text != null) < 2)
            {
                throw new ArgumentException("You should create at least 2 choices for this question!");
            }

            if (input.Choices.Count(c => c.Text != null) == 2 && (input.CorrectAnswerOption == "C" || input.CorrectAnswerOption == "D"))
            {
                throw new ArgumentException("You don't have enough count of choices created to mark this option as correct answer for the question!");
            }

            if (input.Choices.Count(c => c.Text != null) == 3 && input.CorrectAnswerOption == "D")
            {
                throw new ArgumentException("You don't have enough count of choices created to mark this option as correct answer for the question!");
            }

            if (string.IsNullOrWhiteSpace(input.CorrectAnswerOption))
            {
                throw new ArgumentException("There should be a correct option!");
            }

            question.Text = new HtmlSanitizer().Sanitize(input.Text);
            question.Points = input.Points;
            question.CorrectAnswerOption = input.CorrectAnswerOption;

            Choice oldChoice = this.choicesRepository.All().FirstOrDefault(c => c.QuestionId == question.Id && c.IsCorrect);
            oldChoice.IsCorrect = false;

            foreach (var choice in input.Choices.Where(c => c.Text != null))
            {
                Choice dbChoice = this.choicesRepository.All().FirstOrDefault(c => c.Id == choice.Id);
                dbChoice.Text = choice.Text;
            }

            if (input.CorrectAnswerOption == "A")
            {
                Choice choice = this.choicesRepository.All().Where(c => c.QuestionId == question.Id).FirstOrDefault();
                choice.IsCorrect = true;
            }
            else if (input.CorrectAnswerOption == "B")
            {
                Choice choice = this.choicesRepository.All().Where(c => c.QuestionId == question.Id).Skip(1).FirstOrDefault();
                choice.IsCorrect = true;
            }
            else if (input.CorrectAnswerOption == "C")
            {
                Choice choice = this.choicesRepository.All().Where(c => c.QuestionId == question.Id).Skip(2).FirstOrDefault();
                choice.IsCorrect = true;
            }
            else
            {
                Choice choice = this.choicesRepository.All().Where(c => c.QuestionId == question.Id).Skip(3).FirstOrDefault();
                choice.IsCorrect = true;
            }

            await this.questionsRepository.SaveChangesAsync();
            await this.choicesRepository.SaveChangesAsync();
        }
    }
}
