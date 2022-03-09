namespace OnlineCourseManagementSystem.Web.ViewModels.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class DashboardViewModel
    {
        public int CourseCreated { get; set; }

        public int LecturesCreated { get; set; }

        public int AssignmentsCreated { get; set; }

        public int ExamsCreated { get; set; }

        public int ChannelsCreated { get; set; }

        public int RankingForCreatedCourses { get; set; }

        public int RankingForCreatedLectures { get; set; }

        public int RankingForCreatedAssignments { get; set; }

        public double AverageSuccess { get; set; }

        public int CoursesEnrolled { get; set; }

        public int Assignments { get; set; }

        public int Exams { get; set; }

        public int Events { get; set; }

        public int Channels { get; set; }

        public int RankingForAverageSuccess { get; set; }

        public int RankingForEnrolledCourses { get; set; }

        public int RankingForFinishedAssignments { get; set; }

        public DashboardUserViewModel User { get; set; }

        public IEnumerable<CoursesChartViewModel> ExamsChart { get; set; }

        public IEnumerable<CompletedLecturesByCourseViewModel> CompletedLectures { get; set; }

        public IEnumerable<CompletedAssignmentsByCourseViewModel> CompletedAssignments { get; set; }

        public IEnumerable<Top3ExamsByCreatorIdViewModel> TopExamsByCreator { get; set; }

        public IEnumerable<Top3StudentsByCompletedAssignmentsViewModel> TopStudentsByCompletedAssignments { get; set; }

        public IEnumerable<Top3StudentsByCompletedLecturesViewModel> TopStudentsByCompletedLectures { get; set; }
    }
}
