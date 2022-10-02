using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Security.Claims;

namespace WEBUI.ViewComponents
{
    public class UserSlugViewComponent : ViewComponent
    {
        private readonly IPersonalService _personalService;

        public UserSlugViewComponent(IPersonalService personalService)
        {
            _personalService = personalService;
        }

        public ViewViewComponentResult Invoke(int id)
        {

            return View(_personalService.GetUserData(id).Data);
        }
    }
}
