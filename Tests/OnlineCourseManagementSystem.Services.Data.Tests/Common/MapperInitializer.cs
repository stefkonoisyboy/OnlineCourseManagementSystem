namespace OnlineCourseManagementSystem.Services.Data.Tests.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Albums;
    using OnlineCourseManagementSystem.Web.ViewModels.Answers;
    using OnlineCourseManagementSystem.Web.ViewModels.Assignments;
    using OnlineCourseManagementSystem.Web.ViewModels.Certificates;
    using OnlineCourseManagementSystem.Web.ViewModels.Channels;
    using OnlineCourseManagementSystem.Web.ViewModels.ChatbotMessages;
    using OnlineCourseManagementSystem.Web.ViewModels.Chats;
    using OnlineCourseManagementSystem.Web.ViewModels.Choices;
    using OnlineCourseManagementSystem.Web.ViewModels.ContactMessages;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Events;
    using OnlineCourseManagementSystem.Web.ViewModels.Exams;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;
    using OnlineCourseManagementSystem.Web.ViewModels.Lecturers;
    using OnlineCourseManagementSystem.Web.ViewModels.Lectures;
    using OnlineCourseManagementSystem.Web.ViewModels.MessageQAs;
    using OnlineCourseManagementSystem.Web.ViewModels.Messages;
    using OnlineCourseManagementSystem.Web.ViewModels.Orders;
    using OnlineCourseManagementSystem.Web.ViewModels.Questions;
    using OnlineCourseManagementSystem.Web.ViewModels.Reviews;
    using OnlineCourseManagementSystem.Web.ViewModels.Skills;
    using OnlineCourseManagementSystem.Web.ViewModels.Students;
    using OnlineCourseManagementSystem.Web.ViewModels.Tags;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
               typeof(AllCoursesViewModel).GetTypeInfo().Assembly,
               typeof(Course).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
               typeof(AllChatbotMessagesByCreatorIdViewModel).GetTypeInfo().Assembly,
               typeof(ChatbotMessage).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
               typeof(AllMessagesByChannelIdViewModel).GetTypeInfo().Assembly,
               typeof(MessageQA).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
              typeof(AllCertificatesByUserIdViewModel).GetTypeInfo().Assembly,
              typeof(Certificate).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
               typeof(AllReviewsByCourseIdViewModel).GetTypeInfo().Assembly,
               typeof(Review).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
               typeof(AllSkillsByCourseIdViewModel).GetTypeInfo().Assembly,
               typeof(Skill).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
              typeof(AllAnswersByExamIdAndUserIdViewModel).GetTypeInfo().Assembly,
              typeof(Answer).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
               typeof(ChannelByIdViewModel).GetTypeInfo().Assembly,
               typeof(Channel).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
               typeof(AllChoicesByIdViewModel).GetTypeInfo().Assembly,
               typeof(Course).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
               typeof(AllQuestionsByExamViewModel).GetTypeInfo().Assembly,
               typeof(Question).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
               typeof(ResultFromExamViewModel).GetTypeInfo().Assembly,
               typeof(UserExam).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
               typeof(AllExamsViewModel).GetTypeInfo().Assembly,
               typeof(Exam).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
               typeof(AllOrdersByUserIdViewModel).GetTypeInfo().Assembly,
               typeof(Order).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
              typeof(AllVideosByIdViewModel).GetTypeInfo().Assembly,
              typeof(File).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
              typeof(AllLecturesByCreatorIdViewModel).GetTypeInfo().Assembly,
              typeof(Lecture).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
               typeof(AllTagsViewModel).GetTypeInfo().Assembly,
               typeof(Tag).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(AllStudentsByIdViewModel).GetTypeInfo().Assembly,
                typeof(Student).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(AllLecturersByIdViewModel).GetTypeInfo().Assembly,
                typeof(Lecturer).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(UserViewModel).GetTypeInfo().Assembly,
                typeof(ApplicationUser).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(FileViewModel).GetTypeInfo().Assembly,
                typeof(File).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(AllImagesViewModel).GetTypeInfo().Assembly,
                typeof(Album).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(AlbumViewModel).GetTypeInfo().Assembly,
                typeof(Album).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(AssignmentViewModel).GetTypeInfo().Assembly,
                typeof(UserAssignment).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(AssignmentByAdminViewModel).GetTypeInfo().Assembly,
                typeof(Assignment).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(EventViewModel).GetTypeInfo().Assembly,
                typeof(Event).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(ChatViewModel).GetTypeInfo().Assembly,
                typeof(ChatUser).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(UserViewModel).GetTypeInfo().Assembly,
                typeof(ChatUser).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(MessageViewModel).GetTypeInfo().Assembly,
                typeof(Message).GetTypeInfo().Assembly);

            AutoMapperConfig.RegisterMappings(
                typeof(ContactMessageViewModel).GetTypeInfo().Assembly,
                typeof(ContactMessage).GetTypeInfo().Assembly);
        }
    }
}
