namespace OnlineCourseManagementSystem.Web.ViewModels.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class EditTestInputModel : BaseTestInputModel, IMapFrom<Test>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int ProblemId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Test, EditTestInputModel>()
                .ForMember(destination => destination.TestInput, opt => opt.MapFrom(source => source.Input))
                .ForMember(destination => destination.ExpectedOutput, opt => opt.MapFrom(source => source.Output));
        }
    }
}
