using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOlist.API.Entities;

namespace TODOlist.API.Controllers
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
