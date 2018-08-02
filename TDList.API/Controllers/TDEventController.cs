using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog.Common;


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
            if(tdEvent == null)
            {
                return NotFound();
            }
            var result = AutoMapper.Mapper.Map<Models.TDEventDTO>(tdEvent);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddTDEvent([FromBody] Models.TDEventToManip tdEventToAdd)
        {
            if(tdEventToAdd == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = AutoMapper.Mapper.Map<Entities.TDEvent>(tdEventToAdd);

            _TDEventRep.AddTDEvent(result);

            if (!_TDEventRep.IsSaved())
            {
                return StatusCode(500, "Server do not respond");
            }

            string path = "api/events/" + result.Id;
            
            return CreatedAtRoute(path, result);
        }

        [HttpPut("{TDEventID}")]
        public IActionResult UpdateTDEvent(int TDEventID, [FromBody] Models.TDEventToManip tdEventToPut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tdEvent = _TDEventRep.GetTDEvent(TDEventID);
            if (tdEvent == null)
            {
                return NotFound();
            }
            AutoMapper.Mapper.Map(tdEventToPut, tdEvent);

            if (!_TDEventRep.IsSaved())
            {
                return StatusCode(500, "Server do not respond");
            }

            return NoContent();
        }

        [HttpPatch("{TDEventID}")]
        public IActionResult PatchTDEvent(int TDEventID, [FromBody] JsonPatchDocument<Models.TDEventToManip> patchDocument)
        {
            var tdEvent = _TDEventRep.GetTDEvent(TDEventID);
            if (tdEvent == null)
            {
                return NotFound();
            }

            var tdEventToPatch = AutoMapper.Mapper.Map<Models.TDEventToManip>(tdEvent);

            patchDocument.ApplyTo(tdEventToPatch, ModelState);


            AutoMapper.Mapper.Map(tdEventToPatch, tdEvent);

            if (!_TDEventRep.IsSaved())
            {
                return StatusCode(500, "Server do not respond");
            }

            return NoContent();
        }

        [HttpDelete("{TDEventID}")]
        public IActionResult RemoveTDEvent(int TDEventID)
        {
            if(!_TDEventRep.IsExist(TDEventID))
            {
                return NotFound();
            }

            _TDEventRep.RemoveTDEvent(TDEventID);

            if(!_TDEventRep.IsSaved())
            {
                return StatusCode(500, "Server do not respond");
            }

            return NoContent();
        }

    }
}
