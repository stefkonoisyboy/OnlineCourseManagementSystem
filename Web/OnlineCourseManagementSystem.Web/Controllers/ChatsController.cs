namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Data;
    using SmartBreadcrumbs.Attributes;

    public class ChatsController : Controller
    {
        [Authorize]
        [Breadcrumb("Messages", FromAction ="Index", FromController = typeof(HomeController))]
        public IActionResult IndexChats()
        {
            return this.View();
        }
    }
}
