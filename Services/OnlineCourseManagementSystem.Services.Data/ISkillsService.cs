using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourseManagementSystem.Services.Data
{
    public interface ISkillsService
    {
        IEnumerable<T> GetAllByCourseId<T>(int id);
    }
}
