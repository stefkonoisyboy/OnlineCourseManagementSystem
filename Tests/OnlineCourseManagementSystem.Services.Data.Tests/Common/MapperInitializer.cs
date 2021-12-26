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
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;
    using OnlineCourseManagementSystem.Web.ViewModels.Files;
    using OnlineCourseManagementSystem.Web.ViewModels.Lecturers;
    using OnlineCourseManagementSystem.Web.ViewModels.Students;
    using OnlineCourseManagementSystem.Web.ViewModels.Users;

    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
               typeof(AllCoursesViewModel).GetTypeInfo().Assembly,
               typeof(Course).GetTypeInfo().Assembly);

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
        }
    }
}
