namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IDashboardService
    {
        /// <summary>
        /// Get average success by user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        double GetAverageSuccessByUserId(string userId);

        /// <summary>
        /// Get courses enrolled count by user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetCoursesEnrolledCountByUserId(string userId);

        /// <summary>
        /// Get finished assignments count by user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetFinishedAssignmentsCountByUserId(string userId);

        /// <summary>
        /// Get completed exams count by user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetCompletedExamsCountByUserId(string userId);

        /// <summary>
        /// Get events count.
        /// </summary>
        /// <returns></returns>
        int GetEventsCount();

        /// <summary>
        /// Get channels count by user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetChannelsCountByUserId(string userId);

        /// <summary>
        /// Get ranking for average success.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetRankingForAverageSuccess(string userId);

        /// <summary>
        /// Get ranking for enrolled courses.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetRankingForEnrolledCourses(string userId);

        /// <summary>
        /// Get ranking for finished assignments.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetRankingForFinishedAssignments(string userId);

        /// <summary>
        /// Get ranking for created courses.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        int GetRankingForCreatedCourses(string creatorId);

        /// <summary>
        /// Get ranking for created lectures.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        int GetRankingForCreatedLectures(string creatorId);

        /// <summary>
        /// Get ranking for created assignments.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        int GetRankingForCreatedAssignments(string creatorId);

        /// <summary>
        /// Get average grade by exam id and user id.
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        double GetAverageGradeByExamIdAndUserId(int examId, string userId);

        /// <summary>
        /// Get completed lectures by course id and user id.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetCompletedLecturesByCourseIdAndUserId(int courseId, string userId);

        /// <summary>
        /// Get completed assignments by course id and user id.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetCompletedAssignmentsByCourseIdAndUserId(int courseId, string userId);

        /// <summary>
        /// Get created courses count by creator id.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        int GetCreatedCoursesCountByCreatorId(string creatorId);

        /// <summary>
        /// Get created lectures count by creator id.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        int GetCreatedLecturesCountByCreatorId(string creatorId);

        /// <summary>
        /// Get created exams count by creator id.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        int GetCreatedExamsCountByCreatorId(string creatorId);

        /// <summary>
        /// Get created assignments count by creator id.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        int GetCreatedAssignmentsCountByCreatorId(string creatorId);

        /// <summary>
        /// Get created channels count by creator id.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        int GetCreatedChannelsCountByCreatorId(string creatorId);

        /// <summary>
        /// Get all courses count.
        /// </summary>
        /// <returns></returns>
        int GetAllCoursesCount();

        /// <summary>
        /// Get all students count.
        /// </summary>
        /// <returns></returns>
        int GetAllStudentsCount();

        /// <summary>
        /// Get all teachers count.
        /// </summary>
        /// <returns></returns>
        int GetAllTeachersCount();

        /// <summary>
        /// Get user info by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <returns></returns>
        T GetUserInfoById<T>(string userId);

        /// <summary>
        /// Get top 3 exams by creator id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetTop3ExamsByCreatorId<T>(string userId);

        /// <summary>
        /// Get top 3 students by completed lectures.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetTop3StudentsByCompletedLectures<T>();

        /// <summary>
        /// Get top 3 students by completed assignments.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetTop3StudentsByCompletedAssignments<T>();

        /// <summary>
        /// Get top 3 courses by user id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetTop3CoursesByUserId<T>(string userId);

        /// <summary>
        /// Get top 3 courses with most completed lectures by user id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetTop3CoursesWithMostCompletedLecturesByUserId<T>(string userId);

        /// <summary>
        /// Get top 3 courses with most completed assignments by user id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<T> GetTop3CoursesWithMostCompletedAssignmentsByUserId<T>(string userId);
    }
}
