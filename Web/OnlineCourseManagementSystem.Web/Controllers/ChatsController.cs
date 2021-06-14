using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseManagementSystem.Common;
using OnlineCourseManagementSystem.Data.Models;
using OnlineCourseManagementSystem.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Web.Controllers
{
    public class ChatsController : Controller
    {
        [Authorize]
        public IActionResult IndexChats()
        {
            return this.View();
        }
    }
}
