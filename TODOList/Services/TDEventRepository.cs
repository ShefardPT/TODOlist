using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOList.Entities;

namespace TODOList.Services
{
    // todo-list events repository 
    public class TDEventRepository : ITDEventRepository
    {
        // Injecting TDEventContext as a store of TDEvent entities
        private TDEventContext _ctx;

        // Initializing logger
        public static Logger _logger = LogManager.GetCurrentClassLogger();

        // Initializing repository
        public TDEventRepository(TDEventContext ctx)
        {
            _ctx = ctx;
            _logger.Debug("TDEventRepository initialized");
        }

        // Getting list of all events as IEnumerable<TDEvent>
        public TDEvent GetTDEvent(int TDEventID)
        {
            try
            {
                var result = _ctx.TDEvents.Where(p => p.Id == TDEventID).FirstOrDefault();
                _logger.Debug("Called GetTDEvent of TDEventRepository");
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Thrown exception while calling method GetTDEvent");
                throw ex;
            }
        }

        // Getting exact event with ID == TDEventID as parameter
        public IEnumerable<TDEvent> GetTDList()
        {
            try
            {
                var result = _ctx.TDEvents.OrderByDescending(o => o.Urgency).ToList();
                _logger.Debug("Called GetTDList of TDEventRepository");
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Thrown exception while calling method GetTDList");
                throw ex;
            }
        }

        // Addition new event == tdEventToAdd as parameter
        public void AddTDEvent(TDEvent tdEventToAdd)
        {
            try
            {
                _ctx.Add(tdEventToAdd);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Thrown exception while calling method AddTDEvent");
                throw ex;
            }
        }

        // Removing exact event with ID == TDEventID as parameter
        public void RemoveTDEvent(int TDEventID)
        {
            try
            {
                _ctx.Remove(_ctx.TDEvents.Where(p => p.Id == TDEventID).FirstOrDefault());
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Thrown exception while calling method RemoveTDEvent");
                throw ex;
            }
        }

        // Checking if exact event with ID == TDEventID as parameter is exist
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
                _logger.Error(ex, "Thrown exception while calling method IsExist");
                throw ex;
            }
        }

        // Checking if event is saved using SaveChanges() method of context
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
                _logger.Error(ex, "Thrown exception while calling method IsSaved");
                throw ex;
            }
        }
    }
}
