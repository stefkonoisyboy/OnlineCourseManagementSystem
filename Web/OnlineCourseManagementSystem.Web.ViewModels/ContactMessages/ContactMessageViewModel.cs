namespace OnlineCourseManagementSystem.Web.ViewModels.ContactMessages
{
    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class ContactMessageViewModel : IMapFrom<ContactMessage>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Content { get; set; }

        public bool IsSeen { get; set; }

        public string SeenByUserFullName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ContactMessage, ContactMessageViewModel>()
                .ForMember(cmv => cmv.SeenByUserFullName, y => y
                .MapFrom(cm => $"{cm.SeenByUser.FirstName} {cm.SeenByUser.LastName}"));
        }
    }
}
