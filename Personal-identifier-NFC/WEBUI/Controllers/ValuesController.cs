using Business.Abstract;
using Entities.Models;
using Entities.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WEBUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IPersonalService _personalService;
        private readonly IBaseValuesService _valuesService;

        public ValuesController(IPersonalService personalService, IBaseValuesService valuesService)
        {
            _personalService = personalService;
            _valuesService = valuesService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Personal personal)
        {
            _personalService.Add(personal);
            return Ok("eklendi");
        }   
        [HttpPost("aaaaaa")]
        public IActionResult aaaaaaaa([FromBody] PersonalDataDto personal)
        {
            var data=JsonConvert.SerializeObject(personal);
            return Ok(personal);
        }
        [HttpPost("addbasevalue")]
        public IActionResult AddValue([FromBody] BaseValues baseValues)
        {
            _valuesService.Add(baseValues);
            return Ok("eklendi");
        }

    }
}
