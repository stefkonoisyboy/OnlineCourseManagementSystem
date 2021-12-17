using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using OnlineCourseManagementSystem.Web.ViewModels.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Services.Data.Tests.Common
{
    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
               typeof(AllCoursesViewModel).GetTypeInfo().Assembly,
               typeof(Course).GetTypeInfo().Assembly);
        }
    }
}
