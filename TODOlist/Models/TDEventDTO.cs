using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TODOlist.API.Models
{
    public class TDEventDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Status.Urgency Urgency { get; set; }

        public Status.Importance Importance { get; set; }
    }
}
