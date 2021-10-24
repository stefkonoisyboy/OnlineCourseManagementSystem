namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Services.Data;

    public class VideoChatsController : Controller
    {
        [Authorize]
        public IActionResult IndexVideoChat()
        {
            return this.View();
        }
    }
}
