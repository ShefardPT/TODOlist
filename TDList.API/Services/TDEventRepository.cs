using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDList.API.Services
{
    public class TDEventRepository : ITDEventRepository
    {
        private Entities.TDEventContext _ctx;

        public static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public TDEventRepository(Entities.TDEventContext ctx)
        {
            _ctx = ctx;
            _logger.Debug("TDEventRepository initialized");
        }

        public Entities.TDEvent GetTDEvent(int TDEventID)
        {
            try
            {
                var result = _ctx.TDEvents.Where(p => p.Id == TDEventID).FirstOrDefault();
                _logger.Debug("Called GetTDEvent of TDEventRepository");
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error("Thrown exception while calling method GetTDEvent", ex);
                throw ex;
            }
        }

        public IEnumerable<Entities.TDEvent> GetTDList()
        {
            try
            {
                var result = _ctx.TDEvents.OrderByDescending(o => o.Urgency).ToList();
                _logger.Debug("Called GetTDList of TDEventRepository");
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error("Thrown exception while calling method GetTDList", ex);
                throw ex;
            }
        }

        public void AddTDEvent(Entities.TDEvent tdEventToAdd)
        {
            try
            {
                _ctx.Add(tdEventToAdd);
            }
            catch (Exception ex)
            {
                _logger.Error("Thrown exception while calling method AddTDEvent", ex);
                throw ex;
            }
        }

        public void RemoveTDEvent(int TDEventID)
        {
            try
            {
                _ctx.Remove(_ctx.TDEvents.Where(p => p.Id == TDEventID).FirstOrDefault());
            }
            catch (Exception ex)
            {
                _logger.Error("Thrown exception while calling method RemoveTDEvent", ex);
                throw ex;
            }
        }


        public bool IsExist(int TDEventID)
        {
            try
            {
                bool result = _ctx.TDEvents.Any(i => i.Id == TDEventID);
                _logger.Debug("Called IsExist of TDEventRepository");
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error("Thrown exception while calling method IsExist", ex);
                throw ex;
            }
        }


        public bool IsSaved()
        {
            try
            {
                bool result = (_ctx.SaveChanges() >= 0);
                _logger.Debug("Called IsSaved of TDEventRepository");
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error("Thrown exception while calling method IsSaved", ex);
                throw ex;
            }
        }
    }
}
