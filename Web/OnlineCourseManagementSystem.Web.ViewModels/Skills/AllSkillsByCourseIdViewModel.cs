using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Skills
{
    public class AllSkillsByCourseIdViewModel : IMapFrom<Skill>
    {
        public string Text { get; set; }
    }
}
