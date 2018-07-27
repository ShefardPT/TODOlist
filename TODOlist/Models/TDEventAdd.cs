using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODOlist.API.Models
{
    public class TDEventAdd
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Status.Urgency Urgency { get; set; }

        public Status.Importance Importance { get; set; }
    }
}
