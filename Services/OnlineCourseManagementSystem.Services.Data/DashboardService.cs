namespace OnlineCourseManagementSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class DashboardService : IDashboardService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Course> coursesRepository;
        private readonly IDeletableEntityRepository<UserCourse> userCoursesRepository;
        private readonly IDeletableEntityRepository<Assignment> assignmentsRepository;
        private readonly IDeletableEntityRepository<UserAssignment> userAssignmentsRepository;
        private readonly IDeletableEntityRepository<Event> eventsRepository;
        private readonly IDeletableEntityRepository<Exam> examsRepository;
        private readonly IDeletableEntityRepository<UserExam> userExamsRepository;
        private readonly IDeletableEntityRepository<Channel> channelsRepository;
        private readonly IDeletableEntityRepository<Completition> completitionsService;
        private readonly IDeletableEntityRepository<Lecture> lecturesRepository;

        public DashboardService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Course> coursesRepository,
            IDeletableEntityRepository<UserCourse> userCoursesRepository,
            IDeletableEntityRepository<Assignment> assignmentsRepository,
            IDeletableEntityRepository<UserAssignment> userAssignmentsRepository,
            IDeletableEntityRepository<Event> eventsRepository,
            IDeletableEntityRepository<Exam> examsRepository,
            IDeletableEntityRepository<UserExam> userExamsRepository,
            IDeletableEntityRepository<Channel> channelsRepository,
            IDeletableEntityRepository<Completition> completitionsService,
            IDeletableEntityRepository<Lecture> lecturesRepository)
        {
            this.usersRepository = usersRepository;
            this.coursesRepository = coursesRepository;
            this.userCoursesRepository = userCoursesRepository;
            this.assignmentsRepository = assignmentsRepository;
            this.userAssignmentsRepository = userAssignmentsRepository;
            this.eventsRepository = eventsRepository;
            this.examsRepository = examsRepository;
            this.userExamsRepository = userExamsRepository;
            this.channelsRepository = channelsRepository;
            this.completitionsService = completitionsService;
            this.lecturesRepository = lecturesRepository;
        }

        public int GetAllCoursesCount()
        {
            return this.coursesRepository
                .All()
                .Count();
        }

        public int GetAllStudentsCount()
        {
            return this.usersRepository
                .All()
                .Count(u => u.Roles.FirstOrDefault().RoleId.EndsWith("Student"));
        }

        public int GetAllTeachersCount()
        {
            return this.usersRepository
                .All()
                .Count(u => u.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"));
        }

        public double GetAverageGradeByExamIdAndUserId(int examId, string userId)
        {
            return this.examsRepository
                .All()
                .FirstOrDefault(e => e.Id == examId)
                .Users.Count() == 0 ? 0 :
                this.examsRepository
                .All()
                .FirstOrDefault(e => e.Id == examId)
                .Users.Average(u => u.Grade);
        }

        public double GetAverageSuccessByUserId(string userId)
        {
            return this.userExamsRepository
                .All()
                .Count(ue => ue.UserId == userId) == 0 ? 0
                : this.userExamsRepository
                .All()
                .Where(ue => ue.UserId == userId)
                .Average(ue => ue.Grade);
        }

        public int GetChannelsCountByUserId(string userId)
        {
            return this.channelsRepository
                .All()
                .Count(c => c.Users.Any(u => u.UserId == userId));
        }

        public int GetCompletedAssignmentsByCourseIdAndUserId(int courseId, string userId)
        {
            return this.assignmentsRepository
                .All()
                .Count(a => a.CourseId == courseId && a.Users.Any(u => u.UserId == userId));
        }

        public int GetCompletedExamsCountByUserId(string userId)
        {
            return this.userExamsRepository
                .All()
                .Count(ue => ue.UserId == userId);
        }

        public int GetCompletedLecturesByCourseIdAndUserId(int courseId, string userId)
        {
            return this.completitionsService
                .All()
                .Count(c => c.Lecture.CourseId == courseId && c.UserId == userId);
        }

        public int GetCoursesEnrolledCountByUserId(string userId)
        {
            return this.userCoursesRepository
                .All()
                .Count(uc => uc.UserId == userId);
        }

        public int GetCreatedAssignmentsCountByCreatorId(string creatorId)
        {
            return this.assignmentsRepository
                .All()
                .Count(a => a.Course.CreatorId == creatorId);
        }

        public int GetCreatedChannelsCountByCreatorId(string creatorId)
        {
            return this.channelsRepository
                .All()
                .Count(c => c.CreatorId == creatorId);
        }

        public int GetCreatedCoursesCountByCreatorId(string creatorId)
        {
            return this.coursesRepository
                .All()
                .Count(c => c.CreatorId == creatorId);
        }

        public int GetCreatedExamsCountByCreatorId(string creatorId)
        {
            return this.examsRepository
                .All()
                .Count(e => e.CreatorId == creatorId);
        }

        public int GetCreatedLecturesCountByCreatorId(string creatorId)
        {
            return this.lecturesRepository
                .All()
                .Count(l => l.CreatorId == creatorId);
        }

        public int GetEventsCount()
        {
            return this.eventsRepository
                .All()
                .Count();
        }

        public int GetFinishedAssignmentsCountByUserId(string userId)
        {
            return this.userAssignmentsRepository
                .All()
                .Count(ua => ua.UserId == userId);
        }

        public int GetRankingForAverageSuccess(string userId)
        {
            IList<ApplicationUser> orderedUsers = this.usersRepository
                .All()
                .Where(u => u.Roles.FirstOrDefault().RoleId.EndsWith("Student"))
                .OrderByDescending(u => u.Exams.Count() == 0 ? 0 : u.Exams.Average(e => e.Grade))
                .ToList();

            int ranking = 0;

            for (int i = 0; i < orderedUsers.Count(); i++)
            {
                if (orderedUsers[i].Id == userId)
                {
                    ranking = i + 1;
                    break;
                }
            }

            return ranking;
        }

        public int GetRankingForCreatedAssignments(string creatorId)
        {
            IList<ApplicationUser> orderedUsers = this.usersRepository
                .All()
                .Where(u => u.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
                .OrderByDescending(u => u.Assignments.Count(a => a.Assignment.Course.CreatorId == creatorId))
                .ToList();

            int ranking = 0;

            for (int i = 0; i < orderedUsers.Count(); i++)
            {
                if (orderedUsers[i].Id == creatorId)
                {
                    ranking = i + 1;
                    break;
                }
            }

            return ranking;
        }

        public int GetRankingForCreatedCourses(string creatorId)
        {
            IList<ApplicationUser> orderedUsers = this.usersRepository
               .All()
               .Where(u => u.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
               .OrderByDescending(u => u.CoursesCreated.Count(c => c.CreatorId == creatorId))
               .ToList();

            int ranking = 0;

            for (int i = 0; i < orderedUsers.Count(); i++)
            {
                if (orderedUsers[i].Id == creatorId)
                {
                    ranking = i + 1;
                    break;
                }
            }

            return ranking;
        }

        public int GetRankingForCreatedLectures(string creatorId)
        {
            IList<ApplicationUser> orderedUsers = this.usersRepository
               .All()
               .Where(u => u.Roles.FirstOrDefault().RoleId.EndsWith("Lecturer"))
               .OrderByDescending(u => u.Lectures.Count(l => l.CreatorId == creatorId))
               .ToList();

            int ranking = 0;

            for (int i = 0; i < orderedUsers.Count(); i++)
            {
                if (orderedUsers[i].Id == creatorId)
                {
                    ranking = i + 1;
                    break;
                }
            }

            return ranking;
        }

        public int GetRankingForEnrolledCourses(string userId)
        {
            IList<ApplicationUser> orderedUsers = this.usersRepository
                .All()
                .Where(u => u.Roles.FirstOrDefault().RoleId.EndsWith("Student"))
                .OrderByDescending(u => u.Courses.Count())
                .ToList();

            int ranking = 0;

            for (int i = 0; i < orderedUsers.Count(); i++)
            {
                if (orderedUsers[i].Id == userId)
                {
                    ranking = i + 1;
                    break;
                }
            }

            return ranking;
        }

        public int GetRankingForFinishedAssignments(string userId)
        {
            IList<ApplicationUser> orderedUsers = this.usersRepository
                .All()
                .Where(u => u.Roles.FirstOrDefault().RoleId.EndsWith("Student"))
                .OrderByDescending(u => u.Courses.Count())
                .ToList();

            int ranking = 0;

            for (int i = 0; i < orderedUsers.Count(); i++)
            {
                if (orderedUsers[i].Id == userId)
                {
                    ranking = i + 1;
                    break;
                }
            }

            return ranking;
        }

        public IEnumerable<T> GetTop3CoursesByUserId<T>(string userId)
        {
            return this.userExamsRepository
                .All()
                .Where(ue => ue.UserId == userId)
                .OrderByDescending(ue => ue.Grade)
                .Take(3)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetTop3CoursesWithMostCompletedAssignmentsByUserId<T>(string userId)
        {
            return this.coursesRepository
                .All()
                .Where(c => c.Users.Any(u => u.UserId == userId))
                .OrderByDescending(c => c.Assignments.Count(a => a.Users.Any(u => u.AssignmentId == a.Id && u.UserId == userId)))
                .Take(3)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetTop3CoursesWithMostCompletedLecturesByUserId<T>(string userId)
        {
            return this.coursesRepository
                .All()
                .Where(c => c.Users.Any(u => u.UserId == userId))
                .OrderByDescending(c => c.Lectures.Count(l => l.Completitions.Any(comp => comp.LectureId == l.Id && comp.UserId == userId)))
                .Take(3)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetTop3ExamsByCreatorId<T>(string userId)
        {
            return this.examsRepository
                .All()
                .Where(e => e.CreatorId == userId)
                .OrderByDescending(e => e.Users.Count() == 0 ? 0 : e.Users.Average(u => u.Grade))
                .Take(3)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetTop3StudentsByCompletedAssignments<T>()
        {
            return this.usersRepository
                .All()
                .Where(u => u.Roles.FirstOrDefault().RoleId.EndsWith("Student"))
                .OrderByDescending(u => u.Assignments.Count())
                .Take(3)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetTop3StudentsByCompletedLectures<T>()
        {
            return this.usersRepository
                .All()
                .Where(u => u.Roles.FirstOrDefault().RoleId.EndsWith("Student"))
                .OrderByDescending(u => u.Completitions.Count())
                .Take(3)
                .To<T>()
                .ToList();
        }

        public T GetUserInfoById<T>(string userId)
        {
            return this.usersRepository
                .All()
                .Where(u => u.Id == userId)
                .To<T>()
                .FirstOrDefault();
        }
    }
}
