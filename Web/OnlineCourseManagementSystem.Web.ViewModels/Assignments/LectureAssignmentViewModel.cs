namespace OnlineCourseManagementSystem.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class LectureAssignmentViewModel : IMapFrom<UserAssignment>, IHaveCustomMappings
    {

        public string Title { get; set; }

        public int AssignmentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? PossiblePoints { get; set; }

        public string CourseName { get; set; }

        public IEnumerable<AssignmentUserInfoViewModel> Users { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserAssignment, LectureAssignmentViewModel>()
              .ForMember(x => x.CourseName, y => y.MapFrom(x => x.Assignment.Course.Name))
              .ForMember(x => x.AssignmentId, y => y.MapFrom(x => x.AssignmentId))
              .ForMember(x => x.StartDate, y => y.MapFrom(x => x.Assignment.StartDate))
              .ForMember(x => x.EndDate, y => y.MapFrom(x => x.Assignment.EndDate))
              .ForMember(x => x.PossiblePoints, y => y.MapFrom(x => x.Assignment.PossiblePoints))
              .ForMember(x => x.Title, y => y.MapFrom(x => x.Assignment.Title));
        }
    }
}
