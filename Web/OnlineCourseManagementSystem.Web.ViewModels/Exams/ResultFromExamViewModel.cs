namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Answers;
    using OnlineCourseManagementSystem.Web.ViewModels.Questions;

    public class ResultFromExamViewModel : IMapFrom<UserExam>
    {
        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public int ExamId { get; set; }

        public string ExamName { get; set; }

        public bool? ExamIsCertificated { get; set; }

        public int ExamCourseId { get; set; }

        public string ExamCourseName { get; set; }

        public int ExamDuration { get; set; }

        public double Grade { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ExamStartDate { get; set; }

        public DateTime ExamEndDate { get; set; }

        public DateTime? SeenOn { get; set; }

        public DateTime FinishedOn { get; set; }

        public Status Status { get; set; }

        public IEnumerable<AllQuestionsByExamViewModel> Questions { get; set; }

        public IEnumerable<AllAnswersByExamIdAndUserIdViewModel> Answers { get; set; }

        [IgnoreMap]
        public double SuccessRate => (double)(this.CorrectAnswers / this.TotalAnswers) * 100;

        [IgnoreMap]
        public int PointsEarned => this.EarnPoints();

        [IgnoreMap]
        public int TotalPoints => this.Questions.Sum(q => q.Points);

        [IgnoreMap]
        public int TimeSpent => this.CalculateTimeSpent();

        [IgnoreMap]
        public double TotalAnswers => this.Questions.Count();

        [IgnoreMap]
        public double CorrectAnswers => this.Answers.Count(a => a.IsCorrect);

        [IgnoreMap]
        public double CompareRateInPercents { get; set; }

        private int CalculateTimeSpent()
        {
            TimeSpan difference = this.FinishedOn - this.ExamStartDate;
            int spentTime = (int)difference.TotalMinutes;
            return spentTime;
        }

        private int EarnPoints()
        {
            int earnedPoints = 0;

            foreach (var answer in this.Answers)
            {
                if (answer.IsCorrect)
                {
                    earnedPoints += this.Questions.FirstOrDefault(q => q.Id == answer.QuestionId).Points;
                }
            }

            return earnedPoints;
        }
    }
}
