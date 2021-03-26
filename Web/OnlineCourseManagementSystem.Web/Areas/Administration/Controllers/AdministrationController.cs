namespace OnlineCourseManagementSystem.Web.Areas.Administration.Controllers
{
    using OnlineCourseManagementSystem.Common;
    using OnlineCourseManagementSystem.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
