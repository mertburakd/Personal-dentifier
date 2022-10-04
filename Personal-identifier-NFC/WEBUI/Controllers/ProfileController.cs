using AutoMapper;
using Business.Abstract;
using Entities.Models;
using Entities.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using WEBUI.Entities;

namespace WEBUI.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IPersonalService _personalService;
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly RoleManager<CustomIdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public ProfileController(IMapper mapper,IPersonalService personalService, UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager)
        {
            _personalService = personalService;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpGet("MyProfile/{SlugUrl}")]
        public IActionResult MyProfile(string SlugUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
               
                return View(new List<PersonalDataDto>());
            }
            //var xx = _userManager.FindByEmailAsync("mertburakdervisoglu@gmail.com");
            //_userManager.DeleteAsync(xx.Result);
            var data = _personalService.PersonalDataView(SlugUrl).Data;
            return View(data);
        }

        public IActionResult AktifPasif(int id, char durum)
        {
            if (id == 1)
            { 
                 var data = _personalService.GetUserData(Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)));
                var map = _mapper.Map<List<PersonalDataDto>>(JsonConvert.DeserializeObject<List<BaseValues>>(data.Data.Data));
                return PartialView("~/Views/Shared/Partial/_Profiledata.cshtml", map);
            }
            return PartialView("~/Views/Shared/Partial/_Profiledata.cshtml", new List<PersonalDataDto>());
        }
    }
}
