using Business.Abstract;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEBUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IPersonalService _personalService;

        public ValuesController(IPersonalService personalService)
        {
            _personalService = personalService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Personal personal)
        {
            _personalService.Add(personal);
            return Ok("eklendi");
        }

    }
}
