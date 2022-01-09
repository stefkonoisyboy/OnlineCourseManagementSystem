namespace OnlineCourseManagementSystem.Web
{
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    using CloudinaryDotNet;
    using LazZiya.ExpressLocalization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MudBlazor.Services;
    using OnlineCourseManagementSystem.Data;
    using OnlineCourseManagementSystem.Data.Common;
    using OnlineCourseManagementSystem.Data.Common.Repositories;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Data.Repositories;
    using OnlineCourseManagementSystem.Data.Seeding;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Services.Messaging;
    using OnlineCourseManagementSystem.Web.Hubs;
    using OnlineCourseManagementSystem.Web.LocalizationResources;
    using OnlineCourseManagementSystem.Web.ViewModels;
    using OnlineCourseManagementSystem.Web.ViewModels.Mails.Settings;
    using OnlineCourseManagementSystem.Web.ViewModels.VideoConferences;
    using SmartBreadcrumbs.Extensions;
    using Stripe;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            CloudinaryDotNet.Account cloudinaryCredentials = new CloudinaryDotNet.Account(
                this.configuration["Cloudinary:CloudName"],
                this.configuration["Cloudinary:ApiKey"],
                this.configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.AddSingleton(cloudinaryUtility);

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddCors();

            services.Configure<TwilioSettings>(settings =>
            {
                settings.AccountSid = this.configuration["Twilio:TwilioAccountSid"];
                settings.ApiSecret = this.configuration["Twilio:TwilioApiSecret"];
                settings.ApiKey = this.configuration["Twilio:TwilioApiKey"];
            });

            string separator = " / ";
            services.AddBreadcrumbs(this.GetType().Assembly, options =>
            {
                options.TagName = "nav";
                options.TagClasses = "";
                options.OlClasses = "breadcrumb";
                options.LiClasses = "breadcrumb-item";
                options.ActiveLiClasses = "breadcrumb-item active";
                options.SeparatorElement = $"<li class=\"separator\">{separator}</li>";
            });

            services.AddSignalR(options => options.EnableDetailedErrors = true)
                .AddMessagePackProtocol();
            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();

            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(this.configuration);
            services.AddServerSideBlazor();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddMudServices();

            // 36 services overall, but 34 services because we don't use parents and absences service
            services.AddTransient<IUsersService, UsersService>(); // Miro UnitTests Done Comments Done
            services.AddTransient<IStudentsService, StudentsService>(); // Miro UnitTests Done Comments Done
            services.AddTransient<ILecturersService, LecturersService>(); // Miro UnitTests Done Comments Done
            services.AddTransient<IParentsService, ParentsService>();
            services.AddTransient<IFilesService, FilesService>(); // Miro UnitTests Done Comments Done
            services.AddTransient<ICoursesService, CoursesService>(); // Stefko
            services.AddTransient<ITagsService, TagsService>(); // Stefko
            services.AddTransient<ISubjectsService, SubjectsService>(); // Stefko
            services.AddTransient<ILecturesService, LecturesService>(); // Stefko
            services.AddTransient<IAlbumsService, AlbumsService>();// Miro Done UnitTests Done Comments Done
            services.AddTransient<IAssignmentsService, AssignmentsService>();
            services.AddTransient<IOrdersService, OrdersService>(); // Stefko
            services.AddTransient<ITownsService, TownsService>(); // Stefko
            services.AddTransient<IAbsencesService, AbsencesService>();
            services.AddTransient<IPostsService, PostsService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<IEventsService, EventsService>();
            services.AddTransient<IExamsService, ExamsService>(); // Stefko
            services.AddTransient<IQuestionsService, QuestionsService>(); // Stefko
            services.AddTransient<IChoicesService, ChoicesService>(); // Stefko
            services.AddTransient<IChannelsService, ChannelsService>(); // Stefko
            services.AddTransient<IAudienceCommentsService, AudienceCommentsService>(); // Stefko
            services.AddTransient<IChatsService, ChatsService>();
            services.AddTransient<IMessagesService, MessagesService>();
            services.AddTransient<IAnswersService, AnswersService>(); // Stefko
            services.AddTransient<ISkillsService, SkillsService>(); // Stefko
            services.AddTransient<IReviewsService, ReviewsService>(); // Stefko
            services.AddTransient<IContactMessagesService, ContactMessagesService>();
            services.AddTransient<ICompletitionsService, CompletitionsService>(); // Stefko
            services.AddTransient<ICertificatesService, CertificatesService>(); // Stefko
            services.AddTransient<IMessageQAsService, MessageQAsService>(); // Stefko
            services.AddTransient<IDashboardService, DashboardService>(); // Stefko
            services.AddTransient<IChatbotMessagesService, ChatbotMessagesService>(); // Stefko
            services.AddTransient<ISubscribersService, SubscribersService>();
            services.AddTransient<IMailsService, MailsService>();
            services.AddSingleton<ITwilioService, TwilioService>();

            services.Configure<StripeSettings>(this.configuration.GetSection("Stripe"));
            services.Configure<MailSettings>(this.configuration.GetSection("MailSettings"));
            services.AddResponseCompression(opts =>
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.ApiKey = this.configuration.GetSection("Stripe")["SecretKey"];
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            app.UseResponseCompression();

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                        endpoints.MapBlazorHub();
                        endpoints.MapHub<VideoHub>("hubs/VideoHub");
                        endpoints.MapHub<ChatHub>("hubs/ChatHub");
                        endpoints.MapHub<QAHub>("hubs/QAHub");
                    });
        }
    }
}
