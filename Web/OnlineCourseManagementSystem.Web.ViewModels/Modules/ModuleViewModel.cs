namespace OnlineCourseManagementSystem.Web.ViewModels.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;
    using OnlineCourseManagementSystem.Web.ViewModels.Subjects;

    public class ModuleViewModel : IMapFrom<TrainingModule>, IMapFrom<ModuleEntity>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<SubjectViewModel> Subjects { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<TrainingModule, ModuleViewModel>()
                .ForMember(v => v.Id, y => y.MapFrom(tm => tm.ModuleId))
                .ForMember(v => v.Name, y => y.MapFrom(tm => tm.Module.Name));
        }
    }
}
