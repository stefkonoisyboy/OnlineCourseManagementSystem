namespace OnlineCourseManagementSystem.Web.ViewModels.Users
{
    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>, IMapFrom<ChatUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Background { get; set; }

        public string ProfileImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ChatUser, UserViewModel>()
                .ForMember(x => x.FirstName, y => y.MapFrom(cu => cu.User.FirstName))
                .ForMember(x => x.LastName, y => y.MapFrom(cu => cu.User.LastName))
                .ForMember(x => x.Id, y => y.MapFrom(cu => cu.UserId));
        }
    }
}
