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

            // 36 services overall, but 33 services because we don't use parents and absences service
            services.AddTransient<IUsersService, UsersService>(); // 100%
            services.AddTransient<IStudentsService, StudentsService>(); // 100%
            services.AddTransient<ILecturersService, LecturersService>(); // 100%
            services.AddTransient<IParentsService, ParentsService>(); // NOT
            services.AddTransient<IFilesService, FilesService>(); // 100%
            services.AddTransient<ICoursesService, CoursesService>(); // All GET methods done
            services.AddTransient<ITagsService, TagsService>(); // 100%
            services.AddTransient<ISubjectsService, SubjectsService>(); // 100%
            services.AddTransient<ILecturesService, LecturesService>(); // All GET methods done
            services.AddTransient<IAlbumsService, AlbumsService>(); // 100%
            services.AddTransient<IAssignmentsService, AssignmentsService>(); // All GET methods done with comments.
            services.AddTransient<IOrdersService, OrdersService>(); // All GET methods done
            services.AddTransient<ITownsService, TownsService>(); // 100%
            services.AddTransient<IAbsencesService, AbsencesService>(); // NOT
            services.AddTransient<IPostsService, PostsService>(); // 100%
            services.AddTransient<ICommentsService, CommentsService>(); // 100%
            services.AddTransient<IEventsService, EventsService>(); // All GET methods and comments done
            services.AddTransient<IExamsService, ExamsService>(); // All GET methods done
            services.AddTransient<IQuestionsService, QuestionsService>(); // All GET methods done
            services.AddTransient<IChoicesService, ChoicesService>(); // 100%
            services.AddTransient<IChannelsService, ChannelsService>(); // All GET methods done
            services.AddTransient<IAudienceCommentsService, AudienceCommentsService>(); // NOT
            services.AddTransient<IChatsService, ChatsService>(); // All GET methods and comments done
            services.AddTransient<IMessagesService, MessagesService>(); // All GET methods and comments done.
            services.AddTransient<IAnswersService, AnswersService>(); // 100%
            services.AddTransient<ISkillsService, SkillsService>(); // 100%
            services.AddTransient<IReviewsService, ReviewsService>(); // All GET methods done
            services.AddTransient<IContactMessagesService, ContactMessagesService>();  // All GET methods and comments done
            services.AddTransient<ICompletitionsService, CompletitionsService>(); // 100%
            services.AddTransient<ICertificatesService, CertificatesService>(); // 100%
            services.AddTransient<IMessageQAsService, MessageQAsService>(); // All GET methods done
            services.AddTransient<IDashboardService, DashboardService>(); // Stefko
            services.AddTransient<IChatbotMessagesService, ChatbotMessagesService>(); // All GET methods done
            services.AddTransient<ISubscribersService, SubscribersService>(); // All GET methods and comments.
            services.AddTransient<IContestsService, ContestsService>();
            services.AddTransient<IProblemsService, ProblemsService>();
            services.AddTransient<ISubmissionsService, SubmissionsService>();
            services.AddTransient<ITestsService, TestsService>();
            services.AddTransient<IExecutedTestsService, ExecutedTestsService>();
            services.AddTransient<IShedulesServices, ShedulesService>();
            services.AddTransient<ITrainingsService, TrainingsService>();
            services.AddTransient<IModulesService, ModulesService>();
            services.AddTransient<IRoomsService, RoomsService>();

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
