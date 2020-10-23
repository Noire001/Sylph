using System.Collections;
using hurricaneapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace hurricaneapi.Controllers
{
    [Route("[controller]")]
    [Controller]
    public class HurricaneController : ControllerBase
    {
        private HurricaneService HurricaneService { get; }

        public HurricaneController(HurricaneService hurricaneService)
        {
            this.HurricaneService = hurricaneService;
        }

        // GET
        [Route("fetch")]
        [HttpGet]
        public ActionResult Get()
        {
            HurricaneService.FetchFiles();
            return Ok();
        }

        [Route("api")]
        [HttpGet]
        public string GetHurricanes([FromQuery] string query)
        {
            return HurricaneService.GetHurricanes();
        }
    }
}