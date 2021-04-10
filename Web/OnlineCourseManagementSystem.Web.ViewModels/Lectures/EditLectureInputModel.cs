namespace OnlineCourseManagementSystem.Web.ViewModels.Lectures
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class EditLectureInputModel : BaseLectureInputModel, IMapFrom<Lecture>
    {
        public int Id { get; set; }
    }
}
