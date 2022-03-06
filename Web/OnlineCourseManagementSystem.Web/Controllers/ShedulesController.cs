namespace OnlineCourseManagementSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OnlineCourseManagementSystem.Services.Data;
    using OnlineCourseManagementSystem.Web.ViewModels.Shedules;

    public class ShedulesController : Controller
    {
        private readonly IShedulesServices shedulesServices;

        public ShedulesController(IShedulesServices shedulesServices)
        {
            this.shedulesServices = shedulesServices;
        }

        [HttpPost]
        [Authorize(Roles = "Lecturer,Administartor")]
        public async Task<IActionResult> Create(CreateSheduleInputModel sheduleInputModel, int eventId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            try
            {
                sheduleInputModel.EventId = eventId;
                await this.shedulesServices.CreateAsync(sheduleInputModel);
                this.TempData["Message"] = "Successfully created shedule!";
            }
            catch (Exception ex)
            {
                this.TempData["ErrorMessage"] = ex.Message;
            }

            return this.RedirectToAction("AllCreated", "Events");
        }
    }
}
