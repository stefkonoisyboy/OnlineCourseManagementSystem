namespace OnlineCourseManagementSystem.Web.ViewModels.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Courses;

    public class ModuleByIdViewModel : IMapFrom<Module>
    {
        public IEnumerable<AllCoursesBySubjectViewModel> Courses { get; set; }
    }
}
