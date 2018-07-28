using Microsoft.AspNetCore.JsonPatch;
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
            if(tdEvent == null)
            {
                return NotFound();
            }
            var result = AutoMapper.Mapper.Map<Models.TDEventDTO>(tdEvent);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult PostTDEvent([FromBody] Models.TDEventToManip tdEventToAdd)
        {
            var result = AutoMapper.Mapper.Map<Entities.TDEvent>(tdEventToAdd);

            _TDEventRep.PostTDEvent(result);
            if (!_TDEventRep.IsSaved())
            {
                return StatusCode(500);
            }

            string path = "api/events/" + result.Id;
            
            return CreatedAtRoute(path, result);
        }

        [HttpPut("{TDEventID}")]
        public IActionResult PutTDEvent(int TDEventID, [FromBody] Models.TDEventToManip tdEventToPut)
        {
            var tdEvent = _TDEventRep.GetTDEvent(TDEventID);
            if (tdEvent == null)
            {
                return NotFound();
            }
            AutoMapper.Mapper.Map(tdEventToPut, tdEvent);

            if (!_TDEventRep.IsSaved())
            {
                return StatusCode(500);
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
                return StatusCode(500);
            }

            return NoContent();
        }

        [HttpDelete("{TDEventID}")]
        public IActionResult DeleteTDEvent(int TDEventID)
        {
            _TDEventRep.DeleteTDEvent(TDEventID);
            if(!_TDEventRep.IsSaved())
            {
                return StatusCode(500);
            }
            return NoContent();
        }

    }
}
