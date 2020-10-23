using hurricaneapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace hurricaneapi.Controllers
{
    [Route("[controller]")]
    [Controller]
    public class HurricaneController : ControllerBase
    {
        private HurricaneService hurricaneService { get; }
        public HurricaneController(HurricaneService hurricaneService)
        {
            this.hurricaneService = hurricaneService;
        }
        // GET
        [Route("fetch")]
        [HttpGet]
        public ActionResult Get()
        {
            hurricaneService.FetchFiles();
            return Ok();
        }

        
    }
}