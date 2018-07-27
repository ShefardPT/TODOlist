using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDList.API.Entities;

namespace TDList.API.Controllers
{
    public class TDEventController : Controller
    {
        private TDEventContext _ctx;

        public TDEventController(TDEventContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/start")]
        public IActionResult Start()
        {
            return Ok();
        }
    }
}
