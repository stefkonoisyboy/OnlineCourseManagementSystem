namespace OnlineCourseManagementSystem.Web.ViewModels.Certificates
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllCertificatesByUserIdViewModel : IMapFrom<Certificate>
    {
        public int Id { get; set; }

        public string CourseFileRemoteUrl { get; set; }

        public string CourseName { get; set; }

        public int CourseId { get; set; }

        [IgnoreMap]
        public double Grade { get; set; }
    }
}
