using System;
using System.Collections.Generic;
using hurricaneapi.Models;
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


        [HttpGet]
        public List<Hurricane> GetHurricanes()
        {
            return HurricaneService.GetAllHurricanes();
        }
        [Route("api")]
        [HttpGet("{startdate:long?}/{enddate:long?}/{maxspeed:int?}/{active:int?}/{name?}/{sort?}")]
        public List<Hurricane> Get(long startdate = 0, long enddate = Int64.MaxValue, int maxspeed = Int32.MaxValue, short active = 2, string name = "", string sort = "desc")
        {
            return HurricaneService.GetHurricane(startdate, enddate, maxspeed, active, name, sort);
        }
    }
}