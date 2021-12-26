namespace OnlineCourseManagementSystem.Web.ViewModels.Files
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllImagesViewModel : IMapFrom<Album>
    {
        public string Name { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }

        public int Id { get; set; }
    }
}
