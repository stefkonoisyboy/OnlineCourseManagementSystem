namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Services.Data;
    using SmartBreadcrumbs.Attributes;

    public class VideoChatsController : Controller
    {
        [Authorize]
        [Breadcrumb("Video Conference", FromAction = "Index", FromController =typeof(HomeController))]
        public IActionResult IndexVideoChat()
        {
            return this.View();
        }
    }
}
