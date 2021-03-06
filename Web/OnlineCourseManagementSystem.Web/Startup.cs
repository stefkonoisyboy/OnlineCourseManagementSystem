namespace OnlineCourseManagementSystem.Web
{
    using System.Reflection;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
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
    using OnlineCourseManagementSystem.Web.ViewModels;
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

            services.AddSignalR();
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

            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IStudentsService, StudentsService>();
            services.AddTransient<ILecturersService, LecturersService>();
            services.AddTransient<IParentsService, ParentsService>();
            services.AddTransient<IFilesService, FilesService>();
            services.AddTransient<ICoursesService, CoursesService>();
            services.AddTransient<ITagsService, TagsService>();
            services.AddTransient<ISubjectsService, SubjectsService>();
            services.AddTransient<ILecturesService, LecturesService>();
            services.AddTransient<IAlbumsService, AlbumsService>();
            services.AddTransient<IAssignmentsService, AssignmentsService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<ITownsService, TownsService>();
            services.AddTransient<IAbsencesService, AbsencesService>();
            services.AddTransient<IPostsService, PostsService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<IEventsService, EventsService>();
            services.AddTransient<IExamsService, ExamsService>();
            services.AddTransient<IQuestionsService, QuestionsService>();
            services.AddTransient<IChoicesService, ChoicesService>();
            services.AddTransient<IChannelsService, ChannelsService>();
            services.AddTransient<IAudienceCommentsService, AudienceCommentsService>();
            services.AddTransient<IChatsService, ChatsService>();
            services.AddTransient<IMessagesService, MessagesService>();
            services.AddTransient<IAnswersService, AnswersService>();
            services.AddTransient<ISkillsService, SkillsService>();
            services.AddTransient<IReviewsService, ReviewsService>();
            services.AddTransient<IContactMessagesService, ContactMessagesService>();

            services.Configure<StripeSettings>(this.configuration.GetSection("Stripe"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.ApiKey = this.configuration.GetSection("Stripe")["SecretKey"];
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

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
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
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
                        endpoints.MapFallbackToFile("index.html");
                        endpoints.MapBlazorHub();
                        endpoints.MapHub<ChatHub>("hubs/ChatHub");
                    });
        }
    }
}
