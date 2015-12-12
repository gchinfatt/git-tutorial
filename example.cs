using RV.Contracts;
using RV.Core.Entities;
using RV.Core.ViewModels;
using RV.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RV.WebUI.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IResourceService _resourceService;

        public HomeController(IUserService userService, IResourceService resourceService)
        {
            _userService = userService;
            _resourceService = resourceService;
        }


        public async Task<JsonResult> Resources()
        {
            var currentUserId = await _userService.GetUserIdByEmailAsync(User.Identity.Name);
            var modelList = await _resourceService.GetAllResourcesAsync(currentUserId);

            return Json(modelList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> AddResource(Resource resource)
        {
            await _resourceService.AddNewResourceAsync(resource);
            return Content("Resource successfully added!");
        }

        public ActionResult Index()
        {
            return View();
        }

        private static SelectListItem CreateSelectListItem(Category category)
        {
            return new SelectListItem { Value = category.CategoryId.ToString(), Text = category.Name };
        }

        private static SelectListItem CreateSelectListItem(SkillLevel skillLevel)
        {
            var gina ="is awesome";
			return new SelectListItem { Value = skillLevel.SkillLevelId.ToString(), Text = skillLevel.Name };
        }

        #region Notes
        /*
        TODO: Authentication, handling of users

            - Consider pulling out db context hits into a service
            - create new resource

            - new controllers for votes and resources and user control
            - moved resources list to EF generated controllers/views
        */
        #endregion
    }
}