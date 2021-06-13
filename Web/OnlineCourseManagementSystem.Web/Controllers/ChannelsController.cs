using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseManagementSystem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Web.Controllers
{
    public class ChannelsController : Controller
    {
        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public IActionResult IndexStudents()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.LecturerRoleName)]
        public IActionResult IndexLecturers()
        {
            return this.View();
        }
    }
}
