// ReSharper disable VirtualMemberCallInConstructor
namespace OnlineCourseManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using OnlineCourseManagementSystem.Data.Common.Models;
    using OnlineCourseManagementSystem.Data.Models.Enumerations;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Events = new HashSet<Event>();
            this.Orders = new HashSet<Order>();
            this.LecturerExams = new HashSet<Exam>();
            this.Exams = new HashSet<UserExam>();
            this.Files = new HashSet<File>();
            this.Assignments = new HashSet<UserAssignment>();
            this.Courses = new HashSet<UserCourse>();
            this.Comments = new HashSet<Comment>();
            this.Answers = new HashSet<Answer>();
            this.Likes = new HashSet<Like>();
            this.Dislikes = new HashSet<Dislike>();
            this.Posts = new HashSet<Post>();
            this.Albums = new HashSet<Album>();
            this.AudienceComments = new HashSet<AudienceComment>();
            this.Channels = new HashSet<UserChannel>();
            this.CreatedChannels = new HashSet<Channel>();
            this.Chats = new HashSet<ChatUser>();
            this.CreatedChats = new HashSet<Chat>();
            this.Messages = new HashSet<Message>();
            this.Emojis = new HashSet<MessageEmoji>();
            this.Reviews = new HashSet<Review>();
            this.CoursesCreated = new HashSet<Course>();
            this.Lectures = new HashSet<Lecture>();
            this.ContactMessages = new HashSet<ContactMessage>();
            this.ExamsCreated = new HashSet<Exam>();
            this.Certificates = new HashSet<Certificate>();
            this.Completitions = new HashSet<Completition>();
            this.MessageQAs = new HashSet<MessageQA>();
            this.ChatbotMessages = new HashSet<ChatbotMessage>();
            this.FileCompletitions = new HashSet<FileCompletition>();
            this.Submissions = new HashSet<Submission>();
            this.CreatedRooms = new HashSet<Room>();

            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string ParentId { get; set; }

        public virtual Parent Parent { get; set; }

        public string StudentId { get; set; }

        public virtual Student Student { get; set; }

        public string LecturerId { get; set; }

        public virtual Lecturer Lecturer { get; set; }

        public int MachineLearningId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string Background { get; set; }

        public DateTime BirthDate { get; set; }

        public int TownId { get; set; }

        public virtual Town Town { get; set; }

        public string Address { get; set; }

        public string ProfileImageUrl { get; set; }

        public Title Title { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Exam> LecturerExams { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual ICollection<UserAssignment> Assignments { get; set; }

        public virtual ICollection<UserCourse> Courses { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<UserExam> Exams { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Dislike> Dislikes { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public virtual ICollection<AudienceComment> AudienceComments { get; set; }

        public virtual ICollection<UserChannel> Channels { get; set; }

        public virtual ICollection<Channel> CreatedChannels { get; set; }

        public virtual ICollection<ChatUser> Chats { get; set; }

        public virtual ICollection<Chat> CreatedChats { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<MessageEmoji> Emojis { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Course> CoursesCreated { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }

        public virtual ICollection<ContactMessage> ContactMessages { get; set; }

        public virtual ICollection<Exam> ExamsCreated { get; set; }

        public virtual ICollection<Certificate> Certificates { get; set; }

        public virtual ICollection<Completition> Completitions { get; set; }

        public virtual ICollection<MessageQA> MessageQAs { get; set; }

        public virtual ICollection<ChatbotMessage> ChatbotMessages { get; set; }

        public virtual ICollection<FileCompletition> FileCompletitions { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }

        public virtual ICollection<Room> CreatedRooms { get; set; }
    }
}
