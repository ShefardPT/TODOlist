using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOlist.API;

namespace TDList.API.Models
{
    public class TDEventDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Status.Importance Importance { get; set; }

        public Status.Urgency Urgency { get; set; }

        
    }
}
