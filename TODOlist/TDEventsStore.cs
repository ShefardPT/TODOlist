using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOlist.API.Models;

namespace TODOlist.API
{
    public class TDEventsStore
    {
        public static TDEventsStore eventsStore { get; } = new TDEventsStore();

        public List<TDEventDTO> Events { get; set; }

        public TDEventsStore()
        {
            Events = new List<TDEventDTO>()
            {
                new TDEventDTO()
                {
                    ID = 1,
                    Name = "do something",
                    Description = "-",
                    Importance = Status.Importance.MEDIUM,
                    Urgency = Status.Urgency.MEDIUM,
                },
                new TDEventDTO()
                {
                    ID = 2,
                    Name = "Do something NOW",
                    Description = "-",
                    Importance = Status.Importance.IMPORTANT,
                    Urgency = Status.Urgency.URGENT,
                }
            };
        }
    }
}
