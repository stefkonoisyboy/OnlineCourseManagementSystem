using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Exams
{
    public class AllExamsByLectureIdViewModel : IMapFrom<Exam>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string LectureTitle { get; set; }

        public string CreatorFirstName { get; set; }

        public string CreatorLastName { get; set; }
    }
}
