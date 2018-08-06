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

        public static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        
        [HttpGet]
        public IActionResult GetTDList()
        {
            _logger.Debug("TDList was requested");

            var TDList = _TDEventRep.GetTDList();
            var result = AutoMapper.Mapper.Map<IEnumerable<Models.TDEventDTO>>(TDList);

            _logger.Info("TDList was requested successfully");
            return Ok(result);
        }

        [HttpGet("{TDEventID}")]
        public IActionResult GetTDEvent(int TDEventID)
        {
            _logger.Debug($"TDEvent with ID {TDEventID} was requested");

            var tdEvent = _TDEventRep.GetTDEvent(TDEventID);
            if(tdEvent == null)
            {
                return NotFound();
            }
            var result = AutoMapper.Mapper.Map<Models.TDEventDTO>(tdEvent);

            _logger.Debug($"TDEvent with ID {TDEventID} was requested successfully");
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddTDEvent([FromBody] Models.TDEventToManip tdEventToAdd)
        {
            _logger.Debug("Method to add new TDEvent was called");

            if(tdEventToAdd == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = AutoMapper.Mapper.Map<Entities.TDEvent>(tdEventToAdd);

            try
            {
                _TDEventRep.AddTDEvent(result);
            }
            catch (Exception ex)
            {
                _logger.Error("Exception was throwed while addiction TDEvent: ", ex);
            }

            if (!_TDEventRep.IsSaved())
            {
                _logger.Warn("TDList hasn`t been saved while addition TDEvent");
                return StatusCode(500, "Server do not respond.");
            }

            //string path = "api/events/" + result.Id;

            //_logger.Info("TDEvent has been successfully added: " + path);


            _logger.Info("TDEvent has been successfully added: " + "api/events/" + result.Id);
            return CreatedAtRoute(result.Id, result);
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
                _logger.Warn("TDList hasn`t been saved while updating TDEvent");
                return StatusCode(500, "Server do not respond");
            }

            _logger.Info($"TDEvent with ID {TDEventID} has been successfully updated.");
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
                _logger.Warn("TDList hasn`t been saved while patching TDEvent.");
                return StatusCode(500, "Server do not respond");
            }

            _logger.Info($"TDEvent with ID {TDEventID} has been successfully patched.");
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
                _logger.Warn("TDList hasn`t been saved while removing TDEvent");
                return StatusCode(500, "Server do not respond");
            }

            _logger.Info($"TDEvent with ID {TDEventID} has been successfully removed.");
            return NoContent();
        }
    }
}
