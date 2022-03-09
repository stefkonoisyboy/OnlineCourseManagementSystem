namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class TestController : Controller
    {
        public IActionResult SomeAction()
        {
            return this.View();
        }
    }
}
