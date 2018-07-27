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

        public TDEventRepository(Entities.TDEventContext ctx)
        {
            _ctx = ctx;
        }

        public Entities.TDEvent GetTDEvent(int TDEventID)
        {
            return _ctx.TDEvents.Where(p => p.Id == TDEventID).FirstOrDefault();
        }

        public IEnumerable<Entities.TDEvent> GetTDList()
        {
            return _ctx.TDEvents.OrderBy(o => o.Urgency).ToList();
        }

        public void PostTDEvent(Entities.TDEvent tdEventToAdd)
        {
            _ctx.Add(tdEventToAdd);
        }

        public void DeleteTDEvent(int TDEventID)
        {
            _ctx.Remove(_ctx.TDEvents.Where(p => p.Id == TDEventID).FirstOrDefault());
        }

        public bool IsSaved()
        {
            return (_ctx.SaveChanges() >= 0);
        }
    }
}
