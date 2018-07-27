using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOlist.API;

namespace TDList.API
{
    public static class TDEventExtentions
    {
        public static void EnsureSeedDataForContext(this Entities.TDEventContext context)
        {
            if (context.TDEvents.Any())
            {
                return;
            }
            var TDEvents = new List<Entities.TDEvent>()
            {
                new Entities.TDEvent()
                {
                    Name = "Do something",
                    Description= "somewhen",
                    Importance = Status.Importance.INCONSIDERABLE,
                    Urgency = Status.Urgency.FARTHER,
                },
                new Entities.TDEvent()
                {
                    Name = "Do something NOW",
                    Description = "Or u r gonna die",
                    Importance = Status.Importance.IMPORTANT,
                    Urgency = Status.Urgency.URGENT,
                }
            };

            context.TDEvents.AddRange(TDEvents);
            context.SaveChanges();
        }
    }
}
