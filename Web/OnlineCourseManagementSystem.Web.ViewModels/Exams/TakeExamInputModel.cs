namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Answers;
    using OnlineCourseManagementSystem.Web.ViewModels.Paging;
    using OnlineCourseManagementSystem.Web.ViewModels.Questions;

    public class TakeExamInputModel
    {
        public int ExamId { get; set; }

        public int Duration { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string CourseName { get; set; }

        public DateTime StartDate { get; set; }

        public int Scored { get; set; }

        public IEnumerable<AllQuestionsByExamViewModel> Questions { get; set; }

        public IEnumerable<AllAnswersByExamIdAndUserIdViewModel> Answers { get; set; }
    }
}
