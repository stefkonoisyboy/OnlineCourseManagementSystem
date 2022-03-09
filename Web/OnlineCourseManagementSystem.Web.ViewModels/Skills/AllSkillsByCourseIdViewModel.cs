namespace OnlineCourseManagementSystem.Web.ViewModels.Skills
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class AllSkillsByCourseIdViewModel : IMapFrom<Skill>
    {
        public string Text { get; set; }
    }
}
