using Business.Abstract;
using Entities.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEBUI.Controllers
{

    public class ProfileController : Controller
    {
        private readonly IPersonalService _personalService;

        public ProfileController(IPersonalService personalService)
        {
            _personalService = personalService;
        }

        [HttpGet("MyProfile/{SlugUrl}")]
        [Authorize(Roles = "User")]
        public IActionResult MyProfile(string SlugUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View(new List<PersonalDataDto>());
            }
           var data= _personalService.PersonalDataView(SlugUrl).Data;
            return View(string.Format("~/Views/Profile/{0}.cshtml", "MyProfile"), data);
        }
    }
}
