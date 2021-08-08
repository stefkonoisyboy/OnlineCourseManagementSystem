using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Files
{
    public class AllFilesByLectureIdViewModel : IMapFrom<File>
    {
        public string Extension { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }
    }
}
