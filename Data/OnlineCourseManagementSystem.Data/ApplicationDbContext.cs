namespace OnlineCourseManagementSystem.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using OnlineCourseManagementSystem.Data.Common.Models;
    using OnlineCourseManagementSystem.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Lecturer> Lecturers { get; set; }

        public virtual DbSet<Parent> Parents { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<Subject> Subjects { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Assignment> Assignments { get; set; }

        public virtual DbSet<UserAssignment> UserAssignments { get; set; }

        public virtual DbSet<Lecture> Lectures { get; set; }

        public virtual DbSet<Exam> Exams { get; set; }

        public virtual DbSet<UserExam> UserExams { get; set; }

        public virtual DbSet<File> Files { get; set; }

        public virtual DbSet<Answer> Answers { get; set; }

        public virtual DbSet<Choice> Choices { get; set; }

        public virtual DbSet<Question> Questions { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<Like> Likes { get; set; }

        public virtual DbSet<Dislike> Dislikes { get; set; }

        public virtual DbSet<Town> Towns { get; set; }

        public virtual DbSet<UserCourse> UserCourses { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<CourseTag> CourseTags { get; set; }

        public virtual DbSet<CourseLecturer> CourseLecturers { get; set; }

        public virtual DbSet<Album> Albums { get; set; }

        public virtual DbSet<Absence> Absences { get; set; }

        public virtual DbSet<AudienceComment> AudienceComments { get; set; }

        public virtual DbSet<Channel> Channels { get; set; }

        public virtual DbSet<UserChannel> UserChannels { get; set; }

        public virtual DbSet<Chat> Chats { get; set; }

        public virtual DbSet<ChatUser> ChatUsers { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Emoji> Emojis { get; set; }

        public virtual DbSet<MessageEmoji> MessageEmojis { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<Skill> Skills { get; set; }

        public virtual DbSet<ContactMessage> ContactMessages { get; set; }

        public virtual DbSet<Certificate> Certificates { get; set; }

        public virtual DbSet<Completition> Completitions { get; set; }

        public virtual DbSet<MessageQA> MessageQAs { get; set; }

        public virtual DbSet<ChatbotMessage> ChatbotMessages { get; set; }

        public virtual DbSet<Subscriber> Subscribers { get; set; }

        public virtual DbSet<FileCompletition> FileCompletitions { get; set; }

        public virtual DbSet<Contest> Contests { get; set; }

        public virtual DbSet<Problem> Problems { get; set; }

        public virtual DbSet<Test> Tests { get; set; }

        public virtual DbSet<Submission> Submissions { get; set; }

        public virtual DbSet<ExecutedTest> ExecutedTests { get; set; }

        public virtual DbSet<Shedule> Shedules { get; set; }

        public virtual DbSet<Training> Trainings { get; set; }

        public virtual DbSet<ModuleEntity> Modules { get; set; }

        public virtual DbSet<TrainingModule> TrainingModules { get; set; }

        public virtual DbSet<Room> Rooms { get; set; }

        public virtual DbSet<RoomParticipant> RoomParticipants { get; set; }

        public virtual DbSet<Setting> Settings { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            builder.Entity<Exam>()
                .HasOne(e => e.Creator)
                .WithMany(c => c.ExamsCreated)
                .HasForeignKey(e => e.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            this.ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        // Applies configurations
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
