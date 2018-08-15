using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODOList.Models
{
    // Basic model of todo-list event
    public class TDEventDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Status.Importance Importance { get; set; }

        public Status.Urgency Urgency { get; set; }
    }
}
