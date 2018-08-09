using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOlist.API;

namespace TDList.API.Models
{
    // Model of todo-list event without ID. 
    // Designed for manipulation actions like addition and editing
    public class TDEventToManip
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Status.Importance Importance { get; set; }

        public Status.Urgency Urgency { get; set; }
    }
}
