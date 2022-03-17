namespace OnlineCourseManagementSystem.Web.ViewModels.Paging
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Exams;

    public class PagingViewModel
    {
        public int PageNumber { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public bool HasNextPageLectures => this.PageNumber < this.LecturesCount;

        public bool HasNextPageLecturers => this.PageNumber < this.LecturersCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.ActiveCoursesCount / this.ItemsPerPage);

        public int LecturesCount => (int)Math.Ceiling((double)this.LecturesByCreatorIdCount / this.ItemsPerPage);

        public int LecturersCount => (int)Math.Ceiling((double)this.LecturerCoursesCount / this.ItemsPerPage);

        public int ActiveCoursesCount { get; set; }

        public int LecturerCoursesCount { get; set; }

        public int LecturesByCreatorIdCount { get; set; }

        public int ExamsCount { get; set; }

        public int ItemsPerPage { get; set; }

        public string Name { get; set; }

        public int SubjectId { get; set; }

        public int CourseId { get; set; }

        public int ExamId { get; set; }

        public string RecommendationType { get; set; }
    }
}
