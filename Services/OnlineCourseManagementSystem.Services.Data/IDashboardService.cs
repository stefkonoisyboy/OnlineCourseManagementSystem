using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface IDashboardService
    {
        double GetAverageSuccessByUserId(string userId);

        int GetCoursesEnrolledCountByUserId(string userId);

        int GetFinishedAssignmentsCountByUserId(string userId);

        int GetCompletedExamsCountByUserId(string userId);

        int GetEventsCount();

        int GetChannelsCountByUserId(string userId);

        int GetRankingForAverageSuccess(string userId);

        int GetRankingForEnrolledCourses(string userId);

        int GetRankingForFinishedAssignments(string userId);

        int GetRankingForCreatedCourses(string creatorId);

        int GetRankingForCreatedLectures(string creatorId);

        int GetRankingForCreatedAssignments(string creatorId);

        double GetAverageGradeByExamIdAndUserId(int examId, string userId);

        int GetCompletedLecturesByCourseIdAndUserId(int courseId, string userId);

        int GetCompletedAssignmentsByCourseIdAndUserId(int courseId, string userId);

        int GetCreatedCoursesCountByCreatorId(string creatorId);

        int GetCreatedLecturesCountByCreatorId(string creatorId);

        int GetCreatedExamsCountByCreatorId(string creatorId);

        int GetCreatedAssignmentsCountByCreatorId(string creatorId);

        int GetCreatedChannelsCountByCreatorId(string creatorId);

        int GetAllCoursesCount();

        int GetAllStudentsCount();

        int GetAllTeachersCount();

        T GetUserInfoById<T>(string userId);

        IEnumerable<T> GetTop3ExamsByCreatorId<T>(string userId);

        IEnumerable<T> GetTop3StudentsByCompletedLectures<T>();

        IEnumerable<T> GetTop3StudentsByCompletedAssignments<T>();

        IEnumerable<T> GetTop3CoursesByUserId<T>(string userId);

        IEnumerable<T> GetTop3CoursesWithMostCompletedLecturesByUserId<T>(string userId);

        IEnumerable<T> GetTop3CoursesWithMostCompletedAssignmentsByUserId<T>(string userId);
    }
}
