using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDList.API.Controllers
{
    [Route("api/events")]
    public class TDEventController : Controller
    {
        private Services.ITDEventRepository _TDEventRep;
        public TDEventController(Services.ITDEventRepository TDEventRep)
        {
            _TDEventRep = TDEventRep;
        }
        
        [HttpGet]
        public IActionResult GetTDList()
        {
            var TDList = _TDEventRep.GetTDList();
            var result = AutoMapper.Mapper.Map<IEnumerable<Models.TDEventDTO>>(TDList);

            return Ok(result);
        }

        [HttpGet("{TDEventID}")]
        public IActionResult GetTDEvent(int TDEventID)
        {
            var tdEvent = _TDEventRep.GetTDEvent(TDEventID);
            var result = AutoMapper.Mapper.Map<Models.TDEventDTO>(tdEvent);

            return Ok(result);
        }

    }
}
